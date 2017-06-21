using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;
using Moq;
using NUnit.Framework;

namespace CardGameTests.UnitTests.Model.Engine
{
    internal class SampleMinionTestSuperClass : Minion
    {
        public override void OnTurnStart(IPlayer player)
        {
            player.Id = 100;
        }
    }

    internal class SampleMinionTestExtendedClass : SampleMinionTestSuperClass
    {
        public override void OnTurnStart(IPlayer player)
        {
            player.Id = 200;
        }
    }

    [TestFixture]
    public class GameEventRegistratorTests
    {
        private Mock<IMinion> _triggerable;
        private Mock<IPlayer> _player;
        private int _playerId = 1;
        private int _callbackValue = 1;

        [SetUp]
        public void Setup()
        {
            _player = new Mock<IPlayer>();
            _player.Setup(p => p.Id).Returns(_playerId);

            _triggerable = new Mock<IMinion>(MockBehavior.Strict);
            _triggerable.Setup(t => t.PlayOrder).Returns(1);
            _triggerable.Setup(t => t.OnTurnStart(_player.Object)).Callback(() => _callbackValue++);
        }

        [Test]
        public void RegisterEvents_CallingClassDifferentThanBaseClass_AttachesEvent()
        {
            //Arrange
            GameEventManager.Initialize();
            var temp = _callbackValue;

            //Act
            GameEventRegistrator.RegisterEvents(_triggerable.Object, Minion.GetMinionType());
            GameEventManager.OnTurnStart(_player.Object);

            //Assert
            Assert.That(_callbackValue, Is.EqualTo(temp + 1));
        }

        [Test]
        public void RegisterEvents_CallingClassDoesNotImplementEvent_DoesNotAttachEventAndPlayerIdIsNotChanged()
        {
            //Arrange
            GameEventManager.Initialize();
            var sampleMinion = new SampleMinionTestExtendedClass();

            //Act
            GameEventRegistrator.RegisterEvents(sampleMinion, typeof(SampleMinionTestSuperClass));
            GameEventManager.OnTurnEnd(_player.Object);

            //Assert
            Assert.That(_player.Object.Id, Is.EqualTo(_playerId));
        }
    }
}