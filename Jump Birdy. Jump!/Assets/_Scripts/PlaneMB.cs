using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMB : MonoBehaviour {
    float jumpForce = 9f;
    public AudioClip bounce;
    
    public bool alreadyUsed = false, ruszam = false, poczatkowe = true;
    float startX;
    Sprite[] sprites;
    public Sprite grass0, grass1, grass2, underG;
    float deltaMovement = 0;
    bool goLeft = true;
    public int index = 0;

    void Start () {
        startX = transform.position.x;
        sprites = new Sprite[] { grass0, grass1, grass2 };
        LosujSpritePlatformy ();
        RuchPlatformy ();
    }

    void Update () {
        if (ruszam) {
            //print ("xd");
            if (goLeft) {
                transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (transform.position.x - deltaMovement, transform.position.y), Time.deltaTime);
            } else {
                transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (transform.position.x + deltaMovement, transform.position.y), Time.deltaTime);
            }
            if (transform.position.x <= startX - deltaMovement)
                goLeft = false;
            else if (transform.position.x >= startX + deltaMovement)
                goLeft = true;
        }
    }

    void Bounce (Rigidbody rb, float power) {
        //  if (!alreadyUsed)
        // GameManager.instance.Scoring ();
        // print (coll.collider.name);
        Vector2 velocity = rb.velocity;
        //print (" bounce kurwa");
        //rb.velocity = new Vector2(1000f, 100f);
        velocity.y = power;
        rb.velocity = velocity;
        rb.AddTorque (Random.Range (-50f, 50f), Random.Range (-50f, 50f), Random.Range (-50f, 50f));
        alreadyUsed = true;
        GameHelper.lastIndex = index;
        //power = jumpForce;
        if (PlayerMB.instance.realPos.y > 7.5f){
            poczatkowe = false;
            GameManager.instance.ground.SetActive(false);
        }
        if (!poczatkowe)
            DestroyPlane (GameManager.instance.planes[0].gameObject);

    }



    private void OnTriggerEnter (Collider coll) {
        Rigidbody rb = coll.GetComponent<Rigidbody> ();
        float tempY = coll.transform.position.y;
        /* print (PlayerMB.instance.transform.position.y + " > " + transform.position.y);
         print ("Platforma: " + "platforma: " + tag == "Platforma");
         print ("Pozycja: " + (PlayerMB.instance.realPos.y > transform.position.y));*/


        if (PlayerMB.instance.realPos.y > transform.position.y && tag == "Platforma" && PlayerMB.instance.realPos.x >= transform.position.x - 1.14f &&
            PlayerMB.instance.realPos.x <= transform.position.x + 1.14f || gameObject.transform.name == "BoardGround") {

            SoundsPlayer.instance.SoundPlayer (bounce);
            if (GameManager.instance.maxY < tempY) {
                if (!alreadyUsed)
                   GameManager.instance.Scoring (Scoring());
                GameManager.instance.maxY = transform.position.y;
            }
            if (PlayerMB.instance.realPos.y > 10f && alreadyUsed) {
                PlayerMB.instance.gameOverKurwa = true;
                //GameManager.instance.StopCoroutine (GameManager.instance.spawnPlaneCR);
                GameManager.instance.StartCoroutine (GameManager.instance.GameOver ());
            }
            Bounce (rb, powerJump ());
        }
    }

    public void RuchPlatformy () {
        if (gameObject.transform.name == "BoardGround")
            return;
        float szansa = Random.Range (0f, 1f);
        if (szansa > 0.65f) {
            deltaMovement = Random.Range (0.5f, 1.5f);
            ruszam = true;
        }
    }

    public Sprite LosujSpritePlatformy () {
        if (gameObject.transform.name == "BoardGround") {
            return GetComponent<SpriteRenderer> ().sprite = underG;
        }
        /*print (GetComponent<SpriteRenderer> ());
        print (GetComponent<SpriteRenderer> ().sprite);
        print (sprites);*/
        int ktorySprite = Random.Range (0, 100);
        Sprite wybranySprite = grass0;
        if (ktorySprite > 85)
            wybranySprite = grass1;
        else if (ktorySprite < 10)
            wybranySprite = grass2;

        return GetComponent<SpriteRenderer> ().sprite = wybranySprite;

    }
    public float powerJump () {
        if (GetComponent<SpriteRenderer> ().sprite == grass1)
            return 15f;
        if (GetComponent<SpriteRenderer> ().sprite == grass2)
            return 20f;
        return jumpForce;
    }
    public int Scoring () {
        int mnoznik = 1;
        if (GetComponent<SpriteRenderer>().sprite == grass1)
            mnoznik = 3;
        if (GetComponent<SpriteRenderer>().sprite == grass2)
            mnoznik = 4;
        //print("mnoznik " + mnoznik + "jf " + jumpForce);
        return mnoznik;
    }


    public void DestroyPlane (GameObject gameObject) {
        print (gameObject.name);
        GameManager.instance.planes.Remove (this);
        //Destroy (gameObject);
        gameObject.SetActive(false);

    }

}

