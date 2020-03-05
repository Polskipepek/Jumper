using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class OrthoSmoothFollow : MonoBehaviour {

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public bool rot;
    public Transform target;
    public GameObject ballie;
    public Sprite wesola;
    Scene scena;
    bool bylo = false;
    void Start() {
         scena = SceneManager.GetActiveScene ();
        //print (scena.name);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (target) {
            Camera camera = Camera.main;
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            if (rot) {
                transform.rotation = target.rotation;
            }
            if (scena.name == "Finishxd") {
                bylo = true;
                StartCoroutine (ZmienBuzke ());
            }

        }
        if (PlayerMB.instance && PlayerMB.instance.transform.position.y > 321f)
            SceneManager.LoadScene (2);
                
    }
    void LateUpdate() {
       // transform.position = new Vector3(Mathf.Clamp(transform.position.x, 9.5f, GameManager.Mapa.sizeX - 9.5f), Mathf.Clamp(transform.position.y, 5.5f, GameManager.Mapa.sizeY - 5.5f), transform.position.z);
    }
    IEnumerator ZmienBuzke () {
        yield return new WaitForSeconds (0.5f);
        ballie.GetComponent<SpriteRenderer> ().sprite = wesola;

    }
    
}