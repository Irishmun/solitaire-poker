using Microsoft.Xna.Framework.Graphics;
using SolitairePoker.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker.UI
{
    public class PlayHandButton : ButtonBase
    {
        public PlayHandButton(Texture2D texture) : base(texture)
        {
        }

        public override void ClickButton()
        {
            return;
        }

        public override void ClickHandButton(CardDeck deck)
        {
            Card[] selected = deck.GetSelectedCards();
            if (selected.Length == 0)
            { return; }

            //play hand 
            //discard played cards
            //draw new cards

            deck.DiscardCards(selected);
        }
    }
}
