using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.Kata.BowlingGame;

namespace Yoox.Kata.BowlingGameTest
{
    [TestClass]
    public class GameTest
    {
        private Game game;

        [TestInitialize]
        public void TestInitialize()
        {
            this.game = new Game();
        }

        private void RollMany(int n, int pins)
        {
            for (var i = 0; i < n; i++)
            {
                game.Roll(pins);
            }
        }

        [TestMethod]
        public void When_AllRollsWithoutBreakdown_Expect_ScoreEqualsZero()
        {
            RollMany(20, 0);
            Assert.AreEqual(0, game.Score());
        }

        [TestMethod]
        public void When_AllRollsWithSingleBreakdown_Expect_ScoreEqualsTwenty()
        {
            RollMany(20, 1);
            Assert.AreEqual(20, game.Score());
        }

        [TestMethod]
        public void When_AllRollsWithoutExceptOneSpareFollowedByTwoPins_Expect_Score14()
        {
            game.Roll(9);
            game.Roll(1);
            game.Roll(2);
            RollMany(17, 0);
            Assert.AreEqual(14, game.Score());
        }

    }
}
