using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util
{
    public static class EasingUtil
    {
        //https://stackoverflow.com/a/25730573

        public static float EaseInQuad(float t)
        {
            if (t <= 0.5f)
            {
                return 2.0f * t * t;
            }
            t -= 0.5f;
            return 2.0f * t * (1.0f - t) + 0.5f;
        }

        public static float BezierBlend(float t)
        {
            return t * t * (3.0f - 2.0f * t);
        }

        public static float ParametricBlend(float t)
        {
            float sqr = t * t;
            return sqr / (2.0f * (sqr - t) + 1.0f);
        }
    }
}
