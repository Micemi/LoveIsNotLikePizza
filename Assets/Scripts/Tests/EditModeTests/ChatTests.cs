using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ChatTests
    {
        private DifficultyData[] difficulties;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            difficulties = Resources.LoadAll<DifficultyData>("Data/Difficulties");
        }
        
        [Test]
        public void Chat_Initial_Status()
        {
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 2, InitialTime = 40f};
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);
            
            Chat chat = new Chat(pizza);
            chat.Start();

            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.WaitingForPizzaEmoji));
            Assert.That(chat.CurrentTime, Is.EqualTo(40f));
            Assert.That(chat.CurrentHotness, Is.EqualTo(0.5f));

            Assert.That(chat.Pizza.State,  Is.Not.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(0));
        }
        
        [Test]
        public void Chat_First_Tick()
        {
            // Arrange
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 2, InitialTime = 40f, CoolingPerSec = 0.05f};
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);
            Chat chat = new Chat(pizza);
            chat.Start();

            bool wasOnPizzaSendsEmojiCalled = false;
            Emoji emojiSent = null;
            chat.OnPizzaSendsEmoji += (emoji) =>
            {
                wasOnPizzaSendsEmojiCalled = true;
                emojiSent = emoji;
            };

            bool wasOnPizzaSendsReactionCalled = false;
            Emoji reactionSent = null;
            chat.OnPizzaSendsReaction += (emoji) =>
            {
                wasOnPizzaSendsReactionCalled = true;
                reactionSent = emoji;
            };
            
            // Act
            chat.Tick(1f);
            
            // Assert
            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.WaitingForPlayerEmoji));
            Assert.That(chat.CurrentTime, Is.EqualTo(39f).Within(0.01f)); // 1 second passed
            Assert.That(chat.CurrentHotness, Is.EqualTo(0.45f).Within(0.001f)); // cooled by 5%
            
            Assert.That(wasOnPizzaSendsEmojiCalled, Is.True);
            Assert.That(emojiSent, Is.Not.Null);
            Assert.That(pizza.Flavors, Has.Member(emojiSent.Flavor));

            Assert.That(wasOnPizzaSendsReactionCalled, Is.False);
            Assert.That(reactionSent, Is.Null);

            Assert.That(chat.Pizza.State,  Is.Not.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(0));
        }
        
        [Test]
        public void Chat_Second_Tick()
        {
            // Arrange
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 2, InitialTime = 40f, CoolingPerSec = 0.05f};
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);
            Chat chat = new Chat(pizza);
            chat.Start();
            chat.Tick(1f); // first tick

            bool wasOnPizzaSendsEmojiCalled = false;
            Emoji emojiSent = null;
            chat.OnPizzaSendsEmoji += (emoji) =>
            {
                wasOnPizzaSendsEmojiCalled = true;
                emojiSent = emoji;
            };

            bool wasOnPizzaSendsReactionCalled = false;
            Emoji reactionSent = null;
            chat.OnPizzaSendsReaction += (emoji) =>
            {
                wasOnPizzaSendsReactionCalled = true;
                reactionSent = emoji;
            };
            
            // Act
            chat.Tick(1f); // second tick
            
            // Assert
            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.WaitingForPlayerEmoji));
            Assert.That(chat.CurrentTime, Is.EqualTo(38f).Within(0.01f)); // 2 second passed
            Assert.That(chat.CurrentHotness, Is.EqualTo(0.40f).Within(0.001f)); // cooled by 10%

            Assert.That(wasOnPizzaSendsEmojiCalled, Is.False);
            Assert.That(emojiSent, Is.Null);

            Assert.That(wasOnPizzaSendsReactionCalled, Is.False);
            Assert.That(reactionSent, Is.Null);

            Assert.That(chat.Pizza.State,  Is.Not.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(0));
        }
        
        [Test]
        public void Chat_Player_Sends_Right_Emoji_On_His_Turn()
        {
            // Arrange
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 1, InitialTime = 40f, CoolingPerSec = 0.05f, HotnessBonus = 0.2f};
            Pizza pizza = new Pizza("Pizza", null, difficulty,
                new List<Flavor>(new[] {Flavor.Spicy}));
            Chat chat = new Chat(pizza);
            chat.Start();
            chat.Tick(1f); // first tick

            bool wasOnPizzaSendsEmojiCalled = false;
            Emoji emojiSent = null;
            chat.OnPizzaSendsEmoji += (emoji) =>
            {
                wasOnPizzaSendsEmojiCalled = true;
                emojiSent = emoji;
            };

            bool wasOnPizzaSendsReactionCalled = false;
            Emoji reactionSent = null;
            chat.OnPizzaSendsReaction += (emoji) =>
            {
                wasOnPizzaSendsReactionCalled = true;
                reactionSent = emoji;
            };

            Emoji playerEmoji = new Emoji(EmojiData.EmojisByFlavor[Flavor.Spicy].GetRandom());
            
            // Act
            chat.SendPlayerEmoji(playerEmoji);
            
            // Assert
            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.WaitingForPizzaEmoji));
            Assert.That(chat.CurrentTime, Is.EqualTo(39f).Within(0.01f)); // 1 second passed
            Assert.That(chat.CurrentHotness, Is.EqualTo(0.65f).Within(0.001f)); // cooled by 5% but heat up by 20%

            Assert.That(wasOnPizzaSendsEmojiCalled, Is.False);
            Assert.That(emojiSent, Is.Null);

            Assert.That(wasOnPizzaSendsReactionCalled, Is.True);
            Assert.That(reactionSent, Is.Not.Null);
            Assert.That(reactionSent.Category, Is.EqualTo(EmojiCategory.GoodReaction));

            Assert.That(chat.Pizza.State,  Is.Not.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(0));
        }
        
        [Test]
        public void Chat_Player_Sends_Wrong_Emoji_On_His_Turn()
        {
            // Arrange
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 1, InitialTime = 40f, CoolingPerSec = 0.05f, HotnessPenalty = 0.2f, TimePenalty = 5f};
            Pizza pizza = new Pizza("Pizza", null, difficulty,
                new List<Flavor>(new[] {Flavor.Spicy}));
            Chat chat = new Chat(pizza);
            chat.Start();
            chat.Tick(1f); // first tick

            bool wasOnPizzaSendsEmojiCalled = false;
            Emoji emojiSent = null;
            chat.OnPizzaSendsEmoji += (emoji) =>
            {
                wasOnPizzaSendsEmojiCalled = true;
                emojiSent = emoji;
            };

            bool wasOnPizzaSendsReactionCalled = false;
            Emoji reactionSent = null;
            chat.OnPizzaSendsReaction += (emoji) =>
            {
                wasOnPizzaSendsReactionCalled = true;
                reactionSent = emoji;
            };

            Emoji playerEmoji = new Emoji(EmojiData.EmojisByFlavor[Flavor.Geek].GetRandom());
            
            // Act
            chat.SendPlayerEmoji(playerEmoji);
            
            // Assert
            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.WaitingForPizzaEmoji));
            Assert.That(chat.CurrentTime, Is.EqualTo(34f).Within(0.01f)); // 1 second passed and 5 seconds penalty
            Assert.That(chat.CurrentHotness, Is.EqualTo(0.25f).Within(0.001f)); // cooled by 5% and another 20% for the penalty

            Assert.That(wasOnPizzaSendsEmojiCalled, Is.False);
            Assert.That(emojiSent, Is.Null);

            Assert.That(wasOnPizzaSendsReactionCalled, Is.True);
            Assert.That(reactionSent, Is.Not.Null);
            Assert.That(reactionSent.Category, Is.EqualTo(EmojiCategory.BadReaction));

            Assert.That(chat.Pizza.State,  Is.Not.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(0));
        }

        [Test]
        public void Chat_Player_Sends_Emoji_When_It_Is_Not_His_Turn()
        {
            // Arrange
            Difficulty difficulty = new Difficulty {FlavorsQuantity = 1, InitialTime = 40f, CoolingPerSec = 0.05f, HotnessPenalty = 0.2f, TimePenalty = 5f};
            Pizza pizza = new Pizza("Pizza", null, difficulty,
                new List<Flavor>(new[] {Flavor.Spicy}));
            Chat chat = new Chat(pizza);
            chat.Start();

            bool wasOnPizzaSendsEmojiCalled = false;
            Emoji emojiSent = null;
            chat.OnPizzaSendsEmoji += (emoji) =>
            {
                wasOnPizzaSendsEmojiCalled = true;
                emojiSent = emoji;
            };

            bool wasOnPizzaSendsReactionCalled = false;
            Emoji reactionSent = null;
            chat.OnPizzaSendsReaction += (emoji) =>
            {
                wasOnPizzaSendsReactionCalled = true;
                reactionSent = emoji;
            };

            Emoji playerEmoji = new Emoji(EmojiData.EmojisByFlavor[Flavor.Spicy].GetRandom());
            
            // Act
            chat.SendPlayerEmoji(playerEmoji);
            
            // Assert
            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.WaitingForPizzaEmoji));
            Assert.That(chat.CurrentTime, Is.EqualTo(40f).Within(0.01f)); // No time passed yet
            Assert.That(chat.CurrentHotness, Is.EqualTo(0.5f).Within(0.001f)); // Not cooled yet

            Assert.That(wasOnPizzaSendsEmojiCalled, Is.False);
            Assert.That(emojiSent, Is.Null);

            Assert.That(wasOnPizzaSendsReactionCalled, Is.False);
            Assert.That(reactionSent, Is.Null);

            Assert.That(chat.Pizza.State,  Is.Not.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(0));
        }

        [TestCase(0.00f,  2000f)]
        [TestCase(0.39f,  2000f)]
        [TestCase(0.40f,  6000f)]
        [TestCase(0.69f,  6000f)]
        [TestCase(0.70f,  8000f)]
        [TestCase(0.94f,  8000f)]
        [TestCase(0.95f, 10000f)]
        [TestCase(1.00f, 10000f)]
        public void Chat_Finishes_When_Out_Of_Time(float finishHotness, float expectedPoints)
        {
            // Arrange
            float deltaHotness = finishHotness - 0.5f;
            
            Difficulty difficulty = new Difficulty(difficulties[3])
            {
                FlavorsQuantity = 4, CoolingPerSec = 0f, HotnessBonus = deltaHotness,
            };
            Pizza pizza = new Pizza("Pizza", null, difficulty, TestsHelpers.AllFlavors);
            Chat chat = new Chat(pizza);
            chat.Start();

            chat.Tick(1f); // first tick

            Emoji playerEmoji = new Emoji(EmojiData.EmojisByFlavor[Flavor.Spicy].GetRandom());
            chat.SendPlayerEmoji(playerEmoji); // Setting Hotness to 0.5 + deltaHotness

            bool wasOnChatFinishCalled = false;
            float finishPoints = 0;
            chat.OnChatFinish += (points) =>
            {
                wasOnChatFinishCalled = true;
                finishPoints = points;
            };

            // Act
            chat.Tick(39f); // ending tick
            
            // Assert
            Assert.That(chat.State, Is.EqualTo(Chat.ChatState.Finished));
            Assert.That(chat.CurrentTime, Is.EqualTo(0f).Within(0.01f));
            Assert.That(chat.CurrentHotness, Is.EqualTo(finishHotness).Within(0.001f));

            Assert.That(wasOnChatFinishCalled, Is.True);
            Assert.That(finishPoints, Is.EqualTo(expectedPoints).Within(0.01f));

            Assert.That(chat.Pizza.State, Is.EqualTo(PizzaState.Chatted));
            Assert.That(chat.Pizza.Points, Is.EqualTo(expectedPoints));
        }
    }
}
