using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour {

    DontDestroyOnLoadScript instance;
    
    public static int low = 3, medium = 2;
    // Use this for initialization
    void Start () {
        instance = this;
	}

    void Awake () {
        if (instance != null)
            Destroy (gameObject);
        else {
            instance = this;
            GameObject.DontDestroyOnLoad (gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
