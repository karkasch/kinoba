using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business.Managers
{
    public class UserManager
    {
        public User LoginRegisterFromSocial(string provider, string userId, string email, string firstName, string lastName)
        {
            var login = string.Format("{0}_{1}", provider, userId);

            using (var db = new kinobaEntities())
            {

                var user = db.Users.FirstOrDefault(a => a.Login == login);
                if (user == null)
                {
                    user = new User()
                    {
                        Login = login,
                        Email = email,
                        UserType = (int)UserTypes.Agent,
                        Password = Guid.NewGuid().ToString(),
                        AddedDate = DateTime.UtcNow
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
                    {
                        user.Email = email;
                        user.ModifiedDate = DateTime.UtcNow;
                        db.SaveChanges();
                    }
                }

                return user;
            }

        }

        public List<User> LoadUsers(int pageNum, int pageSize)
        {
            var skip = (pageNum - 1) * pageSize;
            var take = pageNum * pageSize;

            using (var db = new kinobaEntities())
            {
                return db.Users.OrderByDescending(a => a.AddedDate).Skip(skip).Take(take).ToList();
            }
        }
    }
}
