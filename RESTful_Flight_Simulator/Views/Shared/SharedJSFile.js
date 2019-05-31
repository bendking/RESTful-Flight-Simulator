function drawCircle(x,y,canvasName) {
    var canvas = document.getElementById(canvasName);
    var context = canvas.getContext("2d");
    context.arc(x, y, 1, 0, 2 * Math.PI, false);
    context.fillStyle = "#FF0000";
    context.fill();

    /*
 * for borders
    context.lineWidth = 0.3;
    context.strokeStyle = '#000000';
    context.stroke();
    */
}

