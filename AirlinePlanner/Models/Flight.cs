using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using AirlinePlannerApp;

namespace AirlinePlannerApp.Models
{
  public class Flight
  {
    private int _id;
		private int _depCityId;
    private int _arrCityId;
    private string _airline;

    public Flight(int depCityId, int arrCityId, string airline, int id = 0)
    {
      _id = id;
			_depCityId = depCityId;
			_arrCityId = arrCityId;
      _airline = airline;
    }
		public override bool Equals(System.Object otherFlight)
    {
	    if (!(otherFlight is Flight))
	    {
	    return false;
	    }
	    else
	    {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = (this.GetId() == newFlight.GetId());
        bool clientEquality = (this.GetAirline() == newFlight.GetAirline());
        return (idEquality && clientEquality);
	    }
    }
    public int GetId()
    {
      return _id;
    }
    public int GetDepCityId()
    {
      return _depCityId;
    }
    public int GetArrCityId()
    {
      return _arrCityId;
    }
    public string GetAirline()
    {
      return _airline;
    }
    public static List<Flight> GetAll()
    {
      List<Flight> allFlights = new List<Flight> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM flights ORDER BY dep_city_id ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int depCityId = rdr.GetInt32(1);
        int arrCityId = rdr.GetInt32(2);
        string airline = rdr.GetString(3);

        Flight newFlight = new Flight(depCityId, arrCityId, airline, id);
        allFlights.Add(newFlight);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allFlights;
    }
  }
}
