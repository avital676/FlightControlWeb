// Global variables
let selected = null;
let flightPath;
let line = [];
let running;

// Load window
window.onload = function load() {
  $('#dragAndDrop').hide();
  running = true;
  this.showMsg(`WELCOME! 
    Add flights by dragging json FlightPlan files to list,
    or display flight details by clicking it!`);
  this.initFlightsLists();
  sleep(100);
  this.deleteEndedFlight();
  this.asyncUpdates();
};

// Close window
window.onclose = function close() {
  running = false;
};

// Select flight with given id on list and map
function selectFlight(flightId) {
  selected = flightId;
  // color selected flight in list:
  const list1 = document.getElementById('list1');
  const list2 = document.getElementById('list2');
  colorList(list1, flightId);
  colorList(list2, flightId);
  // delete previous details from details table:
  if (document.getElementById('listD').rows.length > 1) {
    document.getElementById('listD').deleteRow(1);
  }
  // fill details table:
  const flighturl = `../api/FlightPlan/${flightId}`;
  $.getJSON(flighturl)
      // json request secceeded:
      .done(function(fp) {
        const lastSegment = fp.segments[fp.segments.length - 1];
        $('#listD').append(`<tr><td>${flightId}</td>` +
        `<td>${fp.initial_location.date_time}</td>` +
        `<td>${fp.initial_location.longitude.toFixed(2)},
          ${ fp.initial_location.latitude.toFixed(2)}</td>` +
        `<td>${lastSegment.longitude.toFixed(2)},
          ${ lastSegment.latitude.toFixed(2)}</td>` +
        `<td>${fp.passengers}</td>` +
        `<td>${fp.company_name}</td>`);
        // animate selected plane:
        animatePlane(flightId);
        let i;
        // clear existing route from map:
        for (i = 0; i < line.length; i++) {
          line[i].setMap(null);
        }
        // draw the flight path:
        drawPath(fp);
      })
      // json request failed:
      .fail(function(response) {
        showMsg(response.responseText);
      });
}

// Animate the plane of the given id
function animatePlane(flightId) {
  for (let key in markers) {
    if (key == flightId) {
      markers[key].setAnimation(google.maps.Animation.BOUNCE);
    } else {
      markers[key].setAnimation(null);
    }
  }
}

// Color selected flight on flights list
function colorList(list, flightId) {
  let row;
  let compId;
  let tableRows = list.getElementsByTagName('tr');
  for (let i = 1; i < tableRows.length; i++) {
    row = tableRows[i];
    compId = row.cells[0].innerHTML;
    if (flightId === compId) {
      row.style.backgroundColor = '#80BDFF';
    } else {
      row.style.backgroundColor = '';
    }
  }
}

// Draw the flight route:
function drawPath(fp) {
  let flightPlanCoordinates = [];
  const segments = fp.segments;
  let lng = fp.initial_location.longitude;
  let lat = fp.initial_location.latitude;
  flightPlanCoordinates.push({lat: lat, lng: lng});
  const length = fp.segments.length;
  let i;
  for (i = 0; i < length; i++) {
    lng = segments[i].longitude;
    lat = segments[i].latitude;
    flightPlanCoordinates.push({lat: lat, lng: lng});
  }
  flightPath = new google.maps.Polyline({
    path: flightPlanCoordinates,
    geodesic: true,
    strokeColor: '#FF0000',
    strokeOpacity: 1.0,
    strokeWeight: 2,
  });
  // save poline:
  line.push(flightPath);
  flightPath.setMap(googleMap);
}

// Cancle selction of flight on list and map
function cancelClick(event) {
  if (inside !== true) {
    // delete line on map
    for (i = 0; i < line.length; i++) {
      line[i].setMap(null);
    }
    // delete details from details table:
    if (document.getElementById('listD').rows.length > 1) {
      document.getElementById('listD').deleteRow(1);
    }
    // cancel the colored row
    let row;
    let tableRows = document.getElementById('list1').getElementsByTagName('tr');
    for (let i = 1; i < tableRows.length; i++) {
      row = tableRows[i];
      row.style.backgroundColor = '';
    }
    tableRows = document.getElementById('list2').getElementsByTagName('tr');
    for (let i = 1; i < tableRows.length; i++) {
      row = tableRows[i];
      row.style.backgroundColor = '';
    }
    // un-animate plane: cancel the jump
    for (let key in markers) {
      markers[key].setAnimation(null);
    }
  }
  inside = false;
}

// Show alerts/messages
function showMsg(msg) {
  $('response-alert').show();
  document.getElementById('msg').innerHTML = msg;
  $('#response-alert').fadeTo(10000, 500).slideUp(500, function() {
    $('#response-alert').slideUp(500);
  });
}

// Upadte markers and list async
async function asyncUpdates() {
  while (running) {
    await updateList();
    await updateMarkers();
    await deleteEndedFlight();
  }
}
