function initializeCesium() {
    return new Cesium.Viewer("cesiumContainer");
}

function fetchGenerateEllipse(x1, y1, x2, y2, amountOfPoints) {
    const generateEllipseApiUrl = `http://localhost:5034/api/Data/GenerateEllipse?x1=${x1}&y1=${y1}&x2=${x2}&y2=${y2}&amountOfPoints=${amountOfPoints}`;
    return fetch(generateEllipseApiUrl).then(response => response.json());
}

function fetchSplitLine(x1, y1, x2, y2) {
    const splitLineApiUrl = `http://localhost:5034/api/Data/SplitLine?x1=${x1}&y1=${y1}&x2=${x2}&y2=${y2}`;
    return fetch(splitLineApiUrl).then(response => response.json());
}

function drawSplitLine(viewer, data) {
    viewer.entities.add({
        name: "white splitLine",
        polyline: {
            positions: Cesium.Cartesian3.fromDegreesArray(data),
            width: 5,
            material: new Cesium.PolylineDashMaterialProperty({
                color: Cesium.Color.WHITE,
            }),
        },
    });
}

function drawEllipse(viewer, data) {
    viewer.entities.add({
        name: "Red polygon",
        polygon: {
            hierarchy: Cesium.Cartesian3.fromDegreesArray(data),
            material: Cesium.Color.RED,
        },
    });
}

function drawCoordinates(viewer, x1, y1, x2, y2) {
    const pinBuilder = new Cesium.PinBuilder();
    viewer.entities.add({
        name: "p1 mark",
        position: Cesium.Cartesian3.fromDegrees(x1, y1),
        billboard: {
            image: pinBuilder.fromText("p1", Cesium.Color.BLUE, 48).toDataURL(),
            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
        },
    });

    viewer.entities.add({
        name: "P2 mark",
        position: Cesium.Cartesian3.fromDegrees(x2, y2),
        billboard: {
            image: pinBuilder.fromText("p2", Cesium.Color.GREEN, 48).toDataURL(),
            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
        },
    });
}

function initializeAndDraw(x1, y1, x2, y2, amountOfPoints) {
    const viewer = initializeCesium();
    drawCoordinates(viewer, x1, y1, x2, y2);

    fetchGenerateEllipse(x1, y1, x2, y2, amountOfPoints)
        .then(data => {
            drawEllipse(viewer, data);
        })
        .catch(error => {
            console.error('Error:', error);
        });

    fetchSplitLine(x1, y1, x2, y2)
        .then(data => {
            drawSplitLine(viewer, data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


const x1 = 40;
const y1 = 30;
const x2 = 70;
const y2 = 50;
const amountOfPoints = 18;

initializeAndDraw(x1, y1, x2, y2, amountOfPoints);