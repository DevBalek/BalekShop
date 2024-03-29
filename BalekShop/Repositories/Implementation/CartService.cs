﻿using BalekShop.Models;
using BalekShop.Repositories.Abstract;

namespace BalekShop.Repositories.Implementation
{
    public class CartService : ICartService
    {

        private readonly DatabaseContext context;
        public CartService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Cart model)
        {
            try
            {
                context.Cart.Add(model);
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
                context.Cart.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Cart FindById(int id)
        {
            return context.Cart.Find(id);
        }

		public List<Cart> FindByCartsUserId(int id)
		{
			return context.Cart.Where(a=>a.UserID==id).ToList();
		}

		public IEnumerable<Cart> Get()
        {
            return context.Cart.ToList();
        }

        public bool Update(Cart model)
        {
            try
            {
                context.Cart.Update(model);
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
