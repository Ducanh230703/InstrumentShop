using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminsController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Admin>> GetAdmins()
        {
            return _adminService.GetAllAdmins();
        }

        [HttpGet("{id}")]
        public ActionResult<Admin> GetAdmin(int id)
        {
            var admin = _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return admin;
        }
        [HttpPost("authenticate")]
        public ActionResult<Admin> AuthenticateAdmin(AdminLoginModel model)
        {
            var admin = _adminService.AuthenticateAdmin(model.Email, model.Password);
            if (admin == null)
            {
                return Unauthorized();
            }
            return admin;
        }
        [HttpPost]
        public ActionResult<Admin> PostAdmin(Admin admin)
        {
            _adminService.AddAdmin(admin);
            return CreatedAtAction(nameof(GetAdmin), new { id = admin.AdminId }, admin);
        }

        [HttpPut("{id}")]
        public IActionResult PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }
            _adminService.UpdateAdmin(admin);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            _adminService.DeleteAdmin(id);
            return NoContent();
        }


    }

    public class AdminLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}