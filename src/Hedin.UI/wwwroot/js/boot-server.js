(() => {
    const maximumRetryCount = 50;
    const retryIntervalMilliseconds = 1000;   // fire every second
    // backoff no longer used, but you could re-introduce a smaller incremental delay if desired
    const reconnectModal = document.getElementById('components-reconnect-modal');
    const currentAttempt = document.getElementById('components-reconnect-current-attempt');
    const maxAttempts = document.getElementById('components-reconnect-max-retries');

    const startReconnectionProcess = () => {
        console.log('Reconnection started');
        reconnectModal.classList.add('components-reconnect-show');
        currentAttempt.innerText = '1';
        maxAttempts.innerText = maximumRetryCount.toString();
        let isCanceled = false;

        (async () => {
            for (let i = 0; i < maximumRetryCount; i++) {
                const attemptNumber = i + 1;
                currentAttempt.innerText = `${attemptNumber}`;
                console.log(`Reconnecting: ${attemptNumber} of ${maximumRetryCount}`);

                // **Immediate first try** (so no initial wait)
                try {
                    const result = await Blazor.reconnect();
                    if (result === false) {
                        // reached server but rejected: full reload
                        location.reload();
                        return;
                    }
                    // success!
                    console.log('Reconnected on attempt', attemptNumber);
                    return;
                } catch (err) {
                    console.log('Reconnect attempt failed:', err);
                }

                if (isCanceled) {
                    console.log('Reconnection cancelled');
                    return;
                }

                // wait exactly 1 s before next attempt
                await new Promise(resolve => setTimeout(resolve, retryIntervalMilliseconds));
            }

            // exhausted all attempts → reload
            console.log('Max reconnect attempts reached; reloading');
            location.reload();
        })();

        return {
            cancel: () => {
                isCanceled = true;
                reconnectModal.classList.remove('components-reconnect-show');
                // restore scroll position
                const scrollY = document.body.style.top;
                document.body.style.position = '';
                document.body.style.top = '';
                window.scrollTo(0, parseInt(scrollY || '0', 10) * -1);
            }
        };
    };

    let currentReconnectionProcess = null;
    Blazor.start({
        circuit: {
            reconnectionHandler: {
                onConnectionDown: () => {
                    // start immediately if not already started
                    if (!currentReconnectionProcess) {
                        currentReconnectionProcess = startReconnectionProcess();
                    }
                },
                onConnectionUp: () => {
                    if (currentReconnectionProcess) {
                        currentReconnectionProcess.cancel();
                        currentReconnectionProcess = null;
                    }
                }
            }
        }
    });
})();