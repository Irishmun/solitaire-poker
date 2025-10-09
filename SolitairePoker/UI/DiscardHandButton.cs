using Microsoft.Xna.Framework.Graphics;
using SolitairePoker.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker.UI
{
    internal class DiscardHandButton : ButtonBase
    {
        public DiscardHandButton(Texture2D texture) : base(texture)
        {
        }

        public override void ClickButton()
        {
            return;
        }

        public override void ClickHandButton(CardDeck deck)
        {
            deck.DiscardCards(deck.GetSelectedCards());
        }
    }
}
