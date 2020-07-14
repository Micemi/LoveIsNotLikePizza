using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public static class TestsHelpers
    {
        public static readonly List<Flavor> AllFlavors =
            new List<Flavor>(new[] {Flavor.Creepy, Flavor.Cute, Flavor.Geek, Flavor.Spicy});
    }

    public class PizzaTests
    {
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Pizza_Has_Flavor_Count_Equal_To_Difficulty(int flavorsQuantity)
        {
            Difficulty difficulty = new Difficulty {FlavorsQuantity = flavorsQuantity};
            
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);

            Assert.That(pizza.Flavors.Count, Is.EqualTo(flavorsQuantity));
        }

        [Test]
        public void Pizza_Has_Starting_State_Pending()
        {
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 4};
            
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);

            Assert.That(pizza.State, Is.EqualTo(PizzaState.Pending));
            Assert.That(Pizza.PizzasByState[PizzaState.Pending], Has.Member(pizza));
        }

        [Test]
        public void Pizza_Can_Change_State()
        {
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 4};
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);

            pizza.State = PizzaState.Chatted;

            Assert.That(pizza.State, Is.EqualTo(PizzaState.Chatted));
            Assert.That(Pizza.PizzasByState[PizzaState.Pending], Has.No.Member(pizza));
            Assert.That(Pizza.PizzasByState[PizzaState.Chatted], Has.Member(pizza));
        }
    }
}
