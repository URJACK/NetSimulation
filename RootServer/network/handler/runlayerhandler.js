const netmailbaler = require('../netmailbaler');
class RunLayerHandler {
    constructor() {
        console.log("Init RunLayerHandler");
    }
    handle(obj) {
        let name = obj.name;
        if (name == "link") {
            return this.link(obj.idA, obj.idB, obj.imuphyportA, obj.imuphyportB);
        } else if (name == "linkbroadcastresp") {
            return this.linkbroadcastresp(obj.code);
        } else if (name == "unlink") {
            return this.unlink(obj.idA, obj.idB, obj.imuphyportA, obj.imuphyportB);
        } else if (name == "unlinkbroadcastresp") {
            return this.unlinkbroadcastresp(obj.code);
        } else {
            return false;
        }
    }
    link(idA, idB, imuphyportA, imuphyportB) {
        console.log("link " + imuphyportA);
        netmailbaler.send(netmailbaler.linkbroadcast(netmailbaler.netCodeRecorder.REPEATLANDING));
        netmailbaler.send(netmailbaler.unlinkbroadcast(netmailbaler.netCodeRecorder.SUCCESS));
        return false;
    }
    linkbroadcastresp(code) {
        console.log("linkbroadcastresp " + code)
        return false;
    }
    unlink(idA, idB, imuphyportA, imuphyportB) {
        console.log("unlink " + imuphyportA)
        return false;
    }
    unlinkbroadcastresp(code) {
        console.log("unlinkbroadcastresp " + code)
        return false;
    }
}
module.exports = new RunLayerHandler();