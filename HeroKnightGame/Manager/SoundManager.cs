using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace HeroKnightGame.Manager
{
    public static class SoundManager
    {
        static List<Sound> sounds;
        static SoundEffectInstance _instance;
        public static Song Background_music;

        public static void Init()
        {
            sounds = new List<Sound>();

            sounds.Add( new Sound("Attack_sound", Globals.Content.Load<SoundEffect>("SoundFX/attack_sound")));
            sounds.Add(new Sound("Landing_sound", Globals.Content.Load<SoundEffect>("SoundFX/landing_sound")));
            sounds.Add(new Sound("Footstep_sound", Globals.Content.Load<SoundEffect>("SoundFX/footstep_sound")));

            Background_music = Globals.Content.Load<Song>("Music/Background_music");
        }

        public static void PlaySound(string Name, float volume = 1f, bool loop = false)
        {
            foreach (var sound in sounds)
            {
                if(sound.name == Name)
                {
                    if (!loop) sound.CreateInstance();
                    RunSound(sound, sound.soundEffectInstance, volume);
                }
            }
        }

        private static void RunSound(Sound sound, SoundEffectInstance instance, float volume)
        {
            _instance = instance;

            _instance.Play();
            _instance.Volume = volume;   
        }

        public static void StopSound()
        {
            if(_instance != null) _instance.Stop();
        }

        public static void PlayMusic()
        {
            MediaPlayer.Play(Background_music);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
        }
    }
}
