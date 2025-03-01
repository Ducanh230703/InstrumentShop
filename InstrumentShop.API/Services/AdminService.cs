using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;

namespace InstrumentShop.API.Services
{
    public class AdminService
    {
        private readonly InstrumentShopDbContext _context;

        public AdminService(InstrumentShopDbContext context)
        {
            _context = context;
        }
        public Admin AuthenticateAdmin(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email);
            if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                return admin;
            }
            return null;
        }
        public List<Admin> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.Find(id);
        }

        public void AddAdmin(Admin admin)
        {
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(admin.PasswordHash);
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            var existingAdmin = _context.Admins.Find(admin.AdminId);
            if (existingAdmin != null)
            {
                if (admin.PasswordHash != existingAdmin.PasswordHash)
                {
                    admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(admin.PasswordHash);
                }
                _context.Entry(existingAdmin).CurrentValues.SetValues(admin);
                _context.SaveChanges();
            }
        }

        public void DeleteAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }

        
    }
}