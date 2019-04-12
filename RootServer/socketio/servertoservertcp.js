module.exports = function (socket) {
    console.log("Connect " + socket.remoteAddress + " : " + socket.remotePort)
    socket.setEncoding('binary')
    socket.write("Nihao,We've connected Success!");
    socket.on('close', function (data) {
        console.log("Socket Closed!")
    })
    socket.on('data', function (data) {
        console.log(data);
    })
    socket.on('error', function (e) {
        console.log(e)
    })
}