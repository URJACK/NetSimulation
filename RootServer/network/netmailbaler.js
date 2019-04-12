const NetCodeRecorder = require('./netcoderecorder');
const dgram = require('dgram');
let client = dgram.createSocket('udp4');
class NetMailBaler {
    constructor() {
        console.log("Init NetMailBaler");
        this.remoteIp = "127.0.0.1";
        this.remotePort = 6000;
        this.netCodeRecorder = NetCodeRecorder;
    }
    /**private Packet */
    NodeLayer(name) {
        let obj = new Object();
        obj.type = "node";
        obj.name = name;
        return obj;
    }
    DeviceLayer(name) {
        let obj = new Object();
        obj.type = "device";
        obj.name = name;
        return obj;
    }
    RunLayer(name) {
        let obj = new Object();
        obj.type = "run";
        obj.name = name;
        return obj;
    }

    /** Layer:Node */
    ipbroadcast(id, ip) {
        let obj = this.NodeLayer("ipbroadcast");
        obj.id = id;
        obj.ip = ip;
        return JSON.stringify(obj);
    }
    nodebroadcast(id, ip) {
        let obj = this.NodeLayer("nodebroadcast");
        obj.id = id;
        obj.ip = ip;
        return JSON.stringify(obj);
    }
    registresp(code, token) {
        let obj = this.NodeLayer("registresp");
        obj.code = code;
        obj.token = token;
        return JSON.stringify(obj);
    }
    inforesp(code, info) {
        let obj = this.NodeLayer("inforesp");
        obj.code = code;
        obj.info = info;
        return JSON.stringify(obj);
    }
    nreadybroadcast(code, id) {
        let obj = this.NodeLayer("nreadybroadcast");
        obj.code = code;
        obj.id = id;
        return JSON.stringify(obj);
    }
    sdata(updata) {
        let obj = this.NodeLayer("sdata");
        obj.updata = updata;
        return obj;
    }

    /** Layer:Device */
    imuphyportbroadcast(code, id) {
        let obj = this.DeviceLayer("imuphyportbroadcast");
        obj.code = code;
        obj.id = id;
        obj = this.sdata(obj);
        return JSON.stringify(obj);
    }
    dreadybroadcast(code,id) {
        let obj = this.DeviceLayer("dreadybroadcast");
        obj.code = code;
        obj.id = id;
        obj = this.sdata(obj);
        return JSON.stringify(obj);
    }

    /** Layer:Run */
    linkbroadcast(code) {
        let obj = this.RunLayer("linkbroadcast");
        obj.code = code;
        obj = this.sdata(obj);
        return JSON.stringify(obj);
    }
    unlinkbroadcast(code) {
        let obj = this.RunLayer("unlinkbroadcast");
        obj.code = code;
        obj = this.sdata(obj);
        return JSON.stringify(obj);
    }

    /** public interface */
    send(str) {
        client.send(str, this.remotePort, this.remoteIp, function (err) {
            if (err) {
                console.log(err);
            }
        })
    }
    setIp(ip) {
        this.remoteIp = ip;
    }
    setPort(port) {
        this.remotePort = port;
    }
}
module.exports = new NetMailBaler();