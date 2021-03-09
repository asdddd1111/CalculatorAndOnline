using Microsoft.EntityFrameworkCore;
using Online.DbStaff.Model;
using System.Linq;

namespace Online.DbStaff.Repository
{
    public class UserRepository
    {
        protected OnlineContext context;
        protected DbSet<User> dbSet;
        public UserRepository(OnlineContext context)
        {
            this.context = context;
            dbSet = context.Set<User>();
        }
        public bool IsUnique(string login)
        {
            if (dbSet.Any(x => x.Login == login))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Save(User model)
        {
            if (model.Id > 0)
            {
                dbSet.Update(model);
                context.SaveChanges();
                return;
            }

            dbSet.Add(model);
            context.SaveChanges();
        }
        public User GetUserByNameAndPassword(string login, string password)
        {
            return dbSet.SingleOrDefault(x => x.Login == login && x.Password == password);
        }
    }
}
