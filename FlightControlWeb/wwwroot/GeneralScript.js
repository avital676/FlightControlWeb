function selectFlight(flightId) {
    // color list:
    var list1 = document.getElementById("list1");
    var list2 = document.getElementById("list2");
    var tRows;
    var r;
    var fId;
    tRows = list1.getElementsByTagName('tr');
    for (var row = 1; row < tRows.length; row++) {
        r = tRows[row];
        fId = r.cells[0].innerHTML;
        if (flightId == fId) {
            r.style.backgroundColor = "yellow";
        } else {
            r.style.backgroundColor = "";
        }
    }
    tRows = list2.getElementsByTagName('tr');
    for (var row = 1; row < tRows.length; row++) {
        r = tRows[row];
        fId = r.cells[0].innerHTML;
        if (flightId == fId) {
            r.style.backgroundColor = "yellow";
        } else {
            r.style.backgroundColor = "";
        }
    }

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
        
     // draw line on map:
     var flighturl = "../api/Flights";
     $.getJSON(flighturl, function (data) {
         data.forEach(function (flight) {
             if (flight.flightId == flightId) {
                 var segments = flight.flightPlan.segments;
                 var length = flight.flightPlan.segments.length
                 var i;
                 for (i = 0; i < length - 1; i++) {
                     var a = segments[i].longitude
                     var b = segments[i].latitude
                     var c = segments[i + 1].longitude
                     var d = segments[i + 1].latitude
                     showLine(a, b, c, d)
                 }
             }
         });
     });

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
                    "<td>" + flight.dateTime + "</tr></td>");
            }
        });
    });
}