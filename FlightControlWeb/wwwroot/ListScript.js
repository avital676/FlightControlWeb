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
    document.getElementById("detailes").innerHTML = "afterrrrrrrrrrrrrrrrrrrrrrrrr";


       // var fileurl = "../api/FlightPlan";
       // $.getJSON(fileurl)
}

function f() {
    Document.getElementById("detailes").innerHTML = "f f f f f f f f";
    var flighturl = "../api/Flights";
    $.getJSON(flighturl, function (data) {
        data.forEach(function (flight) {
            $("#list1").append("<tr><td>" + flight.company_name + "</td>" +
                "<td>" + flight.company_name + "</tr></td>");
        });
    });
}