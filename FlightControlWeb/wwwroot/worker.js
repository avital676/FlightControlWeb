var i = 0;
function update() {
    postMessage(i);
    setTimeout("update()", 5000);
}

update();