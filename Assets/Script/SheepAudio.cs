using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SheepAudio : MonoBehaviour {

    private AudioSource source;
	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Sounds/sheep_me");
    }
    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Floor" && !source.isPlaying)
        {
            source.Play();
        }
    }
}
