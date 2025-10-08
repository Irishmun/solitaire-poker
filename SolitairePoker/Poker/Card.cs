using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker.Poker
{
    public struct Card
    {
        private string fileName;
        private SuitEnum suit;
        private FaceEnum face;
        private Sprite sprite;

        public string FileName { get => fileName; set => fileName = value; }
        public SuitEnum Suit { get => suit; set => suit = value; }
        public FaceEnum Face { get => face; set => face = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
        public bool Selected { get; set; }

        public override string ToString()
        {
            return $"{face} of {suit} ({fileName})";
        }
    }
}
