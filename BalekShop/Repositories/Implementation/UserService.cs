using BalekShop.Models.Domain;
using BalekShop.Repositories.Abstract;

namespace BalekShop.Repositories.Implementation
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;
        public UserService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(User model)
        {
            try
            {
                model.CartId = model.Id;
                context.User.Add(model);
                context.SaveChanges();

                var lastUserID = GetAll().Last<User>().Id;

                context.Cart.Add(new Cart { UserID = lastUserID });
                model.CartId = lastUserID;
                Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.User.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User FindById(int id)
        {
            return context.User.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            var data = (from user in context.User                        
                        select new User
                        {
                            Id = user.Id,
                            UserName = user.UserName,                            
                            Adress = user.Adress,
                            Email = user.Email,
                            Password = user.Password,                            
                        }
                        ).ToList();
            return data;
        }

        public bool Update(User model)
        {
            try
            {
                context.User.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
