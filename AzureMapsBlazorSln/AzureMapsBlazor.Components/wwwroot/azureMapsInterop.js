var map = null;
var isMapInitialized = false;

export function InitMap(elementId, options) {
    var configuredMapOptions =
    {
        center: [options.center.longitude, options.center.latitude],
        zoom: options.zoom,
        language: options.language,
        authOptions: options.authOptions
    };
    //var map = new atlas.Map(elementId, defaultMapOptions);
    map = new atlas.Map(elementId, configuredMapOptions);
    isMapInitialized = true;
}

var mapsDataSource = null;
var isLayerCreated = false;
var routeLayer = null;

export function RenderLine(startingPoint, finalPoint, pointsInRoute) {
    map.setCamera({
        center: [startingPoint.longitude, startingPoint.latitude],
    });
    if (mapsDataSource == null) {
        mapsDataSource = new atlas.source.DataSource();
        //Create a data source and add it to the map.
        map.sources.add(mapsDataSource);
    }
    else {
        mapsDataSource.clear();
    }
    var lineStart = startingPoint;
    if (pointsInRoute && pointsInRoute.length > 0) {
        pointsInRoute.forEach((element, index) => {
            var lineEnd = element;
            mapsDataSource.add(new atlas.data.LineString([[lineStart.longitude, lineStart.latitude], [element.longitude, element.latitude]]));
            lineStart = element;

        });
    }
    mapsDataSource.add(new atlas.data.LineString([[lineStart.longitude, lineStart.latitude], [finalPoint.longitude, finalPoint.latitude]]));
    //Create a line and add it to the data source.
    //mapsDataSource.add(new atlas.data.LineString([[startingPoint.longitude, startingPoint.latitude], [finalPoint.longitude, finalPoint.latitude]]));

    //Create a line layer to render the line to the map.
    if (routeLayer != null)
        map.layers.remove(routeLayer);
    routeLayer = new atlas.layer.LineLayer(mapsDataSource, null, {
        strokeColor: 'blue',
        strokeWidth: 5
    });
    map.layers.add(routeLayer);
}

export function SearchRoute(elementId, mapOptions, startingPoint, finalPoint, pointsInRoute) {
    if (!isMapInitialized) {
        InitMap(elementId, mapOptions);
        map.events.add('ready', function () {
            RenderLine(startingPoint, finalPoint, pointsInRoute);
        });
    }
    else {
        RenderLine(startingPoint, finalPoint, pointsInRoute);
    }
}