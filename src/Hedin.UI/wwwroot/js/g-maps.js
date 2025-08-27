// ----------------------
// Light theme
// ----------------------
const lightMapStyle = [
    // locality labels
    {
        featureType: "administrative.locality",
        elementType: "all",
        stylers: [
            { hue: "#2c2e33" },
            { saturation: 7 },
            { lightness: 19 },
            { visibility: "on" }
        ]
    },

    // terrain / landscape
    {
        featureType: "landscape",
        elementType: "geometry",
        stylers: [
            { color: "#BEE0CB" }
        ]
    },
    {
        featureType: "landscape.natural",
        elementType: "geometry.fill",
        stylers: [
            { color: "#B0D9C0" }
        ]
    },

    // points of interest (hidden)
    {
        featureType: "poi",
        elementType: "all",
        stylers: [
            { hue: "#ffffff" },
            { saturation: -100 },
            { lightness: 100 },
            { visibility: "off" }
        ]
    },

    // roads: highways
    {
        featureType: "road.highway",
        elementType: "geometry.fill",
        stylers: [
            { color: "#7D95AE" },
            { visibility: "simplified" }
        ]
    },
    {
        featureType: "road.highway",
        elementType: "geometry.stroke",
        stylers: [
            { color: "#7D95AE" },
            { weight: 1 },
            { visibility: "on" }
        ]
    },

    // roads: arterials & locals
    {
        featureType: "road.arterial",
        elementType: "geometry",
        stylers: [
            { color: "#C4CAD0" },
            { visibility: "simplified" }
        ]
    },
    {
        featureType: "road.local",
        elementType: "geometry",
        stylers: [
            { color: "#C4CAD0" },
            { visibility: "simplified" }
        ]
    },

    // ferry / transit lines
    {
        featureType: "transit.line",
        elementType: "geometry",
        stylers: [
            { color: "#5B9CA8" },
            { weight: 2 },
            { visibility: "on" }
        ]
    },
    {
        featureType: "transit.line",
        elementType: "labels.text.fill",
        stylers: [
            { color: "#5B9CA8" },
            { visibility: "on" }
        ]
    },

    // water
    {
        featureType: "water",
        elementType: "geometry",
        stylers: [
            { color: "#81C5D7" },
            { visibility: "simplified" }
        ]
    }
];


// ----------------------
// Dark theme (final version)
// ----------------------
const darkMapStyle = [
    // 1) Global labels base
    {
        featureType: "all",
        elementType: "labels.text.fill",
        stylers: [
            { saturation: 36 },
            { color: "#000000" },
            { lightness: 40 }
        ]
    },
    {
        featureType: "all",
        elementType: "labels.text.stroke",
        stylers: [
            { visibility: "on" },
            { color: "#000000" },
            { lightness: 16 }
        ]
    },
    {
        featureType: "all",
        elementType: "labels.icon",
        stylers: [
            { visibility: "off" }
        ]
    },

    // 2) Administrative (hidden)
    {
        featureType: "administrative",
        elementType: "geometry",
        stylers: [
            { visibility: "off" }
        ]
    },
    {
        featureType: "administrative",
        elementType: "labels.text.fill",
        stylers: [
            { color: "#dbdbdb" }
        ]
    },

    // 3) Landscape & terrain
    {
        featureType: "landscape",
        elementType: "geometry",
        stylers: [
            { color: "#242424" }
        ]
    },
    {
        featureType: "landscape.natural",
        elementType: "geometry.fill",
        stylers: [
            { color: "#1C1C1C" }
        ]
    },

    // 4) POIs
    {
        featureType: "poi",
        elementType: "geometry",
        stylers: [
            { color: "#1C1C1C" },
            { visibility: "on" }
        ]
    },
    {
        featureType: "poi",
        elementType: "labels.text",
        stylers: [
            { visibility: "off" }
        ]
    },

    // 5) Water
    {
        featureType: "water",
        elementType: "geometry",
        stylers: [
            { color: "#334C70" }
        ]
    },

    // 6) Transit lines
    {
        featureType: "transit.line",
        elementType: "geometry",
        stylers: [
            { color: "#1C1C1C" },
            { visibility: "on" }
        ]
    },

    // 7) Road name labels (toned down + heavy outline)
    {
        featureType: "road",
        elementType: "labels.text.fill",
        stylers: [
            { color: "#888888" }
        ]
    },
    {
        featureType: "road",
        elementType: "labels.text.stroke",
        stylers: [
            { color: "#000000" },
            { weight: 3 }
        ]
    },

    // 8) Road geometry (darker at all zooms)
    {
        featureType: "road.highway",
        elementType: "geometry.fill",
        stylers: [
            { color: "#2A2A2A" },
            { visibility: "on" }
        ]
    },
    {
        featureType: "road.highway",
        elementType: "geometry.stroke",
        stylers: [
            { color: "#000000" }
        ]
    },
    {
        featureType: "road.arterial",
        elementType: "geometry",
        stylers: [
            { color: "#282828" }
        ]
    },
    {
        featureType: "road.local",
        elementType: "geometry",
        stylers: [
            { color: "#202020" }
        ]
    }
];





// ----------------------
// Light theme (omitted)
// ----------------------
// const lightMapStyle = [ /* your light theme styles */ ];

// ----------------------
// Dark theme (omitted)
// ----------------------
// const darkMapStyle = [ /* your dark theme styles */ ];

// ------------------------------------------------------
// Helper: load an image (with CORS) for canvas drawing
// ------------------------------------------------------
function loadImageAsElement(url) {
    return new Promise((resolve, reject) => {
        const img = new Image();
        img.crossOrigin = 'anonymous';
        img.onload = () => resolve(img);
        img.onerror = reject;
        img.src = url;
    });
}

// Detect URL or data‐URI or local file path
function isUrl(str) {
    return typeof str === 'string' && (
        str.startsWith('http://') ||
        str.startsWith('https://') ||
        str.startsWith('data:') ||
        str.startsWith('/') ||
        /\.(svg|png|jpg|jpeg|gif)(\?.*)?$/i.test(str)
    );
}

// Wrap raw SVG path or fragment into a full SVG data URI
function toDataUrl(spec, size, fillColor = '#000') {
    const s = spec.trim();
    let inner = s.startsWith('<')
        ? s.replace(/<path\b([^>]*?)>/gi, (m, attrs) => /\bfill=/i.test(attrs) ? `<path${attrs}>` : `<path fill="${fillColor}"${attrs}>`)
        : `<path d="${s}" fill="${fillColor}"/>`;
    const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="${size}" height="${size}">${inner}</svg>`;
    return 'data:image/svg+xml;charset=UTF-8,' + encodeURIComponent(svg);
}

/**
 * Always paint into a square canvas of exactly `size×size`,
 * rotate the base image, then draw it centered.
 */
function makeRotatedCanvasIcon(url, size, heading) {
    return loadImageAsElement(url).then(img => {
        const canvas = document.createElement('canvas');
        canvas.width = size;
        canvas.height = size;
        const ctx = canvas.getContext('2d');

        ctx.translate(size / 2, size / 2);
        ctx.rotate((heading || 0) * Math.PI / 180);
        ctx.drawImage(img, -size / 2, -size / 2, size, size);

        const dataUrl = canvas.toDataURL('image/png');
        const s = new google.maps.Size(size, size);
        return { url: dataUrl, size: s, scaledSize: s, origin: new google.maps.Point(0, 0), anchor: new google.maps.Point(size / 2, size / 2), optimized: false };
    });
}

/**
 * Square canvas of `baseSize`×`baseSize`.
 * Rotates the base, then overlays the badge un-rotated, both centered.
 */
function makeCompositeCanvasIcon(baseSpec, baseSize, heading, badgeSpec, badgeSize, baseColor, badgeColor) {
    const baseUrl = isUrl(baseSpec) ? baseSpec : toDataUrl(baseSpec, baseSize, baseColor);
    const bSize = badgeSize || Math.round(baseSize / 3);
    const badgeUrl = badgeSpec
        ? (isUrl(badgeSpec) ? badgeSpec : toDataUrl(badgeSpec, bSize, badgeColor))
        : null;

    return Promise.all([loadImageAsElement(baseUrl),
    badgeUrl ? loadImageAsElement(badgeUrl) : Promise.resolve(null)
    ]).then(([baseImg, badgeImg]) => {
        const canvas = document.createElement('canvas');
        canvas.width = baseSize;
        canvas.height = baseSize;
        const ctx = canvas.getContext('2d');

        ctx.translate(baseSize / 2, baseSize / 2);
        ctx.save();
        ctx.rotate((heading || 0) * Math.PI / 180);
        ctx.drawImage(baseImg, -baseSize / 2, -baseSize / 2, baseSize, baseSize);
        ctx.restore();

        if (badgeImg) {
            ctx.drawImage(badgeImg, -bSize / 2, -bSize / 2, bSize, bSize);
        }

        const dataUrl = canvas.toDataURL('image/png');
        const s = new google.maps.Size(baseSize, baseSize);
        return { url: dataUrl, size: s, scaledSize: s, origin: new google.maps.Point(0, 0), anchor: new google.maps.Point(baseSize / 2, baseSize / 2), optimized: false };
    });
}

let googleMapsApiLoading = false;
window.googleMapsInstances = window.googleMapsInstances || {};

window.googleMaps = {
    createInstance(mapId, apiKey, points, dotNetRef, autoFit, isDark, controlOptions) {
        function normalize(opts) {
            const out = {};
            [
                'disableDefaultUI', 'mapTypeControl', 'fullscreenControl',
                'streetViewControl', 'rotateControl', 'zoomControl',
                'clickableIcons', 'keyboardShortcuts'
            ].forEach(key => {
                let v = opts[key];
                if (v === undefined) {
                    v = opts[key.charAt(0).toUpperCase() + key.slice(1)];
                }
                if (typeof v === 'boolean') out[key] = v;
            });
            return out;
        }

        function buildMap() {
            const el = document.getElementById(mapId);
            if (!el) return;
            const center = points.length
                ? { lat: points[0].lat, lng: points[0].lng }
                : { lat: 0, lng: 0 };
            const zoom = points.length === 1 ? 10 : 8;
            const styles = isDark ? darkMapStyle : lightMapStyle;
            const baseOpts = { center, zoom, styles, keyboardShortcuts: false };
            const normOpts = normalize(controlOptions);
            const mapOpts = Object.assign(
                {}, baseOpts, normOpts,
                controlOptions.minZoom != null ? { minZoom: controlOptions.minZoom } : {},
                controlOptions.maxZoom != null ? { maxZoom: controlOptions.maxZoom } : {}
            );
            const map = new google.maps.Map(el, mapOpts);
            const inst = { map, pts: {}, dotNetRef, autoFit, following: null };
            window.googleMapsInstances[mapId] = inst;
            map.addListener('dragstart', () => inst.following = null);
            map.addListener('zoom_changed', () => inst.following = null);
            window.googleMaps.updatePoints(mapId, points, autoFit);
        }

        if (!window.google || !window.google.maps) {
            if (!googleMapsApiLoading) {
                googleMapsApiLoading = true;
                const s = document.createElement('script');
                s.async = true;
                s.defer = true;
                s.src = `https://maps.googleapis.com/maps/api/js?key=${apiKey}&callback=__onGoogleMapsLoaded`;
                document.head.appendChild(s);
            }
            window.googleMapsInstances.pending = window.googleMapsInstances.pending || [];
            window.googleMapsInstances.pending.push({ mapId, apiKey, points, dotNetRef, autoFit, isDark, controlOptions });
        } else {
            buildMap();
        }
    },

    updatePoints(mapId, points, autoFit) {
        const inst = window.googleMapsInstances[mapId];
        if (!inst || !inst.map) return;

        // ─────────── NEW: Remove any markers that are no longer in `points` ───────────
        const incomingIds = points.map(p => p.id);
        // Iterate over all IDs we previously stored in inst.pts
        Object.keys(inst.pts).forEach(existingId => {
            if (!incomingIds.includes(existingId)) {
                // This ID has been removed from the data source—tear it down:
                const { marker, polyline } = inst.pts[existingId];
                // Remove from map
                marker.setMap(null);
                polyline.setMap(null);
                // Delete from our local registry so it's not updated again
                delete inst.pts[existingId];
            }
        });

        function getIconDef(p) {
            if (p.icon) {
                const sz = p.iconSize ?? 64;
                const hdg = p.heading ?? 0;
                const baseColor = p.iconColor ?? '#4CAF50';

                // build URL or data-URI for the base
                const baseUrl = isUrl(p.icon)
                    ? p.icon
                    : toDataUrl(p.icon, sz, baseColor);

                if (p.badge) {
                    const badgeSz = p.badgeSize ?? Math.round(sz / 3);
                    const badgeClr = p.badgeColor ?? '#f00';
                    const badgeUrl = isUrl(p.badge)
                        ? p.badge
                        : toDataUrl(p.badge, badgeSz, badgeClr);

                    return makeCompositeCanvasIcon(baseUrl, sz, hdg, badgeUrl, badgeSz, baseColor, badgeClr);
                }

                return makeRotatedCanvasIcon(baseUrl, sz, hdg);
            }

            // fallback arrow
            return {
                path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW,
                scale: 4,
                rotation: p.heading || 0,
                fillColor: '#f00',
                fillOpacity: 0.8,
                strokeWeight: 2,
                strokeColor: '#fff',
                anchor: new google.maps.Point(0, 0)
            };
        }

        function updateMarkerIcon(marker, p) {
            const def = getIconDef(p);
            if (def.then) {
                def.then(d => marker.setIcon(d));
            } else {
                marker.setIcon(def);
            }
        }

     points.forEach(p => {
        const pos = new google.maps.LatLng(p.lat, p.lng);
        const entry = inst.pts[p.id];

        if (entry) {
            // Existing marker: move it, update its icon, and extend its polyline
            entry.marker.setPosition(pos);
            updateMarkerIcon(entry.marker, p);
            entry.polyline.getPath().push(pos);
        } else {
            // Brand‐new marker: create it and store in inst.pts
            const marker = new google.maps.Marker({ position: pos, map: inst.map, visible: false });
            updateMarkerIcon(marker, p);
            Promise.resolve(getIconDef(p)).then(() => marker.setVisible(true));
            marker.addListener('click', () => {
                inst.map.panTo(pos);
                setTimeout(() => inst.map.setZoom(15), 300);
                inst.dotNetRef.invokeMethodAsync('PointClicked', p);
            });

            const polyline = new google.maps.Polyline({
                path: [pos],
                geodesic: true,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2,
                map: inst.map
            });

            inst.pts[p.id] = { marker, polyline };
        }
    });

        if (autoFit) {
            const coords = points.map(p => ({ lat: p.lat, lng: p.lng }));
            const toRad = d => d * Math.PI / 180;
            const R = 6371;
            const center = coords.reduce((a, c) => ({ lat: a.lat + c.lat, lng: a.lng + c.lng }), { lat: 0, lng: 0 });
            center.lat /= coords.length; center.lng /= coords.length;
            const good = coords.filter(c => {
                const dLat = toRad(c.lat - center.lat);
                const dLng = toRad(c.lng - center.lng);
                const a = Math.sin(dLat / 2) ** 2 + Math.cos(toRad(center.lat)) * Math.cos(toRad(c.lat)) * Math.sin(dLng / 2) ** 2;
                return 2 * R * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a)) <= 500;
            });
            if (good.length > 1) {
                const bounds = new google.maps.LatLngBounds();
                good.forEach(c => bounds.extend(c));
                inst.map.fitBounds(bounds);
            } else if (good.length === 1) {
                inst.map.setCenter(good[0]);
                inst.map.setZoom(15);
            }
        }

        if (inst.following) {
            const e = inst.pts[inst.following];
            if (e) inst.map.panTo(e.marker.getPosition());
        }
    }
    ,

    destroyInstance(mapId) {
        const inst = window.googleMapsInstances[mapId];
        if (!inst) return;
        Object.values(inst.pts).forEach(v => {
            v.marker.setMap(null);
            v.polyline.setMap(null);
        });
        delete window.googleMapsInstances[mapId];
    },

    setStyle(mapId, isDark) {
        const inst = window.googleMapsInstances[mapId];
        if (!inst || !inst.map) return;
        inst.map.setOptions({ styles: isDark ? darkMapStyle : lightMapStyle });
    }
};

function __onGoogleMapsLoaded() {
    const pending = window.googleMapsInstances.pending || [];
    pending.forEach(cfg => {
        window.googleMaps.createInstance(
            cfg.mapId, cfg.apiKey, cfg.points, cfg.dotNetRef,
            cfg.autoFit, cfg.isDark, cfg.controlOptions
        );
    });
    window.googleMapsInstances.pending = [];
}

window.googleMaps.focusOnPoint = function (mapId, pointId, targetZoom, follow, offsetX = 0, offsetY = 0) {
    const inst = window.googleMapsInstances[mapId];
    if (!inst || !inst.map) return;
    const map = inst.map;
    const entry = inst.pts[pointId];
    if (!entry) return;
    const latlng = entry.marker.getPosition();
    const currZ = map.getZoom();
    let midZ = currZ;
    if (typeof targetZoom === 'number' && currZ > targetZoom) {
        midZ = Math.max(targetZoom, currZ - 2);
    }
    const waitIdle = () => new Promise(res => google.maps.event.addListenerOnce(map, 'idle', res));
    let chain = Promise.resolve();
    if (midZ !== currZ) {
        map.setZoom(midZ);
        chain = chain.then(waitIdle);
    }
    chain = chain.then(() => { map.panTo(latlng); return waitIdle(); });
    if (typeof targetZoom === 'number' && midZ !== targetZoom) {
        chain = chain.then(() => { map.setZoom(targetZoom); return waitIdle(); });
    }
    if (offsetX || offsetY) {
        chain = chain.then(() => { map.panBy(offsetX, offsetY); return waitIdle(); });
    }
    chain.then(() => { inst.following = follow ? pointId : null; });
};