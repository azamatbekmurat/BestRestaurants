using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BestRestaurants.Models;

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
       public void Equals_ReturnsTrueForSameName_Cuisine()
       {
         //Arrange, Act
         Cuisine firstCuisine = new Cuisine("Italian");
         Cuisine secondCuisine = new Cuisine("Italian");

         //Assert
         Assert.AreEqual(firstCuisine, secondCuisine);
       }

       [TestMethod]
       public void Save_SavesCuisineToDatabase_CuisineList()
       {
         //Arrange
         Cuisine testCuisine = new Cuisine("Italian");
         testCuisine.Save();

         //Act
         List<Cuisine> result = Cuisine.GetAll();
         List<Cuisine> testList = new List<Cuisine>{testCuisine};

         //Assert
         CollectionAssert.AreEqual(testList, result);
       }

   }
}
