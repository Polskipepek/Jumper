using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {
    public bool testMode;
    public static GameManager instance;
    float poprzedniY = 0f;
    public float maxY = 0f;
    [HideInInspector]
    public int score = 0, tempScore = -1, coins = 0;

    public Text txtScore, txtHighScore, txtBackToMenu;

    public GameObject plane, gameOverGO, bckGameO, ballie, finishGO, ground;
    GameObject planeParent;
    // public PlayerMB player;

    public List<PlaneMB> planes = new List<PlaneMB>();
    public bool finish = false;

    void Start() {
        instance = this;
        planes.Add(GameObject.Find("BoardGround").GetComponent<PlaneMB>());
        planeParent = GameObject.Find("PlanesParent");
        //StartCoroutine (RBStarter ());
        StartCoroutine(SpawnPlane());
        //player = GameObject.Find ("Soccer Ball").GetComponent<PlayerMB>();
        //print (planes[0]);
        if (coins == 0) {
            PlayerPrefs.GetInt("Coins");

        }
    }

    void Update() {

        if (Camera.main.gameObject.GetComponent<OrthoSmoothFollow>().target == ballie && !finish) {
            finish = true;
            finishGO.gameObject.SetActive(true);
            return;
        }

    }


    public IEnumerator SpawnPlane() {
        int i = 1;
        while (poprzedniY < 1000f) {
            PlaneMB temp;
            if (maxY == 0) {
                temp = Instantiate(plane, new Vector2(Random.Range(2f, 3f) - 7.5f, poprzedniY + 2.5f), Quaternion.identity).GetComponent<PlaneMB>();
            } else {
                temp = Instantiate(plane, new Vector2(Random.Range(-3f, 3f) - 7.5f, poprzedniY + 2.5f), Quaternion.identity).GetComponent<PlaneMB>();
            }
            planes.Add(temp);
            temp.index = i++;
            temp.transform.parent = planeParent.transform;
            poprzedniY += 2.5f;
            yield return new WaitForSeconds(0.35f);
        }
        yield return new WaitForSeconds(0.1f);

    }
    public void Scoring(int liczba) {
        if (!PlayerMB.instance.gameOverKurwa) {
            score += liczba;
            txtScore.text = "Your score: " + score;
        }
    }
    public IEnumerator GameOver() {
        if (!testMode || maxY != 220f || !GetComponent<OrthoSmoothFollow>().target == ballie) {
            float tempVolume = MusicPlayer.instance.AS.volume;
            MusicPlayer.instance.AS.volume = 0.1f;
            SoundsPlayer.instance.AS.PlayOneShot(SoundsPlayer.instance.AS.clip = SoundsPlayer.instance.gameOverAC);
            bckGameO.SetActive(true);
            PlayerMB.instance.GetComponentInChildren<Rigidbody>().isKinematic = true;

            while (bckGameO.transform.position.y > Screen.height / 1.95f) {
                bckGameO.transform.position = Vector2.Lerp(new Vector2(Screen.width / 2f, bckGameO.transform.position.y), new Vector2(Screen.width / 2f, Screen.height / 2), Time.deltaTime * 2f);
                yield return new WaitForEndOfFrame();
            }
            AdManager.instance.ShowAdWhenReady();
            //AdManager.instance.ShowAd ();
            PlayerPrefs.SetInt("HighScore", score);
            bckGameO.transform.position = new Vector2(Screen.width / 2f, Screen.height / 2);
            //yield return new WaitForSeconds (0.75f);
            txtHighScore.text += PlayerPrefs.GetInt("HighScore");
            gameOverGO.SetActive(true);
            StartCoroutine(AnimFontSize(txtBackToMenu));
            MusicPlayer.instance.AS.volume = tempVolume;
            planes.Clear();

            //AD//
            if (score < 10)
                DontDestroyOnLoadScript.low--;
            if (score < 30 && score > 10)
                DontDestroyOnLoadScript.medium--;

        }
    }

    IEnumerator AnimFontSize(Text t) {
        while (true) {
            for (int i = 0; i < 25; i++) {
                yield return new WaitForSeconds(0.01f);
                t.fontSize += 1;
            }

            for (int i = 0; i < 25; i++) {
                yield return new WaitForSeconds(0.01f);
                t.fontSize -= 1;
            }
            if (t.fontSize > 400)
                t.fontSize = 200;
        }
    }
    public void Restart() {
        SceneManager.LoadScene(1);
    }
    public void ToMenu() {
        MusicPlayer.instance.AS.clip = null;
        SceneManager.LoadScene(0);
    }

    public void ToShop() {
        SceneManager.LoadScene(2);
    }
    IEnumerator RBStarter() {
        PlayerMB.instance.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        PlayerMB.instance.GetComponent<Rigidbody>().isKinematic = true;

    }
}

