using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour {
    public static SoundsPlayer instance;
    public AudioClip AC;
    public AudioSource AS;
    public AudioClip gameOverAC;
    void Start () {
        instance = this;
        AS = GetComponent<AudioSource> ();
	}
    void Awake () {
        if (instance != null)
            Destroy (gameObject);
        else {
            instance = this;
            GameObject.DontDestroyOnLoad (gameObject);
        }
    }

    void Update () {
		
	}
    public void SoundPlayer (AudioClip ac) {
        AS.PlayOneShot (ac);
    }
}
