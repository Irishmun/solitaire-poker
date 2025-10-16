using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SolitairePoker.Poker;

namespace SolitairePoker.UI
{
    public abstract class ButtonBase
    {
        /// <summary>
        /// Sprite of the button to draw
        /// </summary>
        public ToggleSprite Sprite { get; set; }

        public ButtonBase(Texture2D texture)
        {
            Sprite = new ToggleSprite(texture);
        }

        public abstract void ClickHandButton(CardDeck deck, Logic logic);
        public abstract void ClickButton();
    }
}
