using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HeroKnightGame
{
    public static class Sound
    {
        public static SoundEffect Attack_Sound;
        public static SoundEffect Landing_Sound;
        public static SoundEffect Stepping_Sound;
        public static Song Background_music;

        public static void IntitSound()
        {
            Attack_Sound = Globals.Content.Load<SoundEffect>("SoundFX/attack_sound");
            Landing_Sound = Globals.Content.Load<SoundEffect>("SoundFX/landing_sound");
            Stepping_Sound = Globals.Content.Load<SoundEffect>("SoundFX/footstep_sound");
            Background_music = Globals.Content.Load<Song>("Music/Background_music");
        }

        public static void PlayMusic()
        {
            MediaPlayer.Play(Background_music);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
        }
    }
}
