using System.Linq;
using LiteDB;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private static ILiteDatabase _liteDatabase;

        private static ILiteDatabase GetInstance()
        {
            if (_liteDatabase == null)
            {
                _liteDatabase = new LiteDatabase("Filename=./test.db");
            }
            return _liteDatabase;
        }
        
        [HttpPost]
        public ReturnCode Register([FromBody]UserModel user)
        {
            ILiteDatabase liteDatabase = GetInstance();
            var iUser = liteDatabase.GetCollection<UserEntity>("user");
            var userInfo = iUser.Find(x => x.Username == user.Username);
            if (userInfo != null && userInfo.Any())
            {
                return ReturnCode.Fail(100, "用户名重复");
            }

            iUser.Insert(user.Adapt<UserEntity>());
            return ReturnCode.Success("注册成功");
        }

        [HttpPost]
        public ReturnCode Login([FromBody]UserModel user)
        {
            ILiteDatabase liteDatabase = GetInstance();
            var iUser = liteDatabase.GetCollection<UserEntity>("user");
            var userInfo = iUser.Find(x => x.Username == user.Username && x.Password == user.Password);
            if (userInfo == null || !userInfo.Any())
            {
                return ReturnCode.Fail(401, "用户名不存在或密码错误");
            }

            if (userInfo.Count() > 1)
            {
                return ReturnCode.Fail(401, "用户数量不正确");
            }
            return ReturnCode.Success("登录成功");
            
        }

        [HttpGet]
        public ReturnCode ListUsers()
        {
            ILiteDatabase liteDatabase = GetInstance();
            var iUser = liteDatabase.GetCollection<UserEntity>("user");
            return ReturnCode.Success(iUser.FindAll());
        }
    }
}