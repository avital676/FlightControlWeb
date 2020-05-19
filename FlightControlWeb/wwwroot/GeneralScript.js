let selected = null;
let flightPath;
let line = [];

function selectFlight(flightId) {
    selected = flightId;
    // color list:
    let list1 = document.getElementById("list1");
    let list2 = document.getElementById("list2");
    colorList(list1, flightId);
    colorList(list2, flightId);

    // animate plane:
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:21Z";

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

    selectFlightContinue(flightId);
}
function selectFlightContinue(flightId) {
    for (let key in markers) {
        if (key == flightId) {
            if (markers[key].getAnimation() != google.maps.Animation.BOUNCE) {
                markers[key].setAnimation(google.maps.Animation.BOUNCE);
            }
        } else {
            markers[key].setAnimation(null);
        }
    }
    // fill table:
    // delete previous details:
    if (document.getElementById("listD").rows.length > 1) {
        document.getElementById("listD").deleteRow(1);
    }
    document.getElementById("detailes").innerHTML = document.getElementById("listD").rows.length;
    let i;
    // clear existing route from map:
    for (i = 0; i < line.length; i++) {
        line[i].setMap(null);
    }
}
function colorList(list, flightId) {
    let row;
    let compId;
    let tableRows = list.getElementsByTagName('tr');
    for (let i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        compId = row.cells[0].innerHTML;
        if (flightId == compId) {
            row.style.backgroundColor = "#80BDFF";
        } else {
            row.style.backgroundColor = "";
        }
    }
}

// Drawing the Flight path 
function drawPath(flight) {
    let flightPlanCoordinates = [];
    let segments = flight.flightPlan.segments;
    let lng = flight.flightPlan.initial_Location.longitude;
    let lat = flight.flightPlan.initial_Location.latitude;
    flightPlanCoordinates.push({ lat: lat, lng: lng });
    //showLine(a, b, c, d);
    let length = flight.flightPlan.segments.length
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
    // save poline:
    line.push(flightPath);
    flightPath.setMap(googleMap);
}

function cancelClick(event) {
    if (inside != true) {
        //document.getElementById("detailes").innerHTML = "cancel"
        //delete line on map
        for (i = 0; i < line.length; i++) {
            line[i].setMap(null); //or line[i].setVisible(false);
        }
        //delete details
        if (document.getElementById("listD").rows.length > 1) {
            document.getElementById("listD").deleteRow(1);
        }
        //cancel the color row
        let row;
        let tableRows = document.getElementById("list1").getElementsByTagName('tr');
        for (let i = 1; i < tableRows.length; i++) {
            row = tableRows[i];
            row.style.backgroundColor = "";
        }
         tableRows = document.getElementById("list2").getElementsByTagName('tr');
        for (let i = 1; i < tableRows.length; i++) {
            row = tableRows[i];
            row.style.backgroundColor = "";
        }
        // un-animate plane: cancel the jump
        for (let key in markers) {
            markers[key].setAnimation(null);
        }
    }
    inside = false;
}