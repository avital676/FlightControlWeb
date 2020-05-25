let googleMap;
let markers = {};
let inside = false;

// Initialize map
function initMap() {
    // map options
    let options = {
        zoom: 2,
        center: { lat: 32.00683, lng: 34.88533 },
        mapTypeId: 'terrain',
        id: 'googleMap'
    };
    $("#dragAndDrop").hide();
    // creat new map
    googleMap = new google.maps.Map(document.getElementById('map'), options);
}

// Add markers to map
function addMarker(props) {
    let icon = {
        url: "plane-icon.png", // url
        scaledSize: new google.maps.Size(20, 20), // scaled size
        origin: new google.maps.Point(0, 0), // origin
        anchor: new google.maps.Point(0, 0) // anchor
    };
    let marker = new google.maps.Marker({
        position: props.coords,
        map: googleMap,
        icon: icon
    });
    markers[props.content.flight_Id] = marker;
    // check info window
    if (props.content) {
        // add info window
        content: props.content;
    }
    marker.addListener('click', function () {
        inside = true;
        selectFlight(props.content.flight_Id);
    });
}
