﻿using MvcMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection - Contructor Injection of a new StoreDB based on Music Store Etities shown below
        private MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            //Get most popular albums
            var albums = GetTopSellingAlbums(5);

            return View(albums);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Query for DB to Find and Retrieve top Selling Albums according to OrderDetails
        private List<Album> GetTopSellingAlbums(int count)
        {
            //Group the order details by album and return
            //the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

    }
}