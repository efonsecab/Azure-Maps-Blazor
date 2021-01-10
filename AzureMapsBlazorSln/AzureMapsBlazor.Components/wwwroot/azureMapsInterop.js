var map = null;

export function InitMap(elementId, options) {
    debugger;
    var configuredMapOptions =
    {
        center: [options.center.longitude, options.center.latitude],
        zoom: options.zoom,
        language: options.language,
        authOptions: options.authOptions
    };
    //var map = new atlas.Map(elementId, defaultMapOptions);
    map = new atlas.Map(elementId, configuredMapOptions);
    return map;
}

var mapsDataSource = null;

export function RenderLine(startingPoint, finalPoint, pointsInRoute) {
    debugger;
    if (mapsDataSource == null) {
        mapsDataSource = new atlas.source.DataSource();
        //Create a data source and add it to the map.
        map.sources.add(mapsDataSource);
    }
    var lineStart = startingPoint;
    pointsInRoute.forEach((element, index) => {
        var lineEnd = element;
        mapsDataSource.add(new atlas.data.LineString([[lineStart.longitude, lineStart.latitude], [element.longitude, element.latitude]]));
        lineStart = element;

    });
    mapsDataSource.add(new atlas.data.LineString([[lineStart.longitude, lineStart.latitude], [finalPoint.longitude, finalPoint.latitude]]));
    //Create a line and add it to the data source.
    //mapsDataSource.add(new atlas.data.LineString([[startingPoint.longitude, startingPoint.latitude], [finalPoint.longitude, finalPoint.latitude]]));

    //Create a line layer to render the line to the map.
    map.layers.add(new atlas.layer.LineLayer(mapsDataSource, null, {
        strokeColor: 'blue',
        strokeWidth: 5
    }));
}