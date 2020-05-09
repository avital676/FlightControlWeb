function allowDrop(ev) {
    $("#listsArea").hide();
    $("#dragAndDrop").show();
    ev.preventDefault();
    event.dataTransfer.setData("text/plain", event.target.id)
    document.getElementById("detailes").innerHTML = "drag";
}

function onDrop(ev) {
    ev.preventDefault();
    document.getElementById("detailes").innerHTML = "drropppopopo";
    //document.getElementById("dragAndDrop").style.display = "none";
    $("#listsArea").show();
    $("#dragAndDrop").hide();
    if (ev.dataTransfer.items[0].kind === 'file') {
        var file = ev.dataTransfer.items[0].getAsFile();
        var xhr = new XMLHttpRequest();
        var flighturl = "../api/FlightPlan";
        xhr.open("POST", flighturl, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(file);
        //$.post("../api/FlightPlan", file);
        var flighturl = "../api/Flights";
        $.getJSON(flighturl, function (data) {
            data.forEach(function (flight) {
                $("#list1").append("<tr onclick=flightClick(event)><td>" + flight.flightId + "</td>" +
                    "<td>" + flight.companyName + "</td></tr>");
                $("#list2").append("<tr onclick=flightClick(event)><td>" + flight.flightId + "</td>" +
                    "<td>" + flight.companyName + "</td></tr>");
                addMarker({
                    coords: { lat: flight.latitude, lng: flight.longitude },
                    content: flight
                });
            });
        });
    }
}



function flightClick(ev) {
    document.getElementById("detailes").innerHTML = ev.target.innerHTML;
    selectFlight(ev.target.innerHTML);
}