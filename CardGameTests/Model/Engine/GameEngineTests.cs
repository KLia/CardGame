
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks;
using CardGame.Model.Engine;
using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;
using Moq;
using NUnit.Framework;

namespace CardGameTests.Model.Engine
{
    [TestFixture]
    public class GameEngineTests
    {
        private Mock<IPlayer> _p1;
        private Mock<IPlayer> _p2;
        private Mock<IGameState> _gameState;

        [SetUp]
        public void Setup()
        {
            _p1 = new Mock<IPlayer>(MockBehavior.Strict);
            _p1.Setup(p => p.Id).Returns(1);
            _p1.Setup(p => p.Name).Returns("P1");
            _p1.Setup(p => p.CurrentMana).Returns(0);
            _p1.Setup(p => p.Deck.Shuffle());
            _p1.Setup(p => p.TotalMana).Returns(It.IsAny<int>);
            _p1.SetupProperty(p => p.TotalMana, It.IsAny<int>());
            _p1.Setup(p => p.DrawCard(It.IsAny<bool>())).Returns(It.IsAny<ICard>());
            _p1.Setup(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>())).Returns(new List<ICard>());

            _p2 = new Mock<IPlayer>(MockBehavior.Strict);
            _p2.Setup(p => p.Id).Returns(1);
            _p2.Setup(p => p.Name).Returns("P2");
            _p2.Setup(p => p.CurrentMana).Returns(0);
            _p2.Setup(p => p.Deck.Shuffle());
            _p2.Setup(p => p.TotalMana).Returns(It.IsAny<int>);
            _p2.SetupProperty(p => p.TotalMana, It.IsAny<int>());
            _p2.Setup(p => p.DrawCard(It.IsAny<bool>())).Returns(It.IsAny<ICard>());
            _p2.Setup(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>())).Returns(new List<ICard>());

            _gameState = new Mock<IGameState>(MockBehavior.Strict);
            _gameState.Setup(gs => gs.Player).Returns(_p1.Object);
            _gameState.Setup(gs => gs.Opponent).Returns(_p2.Object);
            _gameState.Setup(gs => gs.CurrentPlayer).Returns(_p1.Object);
            _gameState.Setup(gs => gs.Turn).Returns(It.IsAny<int>);

            GameEngine.Initialize(_p1.Object, _p2.Object, _gameState.Object);
        }

        [TearDown]
        public void TearDown()
        {
            GameEngine.Uninitialize();
        }

        [Test]
        public void StartGame_NoExceptionIsThrown()
        {
            //Arrange

            //Act
            GameEngine.StartGame(_p1.Object, _p2.Object, GameConstants.DRAW_CARDS_AT_GAME_START);

            //Assert
            _p1.Verify(p => p.Deck.Shuffle(), Times.Once);
            _p2.Verify(p => p.Deck.Shuffle(), Times.Once);
            _p1.Verify(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
            _p2.Verify(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void StartTurn_NoExceptionIsThrown()
        {
            //Arrange

            //Act
            GameEngine.StartTurn();

            //Assert
            _gameState.Verify(gs => gs.CurrentPlayer, Times.Once);
            _p1.Verify(p => p.TotalMana, Times.Exactly(2));
            _p1.Verify(p => p.DrawCard(It.IsAny<bool>()), Times.Once);
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        [TestCase(9, 10)]
        public void StartTurn_TotalManaIsNotAtMax_IncrementTotalMana(int input, int expected)
        {
            //Arrange
            _p1.SetupGet(p => p.TotalMana).Returns(input);

            //Act
            GameEngine.StartTurn();

            //Assert
            _p1.VerifySet(p => p.TotalMana = expected);
        }

        [Test]
        public void StartTurn_TotalManaIsAtMax_TotalManaOnlyInvokedOnce()
        {
            //Arrange
            _p1.SetupProperty(p => p.TotalMana, GameConstants.TOTAL_MANA);

            //Act
            GameEngine.StartTurn();

            //Assert
            _p1.Verify(p => p.TotalMana, Times.Once);
            Assert.That(_p1.Object.TotalMana, Is.EqualTo(GameConstants.TOTAL_MANA));
        }

        [Test]
        public void StartTurn_CardWithTurnStartEvent_ValueIsSetToTrue()
        {
            //Arrange
            var value = false;

            var card = new Mock<ICard>(MockBehavior.Strict);
            card.Setup(c => c.Id).Returns(1);
            card.Setup(c => c.PlayOrder).Returns(1);

            GameEventManager.RegisterForEventTurnStart(card.Object, (player) => value = true);

            //Act
            GameEngine.StartTurn();

            //Assert
            Assert.That(value, Is.True);
        }
    }
}
