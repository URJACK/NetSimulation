<!-- TOC -->
- [修改内容](#修改内容)
- [简介(Introduction)](#简介introduction)
    - [额外的模块(Extra Extension)](#额外的模块extra-extension)
        - [Koa-body](#koa-body)
        - [Koa-session](#koa-session)
        - [Ejs](#ejs)
        - [Socket. io](#socket-io)
    - [AutoRoute](#autoroute)
    - [Socket模块的配置(Config About Socket)](#socket模块的配置config-about-socket)
    - [使用CS_socket模块(Use "Client and Server" Socket Module)](#使用cs_socket模块use-client-and-server-socket-module)
        - ["socket. io" in the Server](#socket-io-in-the-server)
        - ["socket. io" in the Client](#socket-io-in-the-client)
    - [使用SS_socket模块(Use Server and Server Socket Module)](#使用ss_socket模块use-server-and-server-socket-module)
        - [Server TCP](#server-tcp)
        - [Client TCP](#client-tcp)
        - [Server UDP](#server-udp)
        - [CLient UDP](#client-udp)

<!-- /TOC -->
# 修改内容
修改了 app.js 关于启动打印日志的一些问题
# 简介(Introduction)
这是为了快速生成一个带有socket的,使用ejs的koa框架

只需要修改 

servertoservertcp.js

servertoserverudp.js

clienttoserver.js 

就可以分别实现服务器之间的TCP、UDP 通信、浏览器与服务器的WebSocket通信

这个框架可以让基于Koa的服务器以最少的代码：

1·与另外的webServer 建立起socket链接。

2·与访问你的用户在他的浏览器上 建立起socket链接。

## 额外的模块(Extra Extension)
### Koa-body
从而能够解析post请求的数据
### Koa-session
实现session
### Ejs
使用ejs 而不是 pug 页面渲染
### Socket. io
实现浏览器与服务器的socket通信

## AutoRoute
只需要新增加一个路由文件放在"/routes"的路径下

就可以自动完成路由的配置，**不再需要在 app.js中添加额外语句**

比如 **/routes/user.js**
```
const router = require('koa-router')()

router.prefix('/users')

router.get('/', function (ctx, next) {
  ctx.body = 'this is a users response!'
})

module.exports = router
```

## Socket模块的配置(Config About Socket)
打开"/socketio/config.js"，以下属性。

想要更改端口
```
//Web HTTP 服务监听的端口
module.exports.HTTPPORT = 80;
//TCP 通信监听的端口
module.exports.TCPLISTEN = 6666;
//UDP 通信监听的端口
module.exports.UDPLISTEN = 6667;
```
想要决定是否启用某个模块
```
//是否使用客户端到服务器的TCP
module.exports.USECSTCP = true;
//是否使用服务器到服务器的TCP
module.exports.USESSTCP = true;
//是否使用服务器到服务器的UDP
module.exports.USESSUDP = true;
```
## 使用CS_socket模块(Use "Client and Server" Socket Module)
本模块使用的socket是基于socket.io实现的

这个socket.io模块是基于WebSocket的
无疑是实现Client 与 Server通信的最佳方案
### "socket. io" in the Server
"/app.js"的同级目录下可以找到"/socketio/clienttoserver.js"在该文件中可以对socket的监听逻辑做出修改。
```
module.exports = function (io) {
    io
        .on('connection', function (socket) {
            console.log('a user connected');
            console.log(socket.request.connection._peername.address); //来访者地址
            socket.on('syq', function (msg) {
                console.log(msg);
                var str = "I'v received : " + msg;
                socket.emit('mymsg', {
                    status: false,
                    msg: str
                });
            });
            socket.on('disconnect', function (data) {
                console.log("a user leaved")
            });
        });
}
```


### "socket. io" in the Client
在页面(编写view)使用socket很简单，直接饮用public的socketio即可.

写操作
```
socket.emit(name,content);
```

name 是这个报文的名字、content 是报文的内容

读操作
```
socket.on(name,function(content){
    /*Code*/    
})
```
这种方式来接受

就像下面这样。
```
<script src="/js/socket.io.js"></script>
<script>
  window.onload = function(){
    var socket = io();
    socket.on('mymsg',function(msg){
        console.log(msg);
    })
    socket.emit('syq',"nihao");
  }
</script>
```

## 使用SS_socket模块(Use Server and Server Socket Module)
尽管socket.io模块是实现Client 与 Server TCP 通信的不二之选。

但是WebSocket 用在能够直接通过TCP 与 UDP 进行通信的应用上实在是大材小用了。

这时,我们需要系统的:

"net"模块来完成 服务器与服务器的TCP通信。

"dgram"模块来完成
服务器与服务器的UDP通信。

### Server TCP
在"/socketio/servertoservertcp.js"中去编写这个函数

TCP服务器的读写API

写操作是
```
socket.write(content);
```
读操作是
```
socket.on('data',function(content){
    /*代码*/
})
```

```
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
```

### Client TCP
```
var net = require('net');
var port = 3500;
var host = '192.168.0.158';
var client = new net.Socket();
//创建socket客户端
client.setEncoding('binary');
//连接到服务端
client.connect(port, host, function () {
  client.write('hello server');
  //向端口写入数据到达服务端
});
client.on('data', function (data) {
  console.log('from server:' + data);
  //得到服务端返回来的数据
});
client.on('error', function (error) {
  //错误出现之后关闭连接
  console.log('error:' + error);
  //这句话似乎不需要 client.destory();
});
client.on('close', function () {
  //正常关闭连接
  console.log('Connection closed');
});
```

### Server UDP
在"/socketio/servertoserverudp.js"中去编写这个函数

写操作是
```
client.send("nihao", 6667, (err) => {
    console.log("Close");
    // client.close();
});
```
读操作是
```
server.on('message', (msg, rinfo) => {
    console.log(`服务器收到：${msg} 来自 ${rinfo.address}:${rinfo.port}`);
});
```


```
const config = require('./config');
module.exports = function (server) {
    server.on('error', (err) => {
        console.log(`服务器异常：\n${err.stack}`);
        server.close();
    });

    server.on('message', (msg, rinfo) => {
        console.log(`服务器收到：${msg} 来自 ${rinfo.address}:${rinfo.port}`);
    });

    server.on('listening', () => {
        const address = server.address();
        console.log(`Udp Listen At : ${address.address}:${address.port}`);
    });

    server.bind(config.UDPLISTEN);
}
```

### CLient UDP
```
const dgram = require('dgram');
//const buf1 = Buffer.from('Some ');
//const buf2 = Buffer.from('bytes');
const client = dgram.createSocket('udp4');
client.send("nihao", 6667, (err) => {
    console.log("Close");
    client.close();
});
```