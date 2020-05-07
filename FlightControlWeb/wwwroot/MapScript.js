var googleMap;
function initMap() {
    //map options
    var options = {
        zoom: 2,
        center: { lat: 42.3601, lng: -70.0589 }
    }
    $("#dragAndDrop").hide();
    //creating new map
    googleMap = new google.maps.Map(document.getElementById('map'), options);


    var a = [41.4668, 153.027];
    addMarker({
        coords: a,
        content: '<h1>  flight num 1 </h1>'
    })

    addMarker({
        coords: { lat: 42.4668, lng: -70.9495 },
        content: '<h1>  flight num 1 </h1>'
    });
    var a = [41.4668, 153.027];
    showLine(37, -122, 41.4668, 153.027);
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
        icon: icon
    });
    
    //check info window
    if (props.content) {
        //add info window
        content: props.content
    };
    marker.addListener('click', function () {
        var x = document.getElementById("detailes");
        if (x.style.display === "none") {
        } else {
            x.innerHTML = props.content;
        }

        // document.getElementById("FlightDetails").style.display = "none";
        //  infoWindow.open(map, marker);
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