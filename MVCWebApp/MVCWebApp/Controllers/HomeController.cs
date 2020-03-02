﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWebApp.Models;
using SystemAPI.Models;

namespace MVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient client = new HttpClient(); 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            client.BaseAddress = new Uri("http://localhost:51044/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

      
        public IActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync("/api/product").Result;
            List<ProductModel> data =  response.Content.ReadAsAsync<List<ProductModel>>().Result; 
            return View(data);
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        //Ta bort senare, bara för design
        public IActionResult Loading()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return PartialView("_LoadingPartial", "Shared");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
