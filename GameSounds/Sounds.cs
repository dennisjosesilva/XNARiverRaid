using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace RiverRaid.GameSounds
{
    public class Sounds
    {
        
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        //Cue sound;


        Dictionary<string, Cue> cues;
        private static Sounds singleton;

        private Sounds()
        {
            Initialize();
            addMusics();
        }

        
        protected void Initialize()
        {
            audioEngine = new AudioEngine("Content\\Sound\\AudioGame.xgs");
            waveBank = new WaveBank(audioEngine, "Content\\Sound\\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content\\Sound\\Sound Bank.xsb");

            cues = new Dictionary<string, Cue>();
        }

        public void Play(string soundName)
        {

            //soundBank.GetCue(soundName).Play();
            //sound = soundBank.GetCue(soundName);
            //sound.Play(); 
            cues[soundName] = soundBank.GetCue(soundName);
            cues[soundName].Play();
        }

        public void Stop(string soundName)
        {
            //soundBank.GetCue(soundName).Pause();
            //sound.Pause();
            //sound.Stop(AudioStopOptions.Immediate);
            //soundBank.GetCue(soundName).Stop(AudioStopOptions.Immediate);

            cues[soundName].Stop(AudioStopOptions.Immediate);
            
        }

        public void Resume(string soundName)
        {
            cues[soundName].Resume();
        }

        public void Pause(string soundName)
        {
            if (!cues[soundName].IsPaused)
            {
                cues[soundName].Pause();
            }
        }
        
        public bool IsPlaying(string soundName)
        {
            return cues[soundName].IsPlaying;
        }

        public bool IsPaused(string soundName)
        {
            return cues[soundName].IsPaused;
        }
        public void StopAll()
        {
            foreach (Cue cue in cues.Values.ToList<Cue>())
            {
                if (cue.IsPlaying)
                {
                    cue.Stop(AudioStopOptions.Immediate);
                }
            }
        }

        public static Sounds getSingleton()
        {
            if (singleton == null)
                singleton = new Sounds();
            return singleton;
        }


        private void addMusics()
        {
            cues.Add("menu1", soundBank.GetCue("menu1"));
            cues.Add("player", soundBank.GetCue("player"));
            cues.Add("explosionPlay", soundBank.GetCue("explosionPlay"));
            cues.Add("fase", soundBank.GetCue("fase"));
            cues.Add("missile", soundBank.GetCue("missile"));
            cues.Add("explosionMissile", soundBank.GetCue("explosionMissile"));
            cues.Add("end", soundBank.GetCue("end"));

        }
    }

}
