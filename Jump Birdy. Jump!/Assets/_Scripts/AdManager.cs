using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    public static AdManager instance;


    // Use this for initialization
    void Start() {
        instance = this;
        Scene scena = SceneManager.GetActiveScene();
        Advertisement.Initialize("a0c25659-59a4-40ef-9ed3-634c98926b66");
        //print ("Test mode: " + Application.isEditor);
        if (scena.buildIndex == 0)
            MenuManager.instance.txtCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
    }
    void Update() {

    }

    public void ShowAdWhenReady() {
        UnityEngine.SceneManagement.Scene xd = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0);
        if (xd.buildIndex == 0) {
            ShowRewardedAd();

            MenuManager.instance.mainMenuGO.SetActive(false);
            return;
        } else if (GameManager.instance.score > 50)
            ShowRewardedAd();
        else if (DontDestroyOnLoadScript.low == 0) {
            ShowRewardedAd();
            DontDestroyOnLoadScript.low = 3;
        } else if (DontDestroyOnLoadScript.medium == 0) {
            ShowRewardedAd();
            DontDestroyOnLoadScript.medium = 2;
        }
    }

    public void ShowRewardedAd() {
        if (Advertisement.IsReady()) {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 2);
                MenuManager.instance.txtCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
                MenuManager.instance.mainMenuGO.SetActive(true);
                break;
            case ShowResult.Skipped:
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                MenuManager.instance.txtCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
                MenuManager.instance.mainMenuGO.SetActive(true);
                Debug.Log("The ad was skipped before reaching the end.");
                MenuManager.instance.mainMenuGO.SetActive(true);
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
