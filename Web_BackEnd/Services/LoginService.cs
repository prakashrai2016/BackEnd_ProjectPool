using ProjectPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.Library;
using ProjectPool.Repository;
using ProjectPool.DataEntity;

namespace ProjectPool.Services
{
    public class LoginService : GenericRepository<User>
    {
        string ConnectionString;

        public LoginService(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Save(User user, bool isEdit)
        {
            if (isEdit)
            {
                user.UserId = UserSession.UserId;
                Update(user);
            }

            else
            {
                var salt = Guid.NewGuid().ToString();
                user.Password = Common.GenerateHash(user.Password, salt);
                Add(user);
            }

            SaveChanges();
        }

        public bool CheckUser(LoginViewModel model)
        {
            var user = Load(x => x.Username == model.username).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == Common.GenerateHash(model.password, user.Salt))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void SetUserSession(LoginViewModel model)
        {
            var user = Load(x => x.Username == model.username).FirstOrDefault();

            UserSession.UserId = user.UserId;
            UserSession.UserName = user.Username;
            
        }
    }
}