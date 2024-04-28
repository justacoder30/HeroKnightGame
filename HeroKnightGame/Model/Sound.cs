using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HeroKnightGame
{
    public class Sound
    {
        public SoundEffect sound;
        public string name; 
        public SoundEffectInstance soundEffectInstance;
        public float volume;

        public Sound(string name, SoundEffect sound)
        {
            this.name = name;
            this.sound = sound;
            soundEffectInstance = sound.CreateInstance();
        }

        public void CreateInstance()
        {
            soundEffectInstance = sound.CreateInstance();
        }
    }
}
