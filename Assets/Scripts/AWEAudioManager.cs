using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWEAudioManager: MonoBehaviour {

	private static AWEAudioManager _instance = null;

	// Audio players components.
	public AudioSource effectSource = null;
	public AudioSource musicSource = null;

	public static AWEAudioManager Instacne {

		get { 
			if (_instance == null) {
				_instance = new AWEAudioManager ();
			}

			return _instance;
		}
	}

	private void Awake() {

		if (_instance == null) {
			
			_instance = this;
			DontDestroyOnLoad (gameObject);
		} 
	}

	public void PlaySoundByName(string name) {

		AudioClip clip = Resources.Load<AudioClip>("Audios/" + name);

		if (effectSource && clip) {

			if (effectSource.isPlaying)
			{
				effectSource.Stop();
			}

			effectSource.clip = clip;
			effectSource.Play();
		}

	}

	// Play a single clip through the sound effects source.
	public void PlaySound(AudioClip clip)
	{
		if (effectSource) {
			effectSource.clip = clip;
			effectSource.Play();
		}

	}

	public void PlayMusicByName(string name) {
	
		AudioClip clip = Resources.Load<AudioClip>("Audios/" + name);

		if (musicSource && clip) {
			
			if (musicSource.isPlaying)
			{
				musicSource.Stop();
			}

			musicSource.clip = clip;
			musicSource.Play();

		}

	}

	public void PlayMusic(AudioClip clip)
	{
		if (musicSource) {
			musicSource.clip = clip;
			musicSource.Play ();
		}

	}

	public void PauseMusic()
	{
		if (musicSource) {
			musicSource.Pause ();
		}
	}

	public void ResumeMusic()
	{
		if (musicSource) {
			musicSource.Play ();
		}
	}
}
