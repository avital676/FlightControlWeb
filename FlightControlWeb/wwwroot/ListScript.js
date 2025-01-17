﻿// Global variables
let deleted = false;
let exist;

// Allow dropping files in dragAndDrop area
function allowDrop(ev) {
  if ((ev.target.id == 'listsArea') || (ev.target.id == 'dragAndDrop')) {
    $('#listsArea').hide();
    $('#dragAndDrop').show();
    ev.preventDefault();
    ev.dataTransfer.setData('text/plain', ev.target.id);
  }
}

// Sleep for a given amount of ms
function sleep(milliseconds) {
  const date = Date.now();
  let currentDate = null;
  do {
    currentDate = Date.now();
  } while (currentDate - date < milliseconds);
}

// Send dropped file to server
function onDrop(ev) {
  ev.preventDefault();
  $('#listsArea').show();
  $('#dragAndDrop').hide();
  if (ev.dataTransfer.items[0].kind === 'file') {
    let file = ev.dataTransfer.items[0].getAsFile();
    let xhr = new XMLHttpRequest();
    const flighturl = '../api/FlightPlan';
    xhr.open('POST', flighturl, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(file);
    xhr.onerror = function () { // the request failed
      showMsg('Failed sending file to server');
    };
    xhr.onload = function() {
      handleRes(xhr);
    };
    sleep(50);
  }
}

// Handle response code from post request
function handleRes(xhr) {
  if (xhr.readyState == xhr.DONE) {
    if (xhr.status == 200) {
      // succeeded:
      showMsg('FlightPlan added');
    } else {
      // failed:
      showMsg(`Couldn't add FlightPlan`);
    }
  }
}

// Show list after drag
function onDragLeave(event) {
  if (event.target.id != 'listsArea') {
    $('#listsArea').show();
    $('#dragAndDrop').hide();
  }
}

// End of drag
function endDrag(event) {
  if (event.target.id != 'listsArea') {
    $('#listsArea').show();
    $('#dragAndDrop').hide();
  }
}

// Update flights lists
function updateList() {
  return new Promise(function (resolve, reject) {
    let date = new Date().toISOString();
    const flighturl = `../api/Flights?relative_to=${date}&sync_all`;
    $.getJSON(flighturl)
        .done(function(flights) {
          addFlightsFromServer(flights);
        })
        .fail(function(response) {
            showMsg(`Couldn't get Flights list`);
        });
    setTimeout(() => resolve('Success'), 0);
  });
}

// Add flights that aren't on list,map
function addFlightsFromServer(flights) {
  for (const flight of flights) {
    if (!checkIfFlightExist(flight.flightId)) {
      appendFlight(flight);
      addMarker({
        coords: {lat: flight.latitude, lng: flight.longitude},
        content: flight,
      });
    }
  }
}

// Check flight exsistance
function checkIfFlightExist(flightId) {
  if (markers[flightId] != null) {
    return true;
  }
  return false;
}

// Delete flights that ended
function deleteEndedFlight() {
  return new Promise(function(resolve, reject) {
    let list = document.getElementById('list1');
    let tableRows = list.getElementsByTagName('tr');
    let row;
    let id;
    for (let i = 1; i < tableRows.length; i++) {
      row = tableRows[i];
      id = row.cells[0].innerHTML;
      checkExistInServer(id);
    }
    setTimeout(() => resolve('Success'), 0);
  });
}

// Check if flight exists in server and delete it if ended
function checkExistInServer(id) {
  let date = new Date().toISOString();
  const flighturl = `../api/Flights?relative_to=${date}&sync_all`;
  exist = false;
  $.getJSON(flighturl)
      .done(function(data) {
        data.forEach(function(flight) {
          if (id == flight.flightId) {
            exist = true;
          }
        });
        if (exist == false) {
          deleteFlight(id, 'list1');
        }
      })
      .fail(function (response) {
          showMsg(`Couldn't get flights from server`);
      });
}

// Add flight to list
function appendFlight(flight) {
  const flightId = flight.flightId;
  if (flight.isExternal === false) {
    $('#list1').append(`<tr id=${flightId} onclick=flightClick(event)>
      <td id=${flightId}>${flightId}</td>
      <td id=${flightId}>${flight.companyName}</td>
      <td id=${flightId}><button type="button" 
        class="btn btn-outline-primary" onclick=deleteFlightEvent(event)
        id=${flightId} data-toggle="tooltip" data-placement="top" 
        title="delete flight">X</button>
      </td></tr>`);
  } else {
    $('#list2').append(`<tr onclick=flightClick(event)>
      <td id=${flightId}>${flightId}</td> 
      <td id=${flightId}>${flight.companyName}</td></tr>`);
  }
}

// Handle flight click
function flightClick(ev) {
  if (deleted) {
    deleted = false;
    return;
  }
  selectFlight(ev.target.id);
}

// Initialize flights lists
function initFlightsLists() {
  let date = new Date().toISOString();
  const flighturl = `../api/Flights?relative_to=${date}&sync_all`;
  $.getJSON(flighturl)
      .done(function(data) {
        data.forEach(function(flight) {
          appendFlight(flight);
          addMarker({
            coords: {lat: flight.latitude, lng: flight.longitude},
            content: flight,
          });
        });
      })
      .fail(function(response) {
        showMsg(`Couldn't initialize flights lists`);
      });
}

// Delete flight request event
function deleteFlightEvent(event) {
  deleted = true;
  const flightId = event.target.id;
  const deleteurl = `../api/Flights/${flightId}`;
  $.ajax({
    url: deleteurl,
    type: 'DELETE',
    error: function(response) {
        showMsg(`Couldn't delete Flight`);
      return;
    },
    success: function(response) {
      showMsg('Flight deleted');
    },
  });
  deleteFlight(flightId, 'list1');
}

// Delete flight from list and map
function deleteFlight(flightId, listId) {
  // delete from list:
  let rowToDelete = document.getElementById(flightId);
  rowToDelete.parentNode.removeChild(rowToDelete);
  // delete from map:
  markers[flightId].setMap(null);
  delete markers[flightId];
  // delete route and details table:
  if (selected === flightId) {
    for (i = 0; i < line.length; i++) {
      line[i].setMap(null);
    }
    document.getElementById('listD').deleteRow(1);
  }
}
