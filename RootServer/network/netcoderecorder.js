/**
 * 编码记录器
 * 记录下通信时交互所使用的编码
 */
class NetCodeRecorder{
    constructor(){
        console.log("Init Code Recorder")
        this.SUCCESS = 200;
        this.IPERR = 301;
        this.TOKENERR = 302;
        this.NAMEERR = 303;
        this.REPEATLANDING = 304;
        this.IMUPHYPORTOCCUPIED = 305;
        this.DEVICENOTEXIST = 306;
        this.LINEERR = 307;
        this.IMUPHYPORTNUMERR = 308;
    }
}
module.exports = new NetCodeRecorder();