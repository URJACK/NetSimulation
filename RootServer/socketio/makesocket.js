const net = require('net');
/**
 * @param {*callbacks}: contains 4 function menbers
 * connect : function,
 * data : function,
 * close : function,
 * error : function
 */
module.exports = function (host, port, callbacks) {
    let client = new net.Socket();
    client.setEncoding('binary');
    client.connect(port, host, callbacks.connect);
    client.on('data', callbacks.data);
    client.on('error', callbacks.error);
    client.on('close', callbacks.close);
    return client;
}