; (function () {
    const script = document.createElement("script");
    // NOTE the leading slash: root-relative to your host app
    script.src = "_content/Hedin.UI/js/g-maps.js";
    script.async = false;   // execute immediately, in order
    document.head.appendChild(script);
})();


function saveAsFile(filename, bytesBase64) {
    const link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function observeAppbarHeight(elementId) {
    const element = document.getElementById(elementId);
    if (!element) return;
    
    let debounceTimeout;
    let lastHeight = 0;
    
    const observer = new ResizeObserver(entries => {
        for (let entry of entries) {
            clearTimeout(debounceTimeout);
            debounceTimeout = setTimeout(() => {
                const height = entry.contentRect.height;
                // Only update if the height has changed
                if (height !== lastHeight) {
                    lastHeight = height;
                    const r = document.querySelector(':root');
                    r.style.setProperty('--mud-appbar-height', `${height}px`);
                }
            }, 100); // Adjust debounce delay as needed
        }
    });
    
    observer.observe(element);
    
    return observer;
}



