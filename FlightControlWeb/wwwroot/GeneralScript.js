﻿var flightPath;
//lost of all line on map
var line = [];
function selectFlight(flightId) {
    // color list:
    var list1 = document.getElementById("list1");
    var list2 = document.getElementById("list2");
    colorList(list1, flightId);
    colorList(list2, flightId);

    // animate plane:
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].id == flightId) {
            if (markers[i].getAnimation() != google.maps.Animation.BOUNCE) {
                markers[i].setAnimation(google.maps.Animation.BOUNCE);
            }
        } else {
            markers[i].setAnimation(null);
        }
    }

    // fill table:
    // delete previous details:
    if (document.getElementById("listD").rows.length > 1) {
        document.getElementById("listD").deleteRow(1);
    }
    document.getElementById("detailes").innerHTML = document.getElementById("listD").rows.length;
    var flighturl = "../api/Flights";
    // add details:
    $.getJSON(flighturl, function (data) {
        data.forEach(function (flight) {
            if (flight.flightId == flightId) {
                $("#listD").append("<tr><td>" + flight.flightId + "</td>" +
                    "<td>" + flight.longitude + "</td>" +
                    "<td>" + flight.latitude + "</td>" +
                    "<td>" + flight.passengers + "</td>" +
                    "<td>" + flight.companyName + "</td>" +
                    "<td>" + flight.dateTimee + "</tr></td>");
                drawPath(flight);
            }
        });
    });
}

function colorList(list, flightId) {
    var row;
    var compId;
    var tableRows = list.getElementsByTagName('tr');
    for (var i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        compId = row.cells[0].innerHTML;
        if (flightId == compId) {
            row.style.backgroundColor = "#80BDFF";
        } else {
            row.style.backgroundColor = "";
        }
    }
}

//Drawing the Flight path 

function drawPath(flight) {
    var flightPlanCoordinates = [];
    var segments = flight.flightPlan.segments;
    var lng = flight.flightPlan.initial_Location.longitude;
    var lat = flight.flightPlan.initial_Location.latitude;
    flightPlanCoordinates.push({ lat: lat, lng: lng });
    //showLine(a, b, c, d);
    var length = flight.flightPlan.segments.length
    var i;
    for (i = 0; i < length; i++) {
        lng = segments[i].longitude;
        lat = segments[i].latitude;
        flightPlanCoordinates.push({ lat: lat, lng: lng });
    }

     flightPath = new google.maps.Polyline({
        path: flightPlanCoordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
     });
    //save all the poline - to delete them later;
    line.push(flightPath);
    flightPath.setMap(googleMap);
}
function cancelClick(event) {

    document.getElementById("detailes").innerHTML = "cancel"
    //delete line on map
    for (i = 0; i < line.length; i++) {
        line[i].setMap(null); //or line[i].setVisible(false);
    }
    //delete details
    if (document.getElementById("listD").rows.length > 1) {
        document.getElementById("listD").deleteRow(1);
    }
    //cancel the color row
    var row;
    var tableRows = document.getElementById("list1").getElementsByTagName('tr');
    for (var i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        row.style.backgroundColor = "";

    }
    var tableRows = document.getElementById("list2").getElementsByTagName('tr');
    for (var i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        row.style.backgroundColor = "";

    }

    // animate plane: cancel the jump
    for (var i = 0; i < markers.length; i++) {
            markers[i].setAnimation(null);
        
    }
}