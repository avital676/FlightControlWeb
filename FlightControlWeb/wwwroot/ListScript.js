window.onload = function () {
    this.initFlightsLists();
    sleep(100);
    this.asyncUpdates();
};

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

// TOO MUCH KINUNIM
function updateList() {
    let row;
    let compId;
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z";
    $.getJSON(flighturl)
        .done(function (flights) {
            flights.forEach(function (flight) {
                let exist = false;
                for (let i = 1; i < tableRows.length; i++) {
                    row = tableRows[i];
                    compId = row.cells[0].innerHTML;
                    if (flight.flight_Id == compId) {
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
        })
        .fail(function (response) {
            showMsg(response.responseText);
        });
}

function deleteEndedFlight() {
    let list = document.getElementById("list1");
    let tableRows = list.getElementsByTagName('tr');
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z&sync_all";
    for (let i = 1; i < tableRows.length; i++) {
        let exist = true;
        $.getJSON(flighturl)
            .done(function (data) {
                data.forEach(function (flight) {
                    if (exist == false) {
                        appendInternalFlight(flight);
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

function appendInternalFlight(flight) {
    let flightId = flight.flight_Id;
    $("#list1").append(`<tr onclick=flightClick(event)><td id=${flightId}>${flightId}</td> 
        <td id=${flightId}>${flight.company_Name}</td>
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
function initFlightsLists() {
    let flighturl = "../api/Flights?relative_to=2020-12-26T23:56:03Z&sync_all";
    $.getJSON(flighturl)
        .done(function (data) {
            data.forEach(function (flight) {
                let flightId = flight.flight_Id;
                if (!flight.isExternal) {
                    // internal flight:
                    appendInternalFlight(flight);
                } else {
                    // external flight:
                    $("#list2").append(`<tr onclick=flightClick(event)><td id=${flightId}>${flightId}</td> 
                <td id=${flightId}>${flight.company_Name}</td></tr>`);
                }
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
    // hide message alert:
    $("success-alert").hide();
}

function deleteFlight(event) {
    deleted = true;
    let flightId = event.target.id;
    // delete from list:
    let deleteurl = `../api/Flights/${flightId}`;
    $.ajax({
        url: deleteurl,
        type: 'DELETE',
        success: function (data, textStatus) {
            alert('request successful');
        },
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

async function asyncUpdates() {
    while (true) {
        await updateMarkers();
    }
}

function updateMarkers() {
    return new Promise(function (resolve, reject) {
        // Usually, this would be an IO operation like an HTTP request   // ??????? what is this
        var flighturl = "../api/Flights?relative_to=2020-12-26T23:56:" + time + "Z";
        $.getJSON(flighturl)
            .done(function (data) {
                data.forEach(function (flight) {
                    var myLatlng = new google.maps.LatLng(flight.latitude, flight.longitude);
                    markers[flight.flight_Id].setPosition(myLatlng);
                    //markers[flight.flight_Id]
                });
            })
            .fail(function () {
                showMsg("Couldn't update markers on map");
            });
        setTimeout(() => resolve("Success"), 2000);
    });
}
