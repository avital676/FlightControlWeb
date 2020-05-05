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