
const multer = require('koa-multer');//加载koa-multer模块
const router = require('koa-router')()
//文件上传
//配置
var storage = multer.diskStorage({
  //文件保存路径
  destination: function (req, file, cb) {
    cb(null, 'F:\\')
  },
  //修改文件名称
  filename: function (req, file, cb) {
    var fileFormat = (file.originalname).split(".");
    cb(null,Date.now() + "." + fileFormat[fileFormat.length - 1]);
  }
})
//加载配置
var upload = multer({ storage: storage });


router.prefix('/uploads')

//路由
router.post('/image', upload.single('file'), async (ctx, next) => {
  ctx.body = {
    state:200,
    msg:"识别成功",
    data:"这是坦克"
  }
})

module.exports = router
