using Microsoft.AspNetCore.Mvc;
using AirlinePlannerApp.Models;
using System.Collections.Generic;

namespace AirlinePlannerApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
          return View();
        }
        [HttpGet("/cities/add")]
        public ActionResult AddCity()
        {
          return View();
        }
        [HttpGet("/cities")]
        public ActionResult Cities()
        {
          List<City> cityList = City.GetAll();
          return View(cityList);
        }
        [HttpGet("/flights")]
        public ActionResult Flights()
        {
          List<Flight> flightList = Flight.GetAll();
          return View(flightList);
        }
        [HttpGet("/flights/add")]
        public ActionResult AddFlight()
        {
          List<City> cityList = City.GetAll();
          return View(cityList);
        }
        [HttpPost("/results")]
        public ActionResult Results()
        {
          return View();
        }
    }
}
