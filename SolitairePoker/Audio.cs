using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitairePoker
{
    public class Audio
    {
        private SoundEffect[] _shuffleEffects;
        private SoundEffect[] _drawEffects;

        private Random _rng;

        private bool _isMuted = false;


        public void LoadContent()
        {
            _rng = new Random((int)DateTime.Now.Ticks);
            List<SoundEffect> drawEffects = new List<SoundEffect>();
            List<SoundEffect> shuffleEffects = new List<SoundEffect>();
            SoundEffect eff;

            eff = Core.Content.Load<SoundEffect>("audio/CARD_MOVE_1");
            if (eff != null)
            { drawEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/CARD_MOVE_2");
            if (eff != null)
            { drawEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/CARD_MOVE_3");
            if (eff != null)
            { drawEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/CARD_MOVE_4");
            if (eff != null)
            { drawEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/CARD_MOVE_5");
            if (eff != null)
            { drawEffects.Add(eff); }

            eff = Core.Content.Load<SoundEffect>("audio/Cards_Shuffle_1");
            if (eff != null)
            { shuffleEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/Cards_Shuffle_2");
            if (eff != null)
            { shuffleEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/Cards_Shuffle_3");
            if (eff != null)
            { shuffleEffects.Add(eff); }
            eff = Core.Content.Load<SoundEffect>("audio/Cards_Shuffle_4");
            if (eff != null)
            { shuffleEffects.Add(eff); }

            _drawEffects = drawEffects.ToArray();
            _shuffleEffects = shuffleEffects.ToArray();
        }

        public void PlayShuffleSound()
        {
            if (_isMuted)
            { return; }
            _shuffleEffects[_rng.Next(_shuffleEffects.Length)].Play();
        }

        public void PlayDrawSound()
        {
            if (_isMuted)
            { return; }
            _drawEffects[_rng.Next(_drawEffects.Length)].Play();
        }
        public bool IsMuted { get => _isMuted; set => _isMuted = value; }
    }
}
