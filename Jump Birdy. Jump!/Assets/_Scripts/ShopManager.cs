using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    public Text Coins;
    public Text LuckLvl, LuckCost;

    public void ShopOpening () {
        SceneManager.LoadScene (2);
    }

    void Start () {
        Coins.text = "" + PlayerPrefs.GetInt ("Coins");
        LuckLvl.text = "lvl " + (PlayerPrefs.GetInt ("LuckLvl") + 1) + "";
        LuckCost.text = ObliczWartosc (1, 1, 10) + " coins";
        PlayerPrefs.SetFloat ("LuckLvlMnoznik", 1.1f);
        PlayerPrefs.SetInt ("LuckLvlStartowe", 10);
        PlayerPrefs.SetInt ("LuckLvlPoziom", 0);
        //if(PlayerPrefs.GetInt("LuckLvl") == null)
        PlayerPrefs.GetInt ("luckLvl");
    }

    // Update is called once per frame
    void Update () {

    }


    double ObliczWartosc (int poziom, double mnoznik, int startowe) {
        double cena = poziom + (mnoznik * startowe) * poziom / 2;
        return cena;

    }
    public void ToMenu () {
        MusicPlayer.instance.AS.Stop ();
        SceneManager.LoadScene (0);
        MusicPlayer.instance.AS.PlayOneShot (MusicPlayer.instance.AS.clip = MusicPlayer.instance.music1);
    }

    public void Ulepsz (string nazwa) {
        nazwa = "LuckLvl";
        if (PlayerPrefs.GetInt ("Coins") >= ObliczWartosc (PlayerPrefs.GetInt (nazwa + "Poziom"), PlayerPrefs.GetFloat ("luckLvlMnoznik"), PlayerPrefs.GetInt ("luckLvlStartowe"))){
            PlayerPrefs.SetInt (nazwa + "Poziom", PlayerPrefs.GetInt (nazwa + "Poziom") + 1);
            LuckCost.text = ObliczWartosc (PlayerPrefs.GetInt (nazwa + "Poziom"), PlayerPrefs.GetFloat ("luckLvlMnoznik"), PlayerPrefs.GetInt ("luckLvlStartowe")) + "";
            LuckLvl.text = PlayerPrefs.GetInt (nazwa + "Poziom") + "";

        }
    }

}
