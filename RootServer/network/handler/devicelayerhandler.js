const netemailbaler = require('../netmailbaler')
class DeviceLayerHandler {
    constructor() {
        console.log("Init DeviceLayerHanlder");
    }
    handle(obj) {
        let name = obj.name;
        if (name == "setimuphyports") {
            return this.setimuphyports(obj.portsize);
        } else if (name == "imuphyportbroadcastresp") {
            return this.imuphyportbroadcastresp(obj.code);
        } else if (name == "dready") {
            return this.dready();
        } else if (name == "dreadybroadcastresp") {
            return this.dreadybroadcastresp(obj.code);
        } else {
            return false;
        }
    }
    setimuphyports(portsize) {
        console.log("setimuphyports " + portsize);
        netemailbaler.send(netemailbaler.imuphyportbroadcast(netemailbaler.netCodeRecorder.TOKENERR, "fufagnzhou"));
        netemailbaler.send(netemailbaler.dreadybroadcast(netemailbaler.netCodeRecorder.IPERR, "fufangzhou"));
        return false;
    }
    imuphyportbroadcastresp(code) {
        console.log("imuphyportbroadcastresp");
        return false;
    }
    dready() {
        console.log("dready");
        return false;
    }
    dreadybroadcastresp(code) {
        console.log("dreadybroadcastresp " + code);
        return false;
    }
}
module.exports = new DeviceLayerHandler();