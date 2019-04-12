const devicelayerhandler = require('./devicelayerhandler');
const runlayerhandler = require('./runlayerhandler');
const netmailbaler = require('../netmailbaler');
class NodeLayerHandler {
    constructor() {
        console.log("init nodelayerhandler");
        this.devicelayerhandler = devicelayerhandler;
        this.runlayerhandler = runlayerhandler;
    }
    handle(obj) {
        let name = obj.name;
        if (name == "regist") {
            return this.regist(obj.id);
        } else if (name == "info") {
            return this.info(obj.id, obj.token);
        } else if (name == "quit") {
            return this.quit(obj.id, obj.token);
        } else if (name == "modify") {
            return this.modify(obj.id, obj.token);
        } else if (name == "nready") {
            return this.nready(obj.id, obj.token);
        } else if (name == "ipbroadcastresp") {
            return this.ipbroadcastresp(obj.code, obj.id);
        } else if (name == "nodebroadcastresp") {
            return this.nodebroadcastresp(obj.code, obj.id);
        } else if (name == "nreadybroadcastresp") {
            return this.nreadybroadcastresp(obj.code, obj.id);
        } else if (name == "cdata") {
            return this.cdata(obj.id, obj.token, obj.updata);
        } else {
            return false;
        }
    }
    regist(id) {
        console.log("regist " + id);
        netmailbaler.send(netmailbaler.ipbroadcast("fangzhou", "12.4.23.7"));
        netmailbaler.send(netmailbaler.nodebroadcast("fangzhou", "12.4.23.7"));
        netmailbaler.send(netmailbaler.registresp(netmailbaler.netCodeRecorder.SUCCESS, "12.4.23.7"));
        netmailbaler.send(netmailbaler.inforesp(netmailbaler.netCodeRecorder.TOKENERR, "what ?"));
        netmailbaler.send(netmailbaler.nreadybroadcast(netmailbaler.netCodeRecorder.IPERR, "fangzhou"));
        return false;
    }
    info(id, token) {
        console.log("info " + id);
        return false;
    }
    quit(id, token) {
        console.log("quit " + id);
        return false;
    }
    modify(id, token) {
        console.log("modify " + id);
        return false;

    }
    nready(id, token) {
        console.log("nready " + id);
        return false;

    }
    ipbroadcastresp(code, id) {
        console.log("ipbroadcastresp " + id);
        return false;
    }
    nodebroadcastresp(code, id) {
        console.log("nodebroadcastresp " + id);
        return false;
    }
    nreadybroadcastresp(code, id) {
        console.log("nreadybroadcastresp " + id);
        return false;

    }
    cdata(id, token, updata) {
        if (updata.type == "device") {
            return this.devicelayerhandler.handle(updata);
        } else if (updata.type == "run") {
            return this.runlayerhandler.handle(updata);
        } else {
            return false;
        }
    }


}
module.exports = new NodeLayerHandler();