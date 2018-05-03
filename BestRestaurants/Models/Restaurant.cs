using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _city;
    private int _cuisineId;

    public Restaurant(string Name, string City, int CuisineId, int Id = 0)
    {
      _name = Name;
      _city = City;
      _cuisineId = Cuisine;
      _id = Id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetCity()
    {
      return _city;
    }
    public string GetCuisineId()
    {
      return _cuisineId;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, city, cuisineId) VALUES (@thisName, @thisCity, @thisCuisine_id);";

      cmd.Parameters.Add(new MySqlParameter("@thisName", _name));

      MySqlParameter city = new MySqlParameter();
      city.ParameterName = "@thisCity";
      city.Value = this._city;
      cmd.Parameters.Add(city); //another way to define is: "cmd.Parameters.Add(new MySqlParameter("@thisCity", _city));"

      cmd.Parameters.Add(new MySqlParameter("@thisCuisine_id", _cuisineId));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

    }
    public static List<Restaurant> GetAll();
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantCity = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(4);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCity, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }
    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", searchId));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int restaurantId = 0;
      string restaurantName = "";
      string restaurantCity = "";
      int restaurantCuisineId = 0;

      while(rdr.Read())
      {
        restaurantId = rdr.GetInt32(0);
        restaurantName = rdr.GetString(1);
        restaurantCity = rdr.GetString(2);
        restaurantCuisineId = rdr.GetInt32(3);
      }
      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCity, restaurantCuisineId, restaurantId);
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return newRestaurant;
    }
    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM restaurants;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool cityEquality = this.GetCity() == newRestaurant.GetCity();
        bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
      }
    }
  }
}
