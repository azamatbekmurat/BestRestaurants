using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BestRestaurants.Models;
using BestRestaurants;
using MySql.Data.MySqlClient;


namespace BestRestaurants.Tests
{

   [TestClass]
   public class RestaurantTests : IDisposable
   {
       public RestaurantTests()
       {
           DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=azamat;";
       }
       public void Dispose()
       {
         Restaurant.DeleteAll();
         Cuisine.DeleteAll();
       }

       [TestMethod]
       public void Equals_OverrideTrueForSameNameLocation_Restaurant()
       {
         //Arrange, Act
         Restaurant firstRestaurant = new Restaurant("Little Italia", "Seattle", 1);
         Restaurant secondRestaurant = new Restaurant("Little Italia", "Seattle", 1);

         //Assert
         Assert.AreEqual(firstRestaurant, secondRestaurant);
       }

       [TestMethod]
       public void Save_SavesRestaurantToDatabase_RestaurantList()
       {
         //Arrange
         Restaurant testRestaurant = new Restaurant("Little Italia", "Seattle", 1);
         testRestaurant.Save();

         //Act
         List<Restaurant> result = Restaurant.GetAll();
         List<Restaurant> testList = new List<Restaurant>{testRestaurant};

         //Assert
         CollectionAssert.AreEqual(testList, result);
       }

   }
}
