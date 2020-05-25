// Global variables
let deleted = false;

// Allow dropping files in dragAndDrop area
function allowDrop(ev) {
    $("#listsArea").hide();
    $("#dragAndDrop").show();
    ev.preventDefault();
    event.dataTransfer.setData("text/plain", event.target.id);
}

// Sleep for a given amount of ms
function sleep(milliseconds) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < milliseconds);
}

// TOO MUCH KINUNIM :(
// Send dropped file to server
function onDrop(ev) {
    ev.preventDefault();
    $("#listsArea").show();
    $("#dragAndDrop").hide();
    if (ev.dataTransfer.items[0].kind === 'file') {
        let file = ev.dataTransfer.items[0].getAsFile();
        let xhr = new XMLHttpRequest();
        let flighturl = "../api/FlightPlan";
        xhr.open("POST", flighturl, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(file);
        xhr.onerror = function () { //the request failed
            showMsg("failed sending file to server");
        };
        xhr.onload = function () {
            if (xhr.readyState === xhr.DONE) {
                if (xhr.status === 200) {
                    // succeeded:
                    showMsg(xhr.responseText);
                } else {
                    // failed:
                    showMsg("Couldn't add FlightPlan");
                }
            }
        };
        sleep(50);
    }
}


// Act in accordance to post FlightPlan request from client:
function loadPost(xhr) {
    if (xhr.readyState === xhr.DONE) {
        if (xhr.status === 200) {
            // succeeded:
            showMsg(xhr.responseText);
        } else {
            // failed:
            showMsg("Couldn't add FlightPlan");
        }
    }
}

/*
// Show list after drag
function onDragLeave(event) {
    if (event.target.id != "listsArea") {
        $("#listsArea").show();
        $("#dragAndDrop").hide();
    }
}*/

function endDrag(event) {
    if (event.target.id != "listsArea") {
        $("#listsArea").show();
        $("#dragAndDrop").hide();
    }
}

// TOO MUCH KINUNIM ///////////////////////////////////////////////
// Update flights lists
function updateList() {
    return new Promise(function (resolve, reject) {
        let row;
        let compId;
        let list = document.getElementById("list1");
        let tableRows = list.getElementsByTagName('tr');
        let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z&sync_all";
        $.getJSON(flighturl)
        .done(function (flights) {
            flights.forEach(function (flight) {
                if (!checkIfFlightExist(flight.flightId)) {
                    appendFlight(flight);
                    addMarker({
                        coords: { lat: flight.latitude, lng: flight.longitude },
                        content: flight
                    });
                }
            });
        })
        .fail(function (response) {
            showMsg(response.responseText);
        });
        setTimeout(() => resolve("Success"), 1000);
    });
}

// Check flight exsistance
function checkIfFlightExist(flightId) {
    if (markers[flightId] != null) {
        return true;
    }
    return false;
}


// TOO MUCH KINUNIM //////////////////////////////////////////////
// Delete flights that ended
function deleteEndedFlight() {
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z&sync_all";
    for (let i = 1; i < tableRows.length; i++) {
        let exist = true;
        $.getJSON(flighturl)
        .done(function (data) {
            data.forEach(function (flight) {
                if (exist === false) {
                    appendFlight(flight);
                    addMarker({
                        coords: { lat: flight.latitude, lng: flight.longitude },
                        content: flight
                    });
                }
            });
        })
        .fail(function (response) {
            // failed:
            showMsg("Failed deleting ended flights");
        });
    }
}

function appendFlight(flight) {
    let flightId = flight.flightId;
    if (flight.isExternal === false) {
        $("#list1").append(`<tr onclick=flightClick(event)><td id=${flightId}>${flightId}</td> 
        <td id=${flightId}>${flight.companyName}</td>
        <td id=${flightId}><button type="button" class="btn btn-outline-primary" onclick=deleteFlight(event)
            id=${flightId} data-toggle="tooltip" data-placement="top" title="delete flight">X</button>
            </td></tr>`);
    } else {
        $("#list2").append(`<tr onclick=flightClick(event)><td id=${flightId}>${flightId}</td> 
                <td id=${flightId}>${flight.companyName}</td></tr>`);
    }
}

function flightClick(ev) {
    if (deleted) {
        deleted = false;
        return;
    }
    selectFlight(ev.target.id);
}

// TOO MUCH KINUNIM
// Initialize flights lists:
function initFlightsLists() {
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:03Z&sync_all";
    $.getJSON(flighturl)
    .done(function (data) {
        data.forEach(function (flight) {
            appendFlight(flight);
            addMarker({
                coords: { lat: flight.latitude, lng: flight.longitude },
                content: flight
            });
        });
    })
    .fail(function (response) {
        // code response from controller:
        showMsg(response.responseText);
    });  
}

function deleteFlight(event) {
    deleted = true;
    let flightId = event.target.id;
    // delete from list:
    let deleteurl = `../api/Flights/${flightId}`;
    $.ajax({
        url: deleteurl,
        type: 'DELETE',
        error: function (response) {
            showMsg(response.responseText);
            return;
        }});
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let row;
    let compId;
    for (let i = 1; i < tableRows.length; i++) {
        row = tableRows[i];
        compId = row.cells[0].innerHTML;
        if (flightId === compId) {
            document.getElementById("list1").deleteRow(i);
        }
    }
    // delete from map:
    markers[flightId].setMap(null);
    delete markers[flightId];
    // delete route and details table:
    if (selected === flightId) {
        for (i = 0; i < line.length; i++) {
            line[i].setMap(null);
        }
        document.getElementById("listD").deleteRow(1);
    }
}

async function asyncUpdates() {
    while (running) {
        await updateMarkers();
        await updateList();
    }
}

// TOO MUCH KINUNIM
function updateMarkers() {
    return new Promise(function (resolve, reject) {
        var flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z&async_all";
        $.getJSON(flighturl)
        .done(function (data) {
            data.forEach(function (flight) {
                var myLatlng = new google.maps.LatLng(flight.latitude, flight.longitude);
                markers[flight.flightId].setPosition(myLatlng);
            });
        })
        .fail(function () {
            showMsg("Couldn't update markers on map");
        });
        setTimeout(() => resolve("Success"), 2000);
    });
}


