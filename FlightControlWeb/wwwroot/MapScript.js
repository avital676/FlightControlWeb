﻿var googleMap;
function initMap() {
    //map options
    var options = {
        zoom: 2,
        center: { lat: 42.3601, lng: -70.0589 }
    }
    $("#dragAndDrop").hide();
    //creating new map
    googleMap = new google.maps.Map(document.getElementById('map'), options);
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
            x.innerHTML = "Company Name:" + props.content.companyName;
            $("#listD").append("<tr><td>" + props.content.flightId + "</td>" +
                "<td>" + props.content.longitude + "</td>" +
                "<td>" + props.content.latitude + "</td>" +
                "<td>" + props.content.passengers + "</td>" +
                "<td>" + props.content.companyName + "</td>" +
                "<td>" + props.content.dateTime + "</tr></td>");
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