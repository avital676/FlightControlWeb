var googleMap;
var markers = {};
var inside = false;

function initMap() {
    //map options
    var options = {
        zoom: 2,
        center: { lat: 32.00683, lng: 34.88533 },
        mapTypeId: 'terrain',
        id: 'googleMap'
    }
    $("#dragAndDrop").hide();
    //creating new map
    googleMap = new google.maps.Map(document.getElementById('map'), options);
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
    });
    markers[props.content.flightId] = marker;
    //check info window
    if (props.content) {
        //add info window
        content: props.content
    };
    marker.addListener('click', function () {
        inside = true;
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

    var flightPath = new google.maps.Polyline({
        path: flightPlanCoordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

    flightPath.setMap(googleMap);
}

function clearMarkers() {
    // Removes the markers from the map, but keeps them in the array.
    setMapOnAll(null);
}