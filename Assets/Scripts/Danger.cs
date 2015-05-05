//-----------------------------------------
//   Danger.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 5/5/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour 
{
    public float speedDiff;
    public float destroySeconds;
    public float destroyScore;
    public GameObject explosion;
    public AudioClip soundExp;
    public float volume;
    public float shadowLength = 1;

    private Rigidbody rigid;
    private GameObject cam;
    private Vector3 camPos;
    private AudioSource audioSrc;
    private Renderer rend;
    private Collider col;
    private float length;

    // Use this for initialization
    void Start()
    {
        // cache components
        rigid = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        // get camera's default position
        cam = GameObject.Find("Main Camera");
        camPos = cam.transform.position;

        // get object's random length, includes shadow distance
        length = rend.bounds.size.z * shadowLength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // add forward force to the object
        float speed = -(speedDiff + GameController.gameSpeed);
        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, speed);

        // when object is out of view...
        if (rigid.position.z < camPos.z - length)
        {
            // destroy this object
            Destroy(this.gameObject);
        }
    }

    void Destruction()
    {
        // increase the game score by 1
        GameController.gameScore += destroyScore;

        // play destruction sound
        audioSrc.clip = soundExp;
        audioSrc.volume = volume;
        audioSrc.Play();

        // spawn explosion effect
        Instantiate(explosion, transform.position, transform.rotation);

        // disable render and collider
        rend.enabled = false;
        col.enabled = false;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            Destruction();
        }
    }
}
