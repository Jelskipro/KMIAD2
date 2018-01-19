using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAudio : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
    }
	void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.Play(44100);
    }
}
