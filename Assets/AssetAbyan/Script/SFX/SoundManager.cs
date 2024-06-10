//Author: Small Hedge Games
//Updated: 21/05/2024

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace SHG.SoundManager
{
    public enum SoundType
    {
        Player,
        Vegrant,
        Villager,
        Archer,
        Knight,
        Lemulut,
        Gelapari,
        Widara,
        Arthamerta,
        
    }

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] public SoundList[] soundList;
        public static SoundManager instance;

        private void Awake()
        {
            instance = this;
        }

        public static AudioClip getClip(SoundType sound, int index = 0)
        {
            SoundList soundList = instance.soundList[(int)sound];
            AudioClip[] clips = soundList.sounds;
            if (clips.Length > index){
                AudioClip clip = clips[index];
                return clip;
            }
            return null;
            
        }

        public void Resize()
        {
            string[] names = Enum.GetNames(typeof(SoundType));
            bool differentSize = names.Length != soundList.Length;

            Dictionary<string, AudioClip[]> clips = new();

            if(differentSize)
            {
                for (int i = 0; i < soundList.Length; ++i)
                {
                    if (soundList[i].sounds != null)
                        clips.Add(soundList[i].name, soundList[i].sounds);
                }
            }
            
            Array.Resize(ref soundList, names.Length);
            for (int i = 0; i < soundList.Length; i++)
            {
                string currentName = names[i];
                soundList[i].name = currentName;
                if (soundList[i].volume == 0) soundList[i].volume = 1;

                if (differentSize)
                {
                    if (clips.ContainsKey(currentName))
                        soundList[i].sounds = clips[currentName];
                    else
                        soundList[i].sounds = null;
                }
            }
        }
    }

    [Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        [Range(0, 1)] public float volume;
        public AudioClip[] sounds;
    }
}