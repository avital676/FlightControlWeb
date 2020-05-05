function initMap() {
    //map options
    var options = {
        zoom: 2,
        center: { lat: 42.3601, lng: -70.0589 }
    }
    //creating new map
    var map = new google.maps.Map(document.getElementById('map'), options);
    addMarker({
        coords: { lat: 42.4668, lng: -70.9495 },
        content: '<h1>  flight num 1 </h1>'
    });


    function addMarker(props) {
        var marker = new google.maps.Marker({
            position: props.coords,
            map: map,
            icon: 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png'
        });
        //check info window
        if (props.content) {
            //add info window
            var infoWindow = new google.maps.InfoWindow({
                content: props.content
            });
            marker.addListener('click', function () {
                var x = document.getElementById("detailes");
                if (x.style.display === "none") {
                } else {
                    x.innerHTML = props.content;
                }

                // document.getElementById("FlightDetails").style.display = "none";
                //  infoWindow.open(map, marker);
            });
        }
    }

}


