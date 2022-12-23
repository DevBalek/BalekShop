﻿using BalekShop.Models.Domain;

namespace BalekShop.Repositories.Abstract
{
    public interface ICartService
    {
        bool Add(Cart model);
        bool Update(Cart model);
        bool Delete(int id);
        Cart FindById(int id);
        IEnumerable<Cart> GetAll();
    }
}
