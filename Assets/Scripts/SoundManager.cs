using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    public enum Sound {
        CorrectAnswer,
        WrongAnswer,
        HealthyReceiveDamage,
        HealthyDied,
        Shoot,
        UnhealthyReceiveDamage,
        UnhealthyDied,
        GameWon,
        GameLose,
        PlaceObject,
        Pop
    }

    public static void PlaySound(Sound sound){
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach (GameManager.SoundAudioClip soundAudioClip in GameManager.GetInstance().soundAudioClipArray) {
            if (soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Sound" + sound + " not found!");
        return null;
    }

}
