const router = require('koa-router')()

router.prefix('/users')

router.get('/', function (ctx, next) {
  if (ctx.session.id != null) {
    ctx.session.id = ctx.session.id + 1;
    ctx.body = ctx.session.id;
  } else {
    ctx.session.id = 1;
    ctx.body = "Init";
  }
})

router.post('/', function (ctx, next) {
  if (ctx.session.id != null) {
    ctx.session.id = ctx.session.id + 1;
    ctx.body = ctx.session.id;
  } else {
    ctx.session.id = 1;
    ctx.body = "Init";
    console.log(ctx.request.body);
  }
})

module.exports = router
