 using UnityEngine;
 using System.Collections;
using Unity.VisualScripting;

public class PlayRandomSound : MonoBehaviour {
 
     public AudioSource AudioSourceForPlayingSoundIn;
     public AudioClip[] audioSources;

     public int clipDelay = 5;

    private void Awake()
    {
        if (AudioSourceForPlayingSoundIn == null)
        {
            if(!TryGetComponent(out AudioSourceForPlayingSoundIn))
            {
                AudioSourceForPlayingSoundIn = gameObject.AddComponent<AudioSource>();
            }
        }
    }
    // Use this for initialization
    void Start () {
 
         StartAudio (0);
     }
 
 
     void StartAudio(float ClipLength)
     {
         Invoke ("RandomSoundness", clipDelay + ClipLength);
     }
 
     void RandomSoundness()
     {
        AudioClip clip = audioSources[Random.Range(0, audioSources.Length)];
        AudioSourceForPlayingSoundIn.clip = clip;
        AudioSourceForPlayingSoundIn.Play ();
         StartAudio (clip.length);
     }
 }