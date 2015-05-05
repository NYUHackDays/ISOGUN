//-----------------------------------------
//   PointsController.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 5/5/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class PointsController : MonoBehaviour
{
    public float speedDiff;
    public float shadowLength = 1;
    public GameObject explosion;

    private Rigidbody rigid;
    private GameObject cam;
    private Vector3 camPos;
    private float length;
    private bool destroyed = false;

	// Use this for initialization
	void Start ()
    {
        // cache components
        rigid = GetComponent<Rigidbody>();

        // set y position on start
        transform.position = new Vector3(transform.position.x, 5.0f, transform.position.z);

        // get camera's default position
        cam = GameObject.Find("Main Camera");
        camPos = cam.transform.position;

        // get object's random length, includes shadow distance
        Renderer rend = GetComponentInChildren<Renderer>();
        length =  rend.bounds.size.z * shadowLength;
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

        // if the game state does not equal game play
        if (GameController.gameState != GameStates.GamePlay)
        {
            // destroy!
            Destruction();
        }
    }

    void Destruction()
    {
        // spawn explosion effect
        Instantiate(explosion, transform.position, transform.rotation);

        // destroy this gameobject
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Bullet" || coll.gameObject.tag == "Player")
        {
            // when bonus is shot, increase score by 10
            GameController.gameScore += 10.0f;

            // run destruction function
            Destruction();
        }
        else
        {
            // if collide with anything else, destroy
            Destroy(this.gameObject);
        }
    }
}
