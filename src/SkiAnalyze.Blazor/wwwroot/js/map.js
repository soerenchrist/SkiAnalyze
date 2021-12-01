let map;
let polyLayerGroup;

window.displayMap = (divName, mapOptions, tileLayerOptions) => {
    map = L.map(divName)
        .setView(mapOptions.center, mapOptions.zoomLevel);

    L.tileLayer(tileLayerOptions.layer, {
        attribution: tileLayerOptions.attribution,
        maxZoom: tileLayerOptions.maxZoom
    }).addTo(map);
}

window.setPolylines = (polylines) => {
    console.log(polylines);
    if (polyLayerGroup) {
        polyLayerGroup.removeFrom(map);
    }
    polyLayerGroup = L.layerGroup();
    
    polylines.forEach(line => {
        polyLayerGroup.addLayer(L.polyline(line.latLngs, { color: line.color }));
    });

    polyLayerGroup.addTo(map);
}