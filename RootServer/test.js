const fs = require('fs');
let routePath = __dirname + '\\routes';
fs.readdir(routePath,function(err,files){
  if(err){
    console.log(err);
    throw err;
  }
  console.log(routePath);
  console.log(files);
})