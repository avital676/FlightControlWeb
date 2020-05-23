let deleted = false;

function allowDrop(ev) {
    $("#listsArea").hide();
    $("#dragAndDrop").show();
    ev.preventDefault();
    event.dataTransfer.setData("text/plain", event.target.id)
    document.getElementById("detailes").innerHTML = "drag";
}

function sleep(milliseconds) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < milliseconds);
}

function onDrop(ev) {
    ev.preventDefault();
    document.getElementById("detailes").innerHTML = "drropppopopo";
    //document.getElementById("dragAndDrop").style.display = "none";
    $("#listsArea").show();
    $("#dragAndDrop").hide();
    if (ev.dataTransfer.items[0].kind == 'file') {
        let file = ev.dataTransfer.items[0].getAsFile();
        let xhr = new XMLHttpRequest();
        let flighturl = "../api/FlightPlan";
        xhr.open("POST", flighturl, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(file);
        sleep(50);
        updateList();
    }
}

/**function onDragLeave(event) {
    $("#listsArea").show();
    $("#dragAndDrop").hide();
}*/

function endDrag(event) {
    $("#listsArea").show();
    $("#dragAndDrop").hide();
}

function updateList() {
    let row;
    let compId;
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z";
    $.getJSON(flighturl, function (data) {
        data.forEach(function (flight) {
            let exist = false;
            for (let i = 1; i < tableRows.length; i++) {
                row = tableRows[i];
                compId = row.cells[0].innerHTML;
                if (flight.flightId == compId) {
                    exist = true;
                    break;
                }
            }
            if (exist == false) {
                appendInternalFlight(flight);
                addMarker({
                    coords: { lat: flight.latitude, lng: flight.longitude },
                    content: flight
                });
            }
        });
    });
}

function deleteEndedFlight() {
    let row;
    let compId;
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z&sync_all";
    for (let i = 1; i < tableRows.length; i++) {
        let exist = true;
        $.getJSON(flighturl, function (data) {
            data.forEach(function (flight) {
                if (exist == false) {
                    appendInternalFlight(flight);
                    addMarker({
                        coords: { lat: flight.latitude, lng: flight.longitude },
                        content: flight
                    });
                }
            });
        });
       
    }
}

function appendInternalFlight(flight) {
    let flightId = flight.flightId;
    $("#list1").append(`<tr onclick=flightClick(event)><td id=${flightId}>${flightId}</td> 
        <td id=${flightId}>${flight.companyName}</td>
        <td id=${flightId}><button type="button" class="btn btn-outline-primary" onclick=deleteFlight(event)
            id=${flightId} data-toggle="tooltip" data-placement="top" title="delete flight">X</button>
            </td></tr>`);
}

function flightClick(ev) {
    if (deleted) {
        deleted = false;
        return;
    }
  ///  document.getElementById("detailes").innerHTML = ev.target.innerHTML;
    selectFlight(ev.target.id);
}

// Initialize flights lists:
let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:03Z&sync_all";
$.getJSON(flighturl, function (data) {
    data.forEach(function (flight) {
        let flightId = flight.flightId;
        if (!flight.isExternal) {
            // internal flight:
            appendInternalFlight(flight);
        } else {
            // external flight:
            $("#list2").append(`<tr onclick=flightClick(event)><td id=${flightId}>${flightId}</td> 
            <td id=${flightId}>${flight.companyName}</td></tr>`);
        }
        addMarker({
            coords: { lat: flight.latitude, lng: flight.longitude },
            content: flight
        });
    });
});

function deleteFlight(event) {
    deleted = true;
    let flightId = event.target.id;
    // delete from list:
    let deleteurl = `../api/Flights/${flightId}`;
    $.ajax({
        url: deleteurl,
        type: 'DELETE'
    });
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let row;
    let compId;
    for (let i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        compId = row.cells[0].innerHTML;
        if (flightId == compId) {
            document.getElementById("list1").deleteRow(i);
        }
    }
    // delete from map:
    markers[flightId].setMap(null);
    delete markers[flightId];
    // delete route and details table:
    if (selected == flightId) {
        for (i = 0; i < line.length; i++) {
            line[i].setMap(null);
        }
        document.getElementById("listD").deleteRow(1);
    }
}

let worker;
let animatedId = null;
function buttonClicked() {
    if (typeof (Worker) !== "undefined") {
        if (typeof (worker) == "undefined") {
            worker = new Worker('worker.js');
        }
        worker.onmessage = function (event) {
            updateList();
            var flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z";
            $.getJSON(flighturl, function (data) {
                data.forEach(function (flight) {
                    var myLatlng = new google.maps.LatLng(flight.latitude, flight.longitude);
                    markers[flight.flightId].setPosition(myLatlng);
                });
            });
        };
        worker.postMessage([]);
    } else {
        document.getElementById("result").innerHTML = "Sorry! No Web Worker support.";
    }
}

// Set the map on all markers in the array:
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}
