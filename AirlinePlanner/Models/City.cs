using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using AirlinePlannerApp;

namespace AirlinePlannerApp.Models
{
  public class City
  {
    private int _id;
		private string _name;
    private string _size;

    public City(string name, string size, int id = 0)
    {
      _id = id;
			_name = name;
      _size = size;
    }
		public override bool Equals(System.Object otherCity)
    {
	    if (!(otherCity is City))
	    {
	    return false;
	    }
	    else
	    {
        City newCity = (City) otherCity;
        bool idEquality = (this.GetId() == newCity.GetId());
        bool clientEquality = (this.GetName() == newCity.GetName());
        return (idEquality && clientEquality);
	    }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public static List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities ORDER BY name ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string size = rdr.GetString(2);

        City newCity = new City(name, size, id);
        allCities.Add(newCity);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allCities;
    }
  }
}
