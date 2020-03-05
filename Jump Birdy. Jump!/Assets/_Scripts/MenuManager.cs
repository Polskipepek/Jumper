using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;
    public Button play, options, backToMenu;
    public Slider musicVolSlider, soundsVolSlider;
    public GameObject mainMenuGO, optionsGO, creditsGO;
    Text txtPlay, txtToMenu;
    public Text txtCoins;
    public AudioClip bellSound;
    float musicVol, soundsVol;
    float t1 = 0.5f;
    public float MusicVol {
        get {
            return musicVol;
        }

        set {
            musicVol = value;
            MusicPlayer.instance.AS.volume = value;
        }
    }

    public float SoundsVol {
        get {
            return soundsVol;
        }

        set {
            soundsVol = value;
            SoundsPlayer.instance.AS.volume = value;

            if (t1 <= 0.001f)
                SoundsPlayer.instance.SoundPlayer (bellSound);
        }
    }

    void Start () {
        txtPlay = play.gameObject.GetComponentInChildren<Text> ();
        txtToMenu = backToMenu.gameObject.GetComponentInChildren<Text> ();
        StartCoroutine (AnimFontSize (txtPlay));
        //StartCoroutine (AnimFontRotation (txtPlay.gameObject));
        instance = this;

        play = GameObject.Find ("Canvas/MainMenu/BtnPlay").GetComponent<Button> ();

    }

    void Update () {
        t1 -= Time.deltaTime;
        if (t1 < 0)
            t1 = 0.5f;
    }
    public void Options () {
        mainMenuGO.gameObject.SetActive (false);
        optionsGO.gameObject.SetActive (true);
        StopAllCoroutines ();
        StartCoroutine (AnimFontSize (txtToMenu));

    }
    public void MainMenu () {
        optionsGO.gameObject.SetActive (false);
        creditsGO.gameObject.SetActive (false);
        mainMenuGO.gameObject.SetActive (true);
        StopAllCoroutines ();
        txtPlay.transform.rotation = Quaternion.identity;
        StartCoroutine (AnimFontSize (txtPlay));
    }
    public void Credits () {
        mainMenuGO.SetActive (false);
        creditsGO.SetActive (true);
    }


    public IEnumerator AnimFontSize (Text t) {
        float ti = 0;
        while (true) {
            /* for (int i = 0; i < 40; i++) {
                 yield return new WaitForSeconds (0.02f);
                 t.fontSize += 1;
             }

             for (int i = 0; i < 40; i++) {
                 yield return new WaitForSeconds (0.02f);
                 t.fontSize -= 1;
             }*/
             
            t.fontSize = (int)(Mathf.Sin(ti += Time.deltaTime) *50) +80;
            yield return new WaitForSeconds(0.005f);

        }
    }
    IEnumerator AnimFontRotation (GameObject GO) {
        Quaternion currRot;
        float t = 0;
        while (true) {
            currRot = GO.transform.localRotation;
            GO.GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, 10 * Mathf.Sin (t += Time.deltaTime));
            yield return new WaitForEndOfFrame ();
        }
    }

    public void Play () {
        StopAllCoroutines ();
        MusicPlayer.instance.AS.Stop();
        SceneManager.LoadScene (1);
        if (MusicPlayer.instance.AS.clip != MusicPlayer.instance.music2) 
            MusicPlayer.instance.AS.PlayOneShot (MusicPlayer.instance.music2);
    }


}
