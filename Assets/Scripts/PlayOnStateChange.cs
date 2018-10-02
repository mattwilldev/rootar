using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStateChange : MonoBehaviour {

	public AudioClip appearClip;
	public AudioClip disappearClip;

	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
		audioSource.enabled = false;
	}

	public void Initialize() {
		audioSource.enabled = true;
	}

	public void playOnAppear() {
		audioSource.clip = appearClip;
		audioSource.Play();
	}

	public void playOnDisappear() {
		audioSource.clip = disappearClip;
		audioSource.Play();
	}
}

