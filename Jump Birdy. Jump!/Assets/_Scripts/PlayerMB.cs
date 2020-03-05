using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMB : MonoBehaviour {

    public static PlayerMB instance;
    public bool gameOverKurwa = false;
    public Transform modelTr;
    Scene scena;
    public Vector3 realPos {
        get {
            return modelTr.position;
        }
    }

    void Start() {
        instance = this;
        scena = SceneManager.GetActiveScene();
        Time.timeScale = 0.85f;
        StartCoroutine(TimeManager());


    }

    void Update() {
        if (!gameOverKurwa || GameManager.instance.testMode) {
            if (scena.buildIndex == 0 && (realPos.x < -7.5 || realPos.x > -5))
                return;
            transform.Translate(new Vector3(Vector3.right.x * Input.acceleration.x / 5f, 0, 0), Space.World);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            //PC//
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                transform.Translate(new Vector3(Vector3.left.x, 0, 0), Space.World);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                transform.Translate(new Vector3(Vector3.right.x, 0, 0), Space.World);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }

            //GetComponentInChildren<Rigidbody>().AddForce (Vector3.down * 10f * GetComponentInChildren<Rigidbody> ().mass);
            if (scena.name == "Board" && realPos.y < GameManager.instance.maxY - 9f && !gameOverKurwa) {
                gameOverKurwa = true;
                //GameManager.instance.StopCoroutine (GameManager.instance.spawnPlaneCR);
                GameManager.instance.StartCoroutine(GameManager.instance.GameOver());
            }

        }


    }

    IEnumerator TimeManager() {
        while (Time.timeScale < 1.25f) {
            if (GameManager.instance.score == 5)
                Time.timeScale = 1f;
            if (GameManager.instance.score > 15 && GameManager.instance.score < 25)
                Time.timeScale = 1.15f;
            if (GameManager.instance.score > 25)
                Time.timeScale += 0.02f;
            print(Time.timeScale);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

}
