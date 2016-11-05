using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Engine;
using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;
using Moq;
using NUnit.Framework;

namespace CardGameTests.UnitTests.Model.Engine
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
            _p1.Setup(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>())).Returns(It.IsAny<List<ICard>>());
            _p1.SetupProperty(p => p.CardsInPlay, new List<ICard>());

            _p2 = new Mock<IPlayer>(MockBehavior.Strict);
            _p2.Setup(p => p.Id).Returns(1);
            _p2.Setup(p => p.Name).Returns("P2");
            _p2.Setup(p => p.CurrentMana).Returns(0);
            _p2.Setup(p => p.Deck.Shuffle());
            _p2.Setup(p => p.TotalMana).Returns(It.IsAny<int>);
            _p2.SetupProperty(p => p.TotalMana, It.IsAny<int>());
            _p2.Setup(p => p.DrawCard(It.IsAny<bool>())).Returns(It.IsAny<ICard>());
            _p2.Setup(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>())).Returns(new List<ICard>());
            _p2.SetupProperty(p => p.CardsInPlay, new List<ICard>());

            _gameState = new Mock<IGameState>(MockBehavior.Strict);
            _gameState.Setup(gs => gs.Player).Returns(_p1.Object);
            _gameState.Setup(gs => gs.Opponent).Returns(_p2.Object);
            _gameState.Setup(gs => gs.CurrentPlayer).Returns(_p1.Object);
            _gameState.Setup(gs => gs.Turn).Returns(It.IsAny<int>);
            _gameState.Setup(gs => gs.SwapCurrentPlayer());
            _gameState.Setup(gs => gs.IncrementTurn());

            GameEngine.Initialize(_p1.Object, _p2.Object, _gameState.Object);
        }

        [TearDown]
        public void TearDown()
        {
            GameEngine.Uninitialize();
        }

        [Test]
        public void RngRandom_GetRandom_ReturnsRandomInt()
        {
            //Arrange
            int i;

            //Act
            i = GameEngine.RngRandom.Next(1,1);

            //Assert
            Assert.That(i, Is.EqualTo(1));
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

        [Test]
        public void EndTurn_CardWithTurnEndEvent_ValueIsSetToTrue()
        {
            //Arrange
            var value = false;

            var card = new Mock<ICard>(MockBehavior.Strict);
            card.Setup(c => c.Id).Returns(1);
            card.Setup(c => c.PlayOrder).Returns(1);

            GameEventManager.RegisterForEventTurnEnd(card.Object, (player) => value = true);

            //Act
            GameEngine.EndTurn();

            //Assert
            Assert.That(value, Is.True);
        }

        [Test]
        public void EndTurn_SwapCurrentPlayerAndIncrementTurnAreInvoked()
        {
            //Arrange

            //Act
            GameEngine.EndTurn();

            //Assert
            _gameState.Verify(gs => gs.SwapCurrentPlayer(), Times.Once);
            _gameState.Verify(gs => gs.IncrementTurn(), Times.Once);
        }

        [Test]
        public void EndTurn_CardsInPlayContainsMinions_ResetTemporaryBuffers()
        {
            //Arrange
            var m1 = new Mock<IMinion>();
            m1.Setup(m => m.ResetTemporaryAttackBuff());
            m1.Setup(m=>m.ResetTemporaryHealthBuff());
            var cardsInPlay = new List<ICard>(1) {m1.Object};
            _p1.Setup(p => p.CardsInPlay).Returns(cardsInPlay);

            //Act
            GameEngine.EndTurn();

            //Assert
            m1.Verify(m => m.ResetTemporaryAttackBuff(), Times.Once);
            m1.Verify(m => m.ResetTemporaryHealthBuff(), Times.Once);
        }

        [Test]
        public void PlayCard_NotPlayersTurn_ThrowInvalidOperationException()
        {
            //Arrange
            var card = new Mock<ICard>(MockBehavior.Strict);
            card.Setup(c => c.Id).Returns(1);
            card.Setup(c => c.PlayerOwner).Returns(_p1.Object);

            _gameState.Setup(gs => gs.CurrentPlayer).Returns(_p2.Object);

            //Act
            
            //Assert
            Assert.Throws<InvalidOperationException>(() => GameEngine.PlayCard(_p1.Object, card.Object, -1, null));
        }

        [Test]
        public void PlayCard_PlayersTurn_NoExceptionThrownAndPlayerPlaysCard()
        {
            //Arrange
            var card = new Mock<ICard>(MockBehavior.Strict);
            card.Setup(c => c.Id).Returns(1);
            card.Setup(c => c.PlayerOwner).Returns(_p1.Object);
            card.Setup(c => c.PlayCard(It.IsAny<int>(), It.IsAny<IDamageable>()));

            _gameState.Setup(gs => gs.CurrentPlayer).Returns(_p1.Object);

            //Act
            GameEngine.PlayCard(_p1.Object, card.Object, -1, null);

            //Assert
            card.Verify(c => c.PlayCard(-1, null), Times.Once);
        }
    }
}
