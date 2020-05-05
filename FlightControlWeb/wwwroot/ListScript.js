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