let selected = null;
let flightPath;
let line = [];

// TOO MUCH KINUNIM
function selectFlight(flightId) {
    selected = flightId;
    // color selected flight in list:
    let list1 = document.getElementById("list1");
    let list2 = document.getElementById("list2");
    colorList(list1, flightId);
    colorList(list2, flightId);
    // delete previous details from details table:
    if (document.getElementById("listD").rows.length > 1) {
        document.getElementById("listD").deleteRow(1);
    }
    // fill details table:
    let flighturl = `../api/FlightPlan/${ flightId }`;
    $.getJSON(flighturl)
    // json request secceeded:
    .done(function (fp) {
        let lastSegment = fp.segments[fp.segments.length - 1];
        $("#listD").append(`<tr><td>${flightId}</td>` +
            `<td>${fp.initial_Location.date_Time}</td>` +
            `<td>${fp.initial_Location.longitude.toFixed(2)},
                ${ fp.initial_Location.latitude.toFixed(2)}</td>` +
            `<td>${lastSegment.longitude.toFixed(2)},
                ${ lastSegment.latitude.toFixed(2)}</td>` +
            `<td>${fp.passengers}</td>` +
            `<td>${fp.company_Name}</td>`);

        // animate selected plane:
        animatePlane(flightId);
        let i;
        // clear existing route from map:
        for (i = 0; i < line.length; i++) {
            line[i].setMap(null);
        }
        // draw the flight path:
        drawPath(fp);
    })
    // json request failed:
    .fail(function (response) {
        showMsg(response.responseText);
    });
}

// TOO MUCH KINUNIM
// Animate the plane of the given id:
function animatePlane(flightId) {
    for (let key in markers) {
        if (key === flightId) {
            if (markers[key].getAnimation() !== google.maps.Animation.BOUNCE) {
                markers[key].setAnimation(google.maps.Animation.BOUNCE);
            }
        } else {
            markers[key].setAnimation(null);
        }
    }
}

function colorList(list, flightId) {
    let row;
    let compId;
    let tableRows = list.getElementsByTagName('tr');
    for (let i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        compId = row.cells[0].innerHTML;
        if (flightId === compId) {
            row.style.backgroundColor = "#80BDFF";
        } else {
            row.style.backgroundColor = "";
        }
    }
}

// Draw the flight path 
function drawPath(fp) {
    let flightPlanCoordinates = [];
    let segments = fp.segments;
    let lng = fp.initial_Location.longitude;
    let lat = fp.initial_Location.latitude;
    flightPlanCoordinates.push({ lat: lat, lng: lng });
    let length = fp.segments.length;
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
    if (inside !== true) {
        //delete line on map
        for (i = 0; i < line.length; i++) {
            line[i].setMap(null);
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

function showMsg(msg) {
    $("response-alert").show();
    document.getElementById("msg").innerHTML = msg;
    $("#response-alert").fadeTo(10000, 500).slideUp(500, function () {
        $("#response-alert").slideUp(500);
    });
}