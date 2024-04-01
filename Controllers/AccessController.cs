﻿using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class AccessController : Controller
    {
        QlvaLiContext db = new QlvaLiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName")==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName")==null)
            {
                var u = db.TUsers.Where(x=>x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null )
                {
                    if (u.LoaiUser ==0)
                    { 
                        HttpContext.Session.SetString("UserName", u.Username.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("UserName", u.Username.ToString());
                        return RedirectToAction("DanhMucSanPham", "HomeAdmin", new { area = "Admin" });

                    }
                }
                
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}