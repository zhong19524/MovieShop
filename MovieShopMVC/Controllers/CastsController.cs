﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class CastsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
