using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PizzaTests
    {
        public static readonly List<Flavor> AllFlavors = new List<Flavor>(new [] {Flavor.Creepy, Flavor.Cute, Flavor.Geek, Flavor.Spicy}); 
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Pizza_Has_Flavor_Count_Equal_To_Difficulty(int flavorsQuantity)
        {
            Difficulty difficulty = new Difficulty {FlavorsQuantity = flavorsQuantity};
            
            Pizza pizza = new Pizza("Pizza", null, difficulty, AllFlavors);

            Assert.That(pizza.Flavors.Count, Is.EqualTo(flavorsQuantity));
        }
    }
}
