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
    $("#dragAndDrop").hide();
    if (ev.dataTransfer.items[0].kind === 'file') {
        var file = ev.dataTransfer.items[0].getAsFile();
        document.getElementById("detailes").innerHTML = file.name;
        var flighturl = "../api/FlightPlan";
        $.ajax({
            url: flighturl,
            type: 'POST',
            dataType: 'json',
            data: file
        });
         //$.post("../api/FlightPlan", file);
    }
}

function flightClick(ev) {
    document.getElementById("detailes").innerHTML = ev.target.innerHTML;
    selectFlight(ev.target.innerHTML);
}
