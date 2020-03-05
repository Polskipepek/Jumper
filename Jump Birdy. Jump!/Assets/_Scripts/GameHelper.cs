using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHelper : MonoBehaviour {

    public static int lastIndex;
    public static GameHelper instance;
    public PlaneMB nextPlane;
    public Image leftArrow, rightArrow;
    bool? left;
    public GameObject rotate, r1;
    float t = 10f;
    bool startPlanes = false;
    public bool? Left {
        get {
            return left;
        }

        set {
            if (value != left) {
                left = value;
                leftArrow.gameObject.SetActive (false);
                rightArrow.gameObject.SetActive (false);
                if (left == true)
                    leftArrow.gameObject.SetActive (true);
                else if (left == false)
                    rightArrow.gameObject.SetActive (true);
            }
        }
    }

    void Start () {
        instance = this;

        nextPlane = GameManager.instance.planes[0];

    }

    private void FixedUpdate () {
        ZnajdzNastepny (PlayerMB.instance.realPos.y);
        //print (PlayerMB.instance.realPos.y);
    }

    void Update () {
        if (!PlayerMB.instance.gameOverKurwa && rotate.gameObject.activeSelf == true && nextPlane != null && nextPlane.transform.position.y < 25f) {
            rotate.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0, 50 * Mathf.Sin ((t += Time.deltaTime)), -45f);
        } else {
            //Destroy (rotate.gameObject);
            rotate.gameObject.SetActive (false);
            r1.gameObject.SetActive (false);

        }

        OnScreenBool ();
        if (PlayerMB.instance.gameOverKurwa) {
            leftArrow.gameObject.SetActive (false);
            rightArrow.gameObject.SetActive (false);
        }
    }
    public PlaneMB ZnajdzNastepny (float y) {
        if (GameManager.instance.planes.Count < 6)
            return null;
        for (int i = 1; i < 6; i++) {
            //print ("sprawdzam: " + GameManager.instance.planes[i].index);
            if (GameManager.instance.planes[i].transform.position.y >= y && GameManager.instance.planes[i].alreadyUsed == false) {
                nextPlane = GameManager.instance.planes[i];
                //nextPlane.GetComponent<SpriteRenderer> ().color = Color.green;
                if (i >= 2)
                    //GameManager.instance.planes[i-1].GetComponent<SpriteRenderer> ().color = Color.white;
                return GameManager.instance.planes[i];
            } else {
                if (y > GameManager.instance.planes[i].transform.position.y + 7.5f )
                    GameManager.instance.planes[i].DestroyPlane (GameManager.instance.planes[i].gameObject);
            }
        }
        return null;
    }

    public bool OnScreenBool () {
        if (nextPlane == null)
            return false;

        Vector3 toScreen = Camera.main.WorldToScreenPoint (nextPlane.transform.position);
        if (toScreen.x < 0 || toScreen.x > Screen.width) {
            Left = toScreen.x < 0;
            return false;
        } else
            Left = null;
        return true;
    }


}
