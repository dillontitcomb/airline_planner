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
        [HttpGet("/cities")]
        public ActionResult Cities()
        {
          List<City> cityList = City.GetAll();
          return View(cityList);
        }
        [HttpGet("/cities/add")]
        public ActionResult CityForm()
        {
          return View();
        }
        [HttpPost("/cities")]
        public ActionResult AddCity()
        {
          City newCity = new City(Request.Form["city-name"], Request.Form["city-size"]);
          newCity.Save();
          List<City> cityList = City.GetAll();
          return View("Cities", cityList);
        }
        [HttpGet("/flights")]
        public ActionResult Flights()
        {
          List<Flight> flightList = Flight.GetAll();
          return View(flightList);
        }
        [HttpGet("/flights/{id}")]
        public ActionResult FlightInfo(int id)
        {
          Flight newFlight = Flight.Find(id);
          return View(newFlight);
        }
        [HttpGet("/flights/add")]
        public ActionResult FlightForm()
        {
          List<City> cityList = City.GetAll();
          return View(cityList);
        }
        [HttpPost("/flights")]
        public ActionResult AddFlight()
        {
          Flight newFlight = new Flight(int.Parse(Request.Form["departure-city"]),int.Parse(Request.Form["arrival-city"]), Request.Form["airline"]);
          newFlight.Save();
          List<Flight> flightList = Flight.GetAll();
          return View("Flights", flightList);
        }
        [HttpPost("/results")]
        public ActionResult Results()
        {
          return View();
        }
    }
}
