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
    public static Flight Find(int id)
    {
			MySqlConnection conn = DB.Connection();
			conn.Open();

			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM flights WHERE id = @thisId;";

			MySqlParameter thisId = new MySqlParameter();
			thisId.ParameterName = "@thisId";
			thisId.Value = id;
			cmd.Parameters.Add(thisId);

			var rdr = cmd.ExecuteReader() as MySqlDataReader;

			int flightId = 0;
			int flightDepCityId = 0;
      int flightArrCityId = 0;
      string flightAirline = "";

			while (rdr.Read())
			{
			  flightId = rdr.GetInt32(0);
			  flightDepCityId = rdr.GetInt32(1);
        flightArrCityId = rdr.GetInt32(2);
        flightAirline = rdr.GetString(3);
			}

			Flight foundFlight = new Flight(flightDepCityId, flightArrCityId, flightAirline, flightId);
			conn.Close();
			if (conn != null)
			{
			  conn.Dispose();
			}
			return foundFlight;
    }
    public List<City> GetCities()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT flights.dep_city_id, flights.arr_city_id FROM flights
          JOIN cities AS cities1 ON (cities.id = flights.dep_city_id)
          JOIN cities AS cities2 ON (cities.id = flights.arr_city_id)
          WHERE flights.id = @flightId;";

      MySqlParameter flightIdParameter = new MySqlParameter("@flightId", _id);
      cmd.Parameters.Add(flightIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<City> cityList = new List<City>{};

      while(rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string size = rdr.GetString(2);
        City newCity = new City(name, size, cityId);
        cityList.Add(newCity);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return cityList;
    }
    public void Save()
	 	{
			MySqlConnection conn = DB.Connection();
			conn.Open();

			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"INSERT INTO flights (dep_city_id, arr_city_id, airline) VALUES (@depCityId, @arrCityId, @airline);";

			MySqlParameter arrCityId = new MySqlParameter("@arrCityId", this._arrCityId);
			cmd.Parameters.Add(arrCityId);
      MySqlParameter depCityId = new MySqlParameter("@depCityId", this._depCityId);
      cmd.Parameters.Add(depCityId);
      MySqlParameter airline = new MySqlParameter("@airline", this._airline);
      cmd.Parameters.Add(airline);

			cmd.ExecuteNonQuery();
			_id = (int) cmd.LastInsertedId;

			conn.Close();
			if (conn != null)
			{
			conn.Dispose();
			}
	 	}
  }
}
