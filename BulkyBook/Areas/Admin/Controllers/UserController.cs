using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BulkyBook.Utility;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View();
        }
        

        #region API CALLS
       
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _db.applicationUsers.Include(u=>u.Company).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId==user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u=>u.Id==roleId).Name;
                if (user.Company==null)
                {
                    user.Company = new Company() { Name = "" };
                }
            }
            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDB = _db.applicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDB==null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if (objFromDB.LockoutEnd != null && objFromDB.LockoutEnd>DateTime.Now)
            {
                //user Locked we will unlock them
                objFromDB.LockoutEnd = DateTime.Now;
            }
            else
            {
                //not locked we will lock them
                objFromDB.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful" });

        }
        #endregion


    }
}
