// Sticky horizontal scrollbar for HUIDataGrid (StickyHorizontalScroll).
// Mirrors the table container's horizontal scroll onto a Blazor-rendered proxy bar that is position: sticky at the
// viewport bottom. All DOM here is owned by Blazor, so this only reads it, sets the inner width, syncs scrollLeft and
// toggles one data attribute on the host (never a class or style Blazor renders).

export function init(host) {
    const ATTR = 'data-hui-sticky-hscroll';

    // Both are re-queried every time: the grid can replace them on re-render. The bar sits at the top of the pager slot
    // (between rows and pager, like the native bar), or directly after the grid when there is no pager.
    const scroller = () => host.querySelector(':scope > .mud-table > .mud-table-container');
    const bar = () => host.querySelector(
        ':scope > .mud-table > .mud-table-pagination > .hui-datagrid-hscroll-bar, :scope > .hui-datagrid-hscroll-bar');
    if (!bar()) return null;

    let frame = 0;
    let warned = false;

    // Stickiness only works if nothing between the grid and the page scroller clips its content. A clipping ancestor
    // is one whose overflow is not visible but which does not actually scroll (content fits), so the bar sticks to it.
    const warnIfClipped = () => {
        if (warned) return;
        warned = true;
        for (let el = host.parentElement; el && el !== document.body && el !== document.documentElement; el = el.parentElement) {
            const style = getComputedStyle(el);
            if (style.overflowY === 'visible' && style.overflowX === 'visible') continue;
            if (style.overflowY === 'clip' && style.overflowX === 'clip') continue; // clip does not create a scroll container
            const scrolls = style.overflowY === 'auto' || style.overflowY === 'scroll';
            if (scrolls && el.scrollHeight > el.clientHeight + 1) continue; // a real scroller (page, dialog body)
            console.warn('HUIDataGrid StickyHorizontalScroll: ancestor clips the grid, so the scrollbar will stick to it rather than the viewport.', el);
            return;
        }
    };

    const update = () => {
        frame = 0;
        const s = scroller();
        const b = bar();
        // Bar removed (parameter toggled off): never leave the native bar hidden without a proxy.
        if (!s || !b) { host.removeAttribute(ATTR); return; }
        // scrollLeft is negative in RTL; not supported, so leave the native bar alone.
        const overflowing = s.scrollWidth > s.clientWidth + 1 && getComputedStyle(host).direction !== 'rtl';
        // Match the scroll range, not the content width: the scroller can be narrower than the bar (vertical scrollbar).
        b.firstElementChild.style.width = (s.scrollWidth - s.clientWidth + b.clientWidth) + 'px';
        host.toggleAttribute(ATTR, overflowing);
        if (overflowing) {
            warnIfClipped();
            if (Math.abs(b.scrollLeft - s.scrollLeft) > 1) {
                drive('scroller');
                b.scrollLeft = s.scrollLeft;
            }
        }
    };
    const schedule = () => { if (!frame) frame = requestAnimationFrame(update); };

    // Two-way sync: only the side the user is driving writes to the other; the echo event from that write is ignored
    // for a short window. Comparing scrollLeft with !== instead makes fractional values ping-pong and the bars jitter.
    let driver = null;
    let release = 0;
    const drive = who => {
        driver = who;
        clearTimeout(release);
        release = setTimeout(() => { driver = null; }, 80);
    };
    // One capturing listener on the host (scroll does not bubble) serves both sides, so a replaced bar needs no re-wiring.
    const onScroll = e => {
        const s = scroller();
        const b = bar();
        if (!s || !b) return;
        if (e.target === s && driver !== 'bar') {
            drive('scroller');
            b.scrollLeft = s.scrollLeft;
        } else if (e.target === b && driver !== 'scroller') {
            drive('bar');
            s.scrollLeft = b.scrollLeft;
        }
    };
    host.addEventListener('scroll', onScroll, { capture: true, passive: true });

    const resize = new ResizeObserver(schedule);
    let observed = [];
    const measured = () => {
        const s = scroller();
        return [s, s ? s.querySelector('table') : null, bar()];
    };
    const observe = () => {
        resize.disconnect();
        observed = measured();
        observed.forEach(el => el && resize.observe(el));
    };
    // Rows render constantly (paging, virtualization); only react when the elements we measure were replaced.
    const mutations = new MutationObserver(() => {
        if (measured().some((el, i) => el !== observed[i])) {
            observe();
            schedule();
        }
    });
    mutations.observe(host, { childList: true, subtree: true });

    observe();
    update();

    return {
        dispose() {
            if (frame) cancelAnimationFrame(frame);
            resize.disconnect();
            mutations.disconnect();
            clearTimeout(release);
            host.removeEventListener('scroll', onScroll, true);
            host.removeAttribute(ATTR);
        }
    };
}
