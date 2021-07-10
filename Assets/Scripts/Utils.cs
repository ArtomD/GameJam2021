using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    public static AudioSource AddAudioNoFalloff(GameObject gameObject, AudioClip clip, bool loop, bool playAwake, float vol, float pitch)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        newAudio.pitch = pitch;
        return newAudio;
    }
}
