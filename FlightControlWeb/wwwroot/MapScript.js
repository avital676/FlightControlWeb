// Global variables
let googleMap;
let markers = {};
let inside = false;

// Initialize map
function initMap() {
  // map options
  let options = {
    zoom: 2,
    center: {lat: 32.00683, lng: 34.88533},
    mapTypeId: 'terrain',
    id: 'googleMap',
  };
  // creat new map
  googleMap = new google.maps.Map(document.getElementById('map'), options);
}

// Add markers to map
function addMarker(props) {
  let icon = {
    url: 'plane-icon.png', // url
    scaledSize: new google.maps.Size(20, 20), // scaled size
    origin: new google.maps.Point(0, 0), // origin
    anchor: new google.maps.Point(0, 0), // anchor
  };
  let marker = new google.maps.Marker({
    position: props.coords,
    map: googleMap,
    icon: icon,
  });
  markers[props.content.flightId] = marker;
  // check info window
  if (props.content) {
    // add info window
    content: props.content;
  }
  // click on marker function:
  marker.addListener('click', function() {
    inside = true;
    selectFlight(props.content.flightId);
  });
}

// Update markers position on map
function updateMarkers() {
  return new Promise(function (resolve, reject) {
    const flighturl = '../api/Flights?relative_to=2020-12-26T23:56:' +
      time + 'Z&async_all';
    $.getJSON(flighturl)
        .done(function(data) {
          moveMarkers(data);
        })
        .fail(function () {
          showMsg(`Couldn't update markers on map`);
        });
    setTimeout(() => resolve('Success'), 2000);
  });
}

// Move marker on map
function moveMarkers(flights) {
  for (const flight of flights) {
    const pos = new google.maps.LatLng(flight.latitude, flight.longitude);
    markers[flight.flightId].setPosition(pos);
  }
}
