using Microsoft.Xna.Framework.Audio;

namespace HeroKnightGame
{
    public static class Sound
    {
        public static SoundEffect Attack_Sound;
        public static SoundEffect Landing_Sound;

        public static void Intit()
        {
            Attack_Sound = Globals.Content.Load<SoundEffect>("SoundFX/attack_sound");
            Landing_Sound = Globals.Content.Load<SoundEffect>("SoundFX/landing_sound");
        } 
    }
}
