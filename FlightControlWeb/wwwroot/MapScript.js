var googleMap;
var markers = [];
var flightPath;
var line = [];
function initMap() {
    //map options
    var options = {
        zoom: 2,
        center: { lat: 32.00683, lng: 34.88533 },
        mapTypeId: 'terrain'
    }
    $("#dragAndDrop").hide();
    //creating new map
    googleMap = new google.maps.Map(document.getElementById('map'), options);
    //showLine(37, -122, 41.4668, 153.027);
}

function addMarker(props) {
    var icon = {
        url: "plane-icon.png", // url
        scaledSize: new google.maps.Size(20, 20), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
    };

    var marker = new google.maps.Marker({
        position: props.coords,
        map: googleMap,
        icon: icon,
        id: props.content.flightId,
    });
    markers.push(marker);
    //check info window
    if (props.content) {
        //add info window
        content: props.content
    };
    marker.addListener('click', function () {
        var x = document.getElementById("detailes");
        if (x.style.display === "none") {
        } else {
            selectFlight(props.content.flightId);
        }
    });
}



function showLine(a, b, c, d) {
    var flightPlanCoordinates = [
        { lat: a, lng: b },
        { lat: c, lng: d }
    ];

     flightPath = new google.maps.Polyline({
        path: flightPlanCoordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
     });
    line.push(flightPath);
    flightPath.setMap(googleMap);
}



// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}
