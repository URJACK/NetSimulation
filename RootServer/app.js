const Koa = require('koa')
const app = new Koa()
const views = require('koa-views')
const json = require('koa-json')
const onerror = require('koa-onerror')
const bodyparser = require('koa-bodyparser')
const logger = require('koa-logger')

//my extension module needed
const session = require('koa-session')
const koabody = require('koa-body')
const fs = require('fs')

//init function for client to server
const initSocketIo = require('./socketio/clienttoserver');
//init function for server to server in tcp
const initSocketServerTcp = require('./socketio/servertoservertcp');
//init function for server to server in udp
const initSocketServerUdp = require('./socketio/servertoserverudp');
//require a net module
const net = require('net')
//socket Config
const socketConfig = require('./socketio/config');
//client connect to server's port
const listenPort = socketConfig.HTTPPORT;
//server connect to server's port
const connectPort = socketConfig.TCPLISTEN;

//create the io's module
const socketIoServer = require('http').Server(app.callback())
const io = require('socket.io')(socketIoServer)
const udpServer = require('dgram').createSocket('udp4');

if (socketConfig.USESSTCP) {
  // servertoservertcp io module worked
  const serverConnectedServer = net
    .createServer(initSocketServerTcp)
    .listen(connectPort);
    console.log("Server To Server TCP Listen At ", socketConfig.TCPLISTEN);
}
if (socketConfig.USECSTCP) {
  // clienttoserver io module worked
  initSocketIo(io);
  console.log("Client To Server Tcp Listen Is Available");
}
if (socketConfig.USESSUDP) {
  // servertoserverudp io module worked
  initSocketServerUdp(udpServer);
  console.log("Server To Server UDP Listen At",socketConfig.UDPLISTEN);
}
console.log("HTTP Listen At ", socketConfig.HTTPPORT);
// Http Basic Task
socketIoServer.listen(listenPort);

// session config
app.keys = ['some secret hurr'];
const CONFIG = {
  key: "Ifox:Lab",
  maxAge: 86400000,
  overwrite: true,
  httpOnly: true
}
app.use(session(CONFIG, app));

// koa-body
app.use(koabody());

//route module
const routePath = __dirname + "\\routes";
let routeModules = [];
fs.readdir(routePath, function (err, files) {
  for (let index in files) {
    let filePath = files[index];
    let realFilePath = routePath + '\\' + filePath
    routeModules.push(require(realFilePath));
  }
  //routes
  for (let index in routeModules) {
    let routeModule = routeModules[index];
    app.use(routeModule.routes(), routeModule.allowedMethods());
  }
});

onerror(app)

// middlewares
app.use(bodyparser({
  enableTypes: ['json', 'form', 'text']
}))
app.use(json())
app.use(logger())
app.use(require('koa-static')(__dirname + '/public'))

app.use(views(__dirname + '/views', { extension: 'ejs' }))

// logger
app.use(async (ctx, next) => {
  const start = new Date()
  await next()
  const ms = new Date() - start
  console.log(`${ctx.method} ${ctx.url} - ${ms}ms`)
})

// error-handling
app.on('error', (err, ctx) => {
  console.error('server error', err, ctx)
});

module.exports = app
