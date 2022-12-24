using BalekShop.Models;
using BalekShop.Repositories.Abstract;

namespace BalekShop.Repositories.Implementation
{
    public class AdminService : IAdminService
	{
		private readonly DatabaseContext context;
		public AdminService(DatabaseContext context)
		{
			this.context = context;
		}
		public bool Add(Admin model)
		{
			try
			{
				context.Admin.Add(model);
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
				context.Admin.Remove(data);
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public Admin FindById(int id)
		{
			return context.Admin.Find(id);
		}

		public IEnumerable<Admin> Get()
		{
			return context.Admin.ToList();
		}

		public bool Update(Admin model)
		{
			try
			{
				context.Admin.Update(model);
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
