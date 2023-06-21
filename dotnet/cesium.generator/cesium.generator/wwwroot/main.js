const viewer = new Cesium.Viewer("cesiumContainer");

const x1 = 40;
const y1 = 30;
const x2 = 70;
const y2 = 50;

const amountOfPoints = 18;

// Build the API URL with the query parameters
const GenerateEllipseApiUrl = `http://localhost:5034/api/Data/GenerateEllipse?x1=${x1}&y1=${y1}&x2=${x2}&y2=${y2}&amountOfPoints=${amountOfPoints}`;

// Make the API request
fetch(GenerateEllipseApiUrl)
    .then(response => response.json())
    .then(data => {
        // Process the response data
        drawElipse(data); // Do something with the received points
    })
    .catch(error => {
        // Handle any errors
        console.error('Error:', error);
    });



// Build the API URL with the query parameters
const SplitLineapiUrl = `http://localhost:5034/api/Data/SplitLine?x1=${x1}&y1=${y1}&x2=${x2}&y2=${y2}`;

// Make the API request
fetch(SplitLineapiUrl)
    .then(response => response.json())
    .then(data => {
        // Process the response data
        drawSplitLine(data); // Do something with the received points
    })
    .catch(error => {
        // Handle any errors
        console.error('Error:', error);
    });

function drawSplitLine(data) {
    console.log("drawSplitLine data="+data)
    viewer.entities.add({
        name: "white splitLine",
        polyline: {
            positions: Cesium.Cartesian3.fromDegreesArray(data),
            width: 5,
            material: new Cesium.PolylineDashMaterialProperty({
                color: Cesium.Color.WHITE,
            }),
        }
    });
}

function drawElipse(data) {
    console.log("drawElipse data=" + data)
    viewer.entities.add({
        name: "Red polygon",
        polygon: {
            hierarchy: Cesium.Cartesian3.fromDegreesArray(data),
            material: Cesium.Color.RED,
        },
    });
}

function drawCoordinates(x1, y1, x2, y2) {
    console.log("Adding coordinates");
    const pinBuilder = new Cesium.PinBuilder();
    viewer.entities.add({
        name: "p1 mark",
        position: Cesium.Cartesian3.fromDegrees(x1,y1),
        billboard: {
            image: pinBuilder.fromText("p1", Cesium.Color.BLUE, 48).toDataURL(),
            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
        },
    });

    viewer.entities.add({
        name: "P2 mark",
        position: Cesium.Cartesian3.fromDegrees(x2,y2),
        billboard: {
            image: pinBuilder.fromText("p2", Cesium.Color.GREEN, 48).toDataURL(),
            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
        },
    });
}

drawCoordinates(x1,y1,x2,y2);