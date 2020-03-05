using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public static MusicPlayer instance = null;
    public AudioSource AS;
    public AudioClip music1, music2, music3;

    void Awake () {
        if (instance != null)
            Destroy (gameObject);
        else {
            instance = this;
            GameObject.DontDestroyOnLoad (gameObject);
        }
    }



	void Start () {
        AS.PlayOneShot(AS.clip = music1);
	}
	
	void Update () {
		
	}
}
