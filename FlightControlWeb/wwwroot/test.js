function myFunction() {
    document.getElementById("demo").innerHTML = "Paragraph changed.";
    var json = JavaScriptSerializer.Serialize(Flight);
    $.getJSON('Flight.json', function (Flight) {
        console.log('Flight', Flight);
    });
}