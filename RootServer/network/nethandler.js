const nodelayerhandler = require('./handler/nodelayerhandler');
class NetHandler {
    constructor() {
        console.log("init NetHandler");
        this.nodelayerhandler = nodelayerhandler;
    }
    handle(obj) {
        try {
            obj = JSON.parse(obj);
            if (obj.type == "node") {
                return this.nodelayerhandler.handle(obj);
            } else {
                return false;
            }
        } catch (err) {
            if (err) {
                console.log("Debug message is not <JSON>!")
                return;
            }
        }
    }
}
module.exports = new NetHandler();