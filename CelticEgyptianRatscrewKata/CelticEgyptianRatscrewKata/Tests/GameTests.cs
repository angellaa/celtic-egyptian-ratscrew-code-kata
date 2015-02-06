using CelticEgyptianRatscrewKata.SnapRules;
using Moq;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    class GameTests
    {
        [Test]
        public void NewGame()
        {
            var dealer = new Dealer();
            
            var randomGenerator = new Mock<IRandomNumberGenerator>();
            randomGenerator.Setup(x => x.Get(0, It.IsAny<int>())).Returns(0);

            var shuffler = new Shuffler(randomGenerator.Object);
            var deck = Cards.With(
                new Card(Suit.Clubs, Rank.Three),
                new Card(Suit.Diamonds, Rank.Three)
                );

            var rule = new Mock<IRule>();
            rule.Setup(x => x.CanSnap(It.IsAny<Cards>())).Returns(true);

            var game = new Game(dealer, shuffler, new[] {rule.Object}, deck);

            var player1 = new Player();
            var player2 = new Player();
            player1.Join(game);
            player2.Join(game);

            game.Start();

            player1.LayCard();
            player2.LayCard();
            player2.CallSnap();

            Assert.AreEqual(player2, game.Winner());
            Assert.IsEmpty(player1.Pile);
            Assert.AreEqual(deck, player2.Pile);
            Assert.IsEmpty(game.Stack);
        }
    }
}
