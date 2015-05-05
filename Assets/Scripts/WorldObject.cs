//-----------------------------------------
//   WorldObject.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 5/5/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour 
{
    public float maxScale;
    public float speedDiff;
    public float heightDiff = 1;
    public float widthDiff = 1;
    public float lengthDiff = 1;
    public float shadowLength = 1;

    private Rigidbody rigid;
    private GameObject cam;
    private Vector3 camPos;
    private float length;
    private float height;

    // Use this for initialization
    void Start()
    {
        // cache components
        rigid = GetComponent<Rigidbody>();

        // get camera's default position
        cam = GameObject.Find("Main Camera");
        camPos = cam.transform.position;

        // randomize scale on launch
        float randScale = Random.Range(1.0f, maxScale);
        transform.localScale = new Vector3(randScale * widthDiff, randScale * heightDiff, randScale * lengthDiff);

        // get object's random length, includes shadow distance
        Renderer rend = GetComponent<Renderer>();
        length = rend.bounds.size.z * shadowLength;

        // get object's height and update y position
        height = transform.localScale.y;
        transform.position = new Vector3(transform.position.x, height / 2, transform.position.z);
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
}
