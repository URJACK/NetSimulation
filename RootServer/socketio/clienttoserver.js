module.exports = function (io) {
    io
        .on('connection', function (socket) {
            console.log('a user connected');
            console.log(socket.request.connection._peername.address); //来访者地址
            socket.on('syq', function (msg) {
                console.log(msg);
                var str = "I'v received : " + msg;
                socket.emit('syq2', {
                    status: false,
                    msg: str
                });
            });
            socket.on('disconnect', function (data) {
                console.log("a user leaved")
            });
        });
}