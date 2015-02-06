using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CelticEgyptianRatscrewKata.SnapRules;

namespace CelticEgyptianRatscrewKata
{
    internal class Game
    {
        private readonly Dealer m_Dealer;
        private readonly Shuffler m_Shuffler;
        private readonly IEnumerable<IRule> m_Rules;
        private readonly Cards m_Deck;
        private readonly List<Player> m_Players;
        private readonly SnapValidator m_SnapValidator;

        public Cards Stack { get; set; }

        public Game(Dealer dealer, Shuffler shuffler, IEnumerable<IRule> rules, Cards deck)
        {
            m_Dealer = dealer;
            m_Shuffler = shuffler;
            m_Rules = rules;
            m_Deck = deck;
            m_Players = new List<Player>();
            m_SnapValidator = new SnapValidator();
            Stack = Cards.Empty();
        }

        public void AddPlayer(Player player)
        {
            m_Players.Add(player);
        }

        public void Start()
        {
            var hands = m_Dealer.Deal(m_Players.Count, m_Shuffler.Shuffle(m_Deck));

            var stack = new Queue<Cards>(hands);
            foreach (var player in m_Players)
            {
                player.Pile = stack.Dequeue();
            }
        }

        public void LayCard(Card card)
        {
            Stack.AddToTop(card);
        }

        public Cards Snap()
        {
            if (m_SnapValidator.CanSnap(Stack, m_Rules))
            {
                var stack = Stack;
                Stack = Cards.Empty();

                return stack;
            }

            return Cards.Empty();
        }

        public Player Winner()
        {
            return m_Players.SingleOrDefault(player => player.Pile.Equals(m_Deck));
        }
    }
}