function allowDrop(ev) {
    $("#listsArea").hide();
    $("#dragAndDrop").show();
    ev.preventDefault();
    event.dataTransfer.setData("text/plain", event.target.id)
    document.getElementById("detailes").innerHTML = "drag";
}

function onDrop(ev) {
    document.getElementById("detailes").innerHTML = "drropppopopo";
    document.getElementById("dragAndDrop").style.display = "none"
    $("#listsArea").show();
    ev.preventDefault();
    if (ev.dataTransfer.items[0].kind === 'file') {
        var file = ev.dataTransfer.items[0].getAsFile();
        document.getElementById("detailes").innerHTML = file.name;
        var flighturl = "../api/FlightPlan";
        $.ajax({
            url: flighturl,
            type: 'POST',
            // dataType: 'json',
            dsta: file[0]
        });
        document.getElementById("detailes").innerHTML = "after";


        //  $.post("../api/FlightPlan", file);
    }
    //not here
    /*   var flighturl = "../api/Flights";
       $.getJSON(flighturl, function (data) {
           data.forEach(function (flight) {
               $("#list2").append("<tr><td>" + flight.company_name + "</td>" +
                   "<td>" + flight.date_time + "</tr></td>");
           });
       });*/
    //document.getElementById("detailes").innerHTML = "afterrrrrrrrrrrrrrrrrrrrrrrrr";


    // var fileurl = "../api/FlightPlan";
    // $.getJSON(fileurl)
}

function flightClick(ev) {
    document.getElementById("detailes").innerHTML = ev.target.innerHTML;
    selectFlight(ev.target.innerHTML);
}
/*
    // get selected row and tables:
    var rowNum = ev.target.parentNode.rowIndex;
    var fList = ev.target.offsetParent.attributes.id.nodeValue;
    var rowsNotSelected;
    var table = document.getElementById(fList);
    var list1 = document.getElementById("list1");
    var list2 = document.getElementById("list2");
    // uncolor all rows in both tables:
    rowsNotSelected = list1.getElementsByTagName('tr');
    for (var row = 0; row < rowsNotSelected.length; row++) {
        rowsNotSelected[row].style.backgroundColor = "";
    }
    rowsNotSelected = list2.getElementsByTagName('tr');
    for (var row = 0; row < rowsNotSelected.length; row++) {
        rowsNotSelected[row].style.backgroundColor = "";
    }
    // color selected row:
    var rowSelected = table.getElementsByTagName('tr')[rowNum];
    rowSelected.style.backgroundColor = "yellow";
    */

//    // delete previous details:
//    if (document.getElementById("listD").rows.length > 1) {
//        document.getElementById("listD").deleteRow(1);
//    }
//    document.getElementById("detailes").innerHTML = document.getElementById("listD").rows.length;
//    var flighturl = "../api/Flights";
//    // add details:
//    $.getJSON(flighturl, function (data) {
//        data.forEach(function (flight) {
//            if (flight.flight_id == ev.target.innerHTML) {
//                $("#listD").append("<tr><td>" + flight.FlightId + "</td>" +
//                    "<td>" + flight.longitude + "</td>" +
//                    "<td>" + flight.latitude + "</td>" +
//                    "<td>" + flight.passengers + "</td>" +
//                    "<td>" + flight.companyName + "</td>" +
//                    "<td>" + flight.dateTime + "</tr></td>");
//            }
//        });
//    });
//}
