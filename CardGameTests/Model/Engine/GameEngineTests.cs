
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
            _p1.Setup(p => p.CurrentMana).Returns(0);
            _p1.Setup(p => p.Deck.Shuffle());
            _p1.Setup(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>())).Returns(new List<ICard>());

            _p2 = new Mock<IPlayer>(MockBehavior.Strict);
            _p2.Setup(p => p.Id).Returns(1);
            _p2.Setup(p => p.CurrentMana).Returns(0);
            _p2.Setup(p => p.Deck.Shuffle());
            _p2.Setup(p => p.DrawCards(It.IsAny<int>(), It.IsAny<bool>())).Returns(new List<ICard>());

            _gameState = new Mock<IGameState>(MockBehavior.Strict);

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


    }
}
