
namespace CelticEgyptianRatscrewKata
{
    internal class Player
    {
        private Game m_Game;

        public Cards Pile { get; set; }

        public void Join(Game game)
        {
            game.AddPlayer(this);
            m_Game = game;
        }

        public void LayCard()
        {
            var card = Pile.Pop();

            m_Game.LayCard(card);
        }

        public void CallSnap()
        {
            var cards = m_Game.Snap();

            Pile.AddToTop(cards);
        }
    }
}