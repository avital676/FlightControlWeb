var i = 0;
var tens = 0;
var ones = 1;
var time;
function update() {
    makeTime();
    postMessage(time);
    setTimeout("update()", 1000);
    ones++;
}

function makeTime() {
    if (ones % 10 == 0) {
        ones = 0;
        tens++;
    }
    time = tens + "" + ones;
}
update();
