//-----------------------------------------
//   Points.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 5/5/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour
{
    private float rotX, rotY, rotZ;
    private Vector3 randRot;

	// Use this for initialization
	void Start () 
    {
        // add random rotational velocity on start
        randRot.x = Random.Range(-100.0f, 100.0f);
        randRot.y = Random.Range(-100.0f, 100.0f);
        randRot.z = Random.Range(-100.0f, 100.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        // get current rotation
        Vector3 rot = transform.eulerAngles;

        // check object's tag
        if (gameObject.tag == "PointsShell")
        {
            // rotate shell forwards
            transform.eulerAngles = new Vector3(rot.x + randRot.x * Time.deltaTime, rot.y + randRot.y * Time.deltaTime, rot.z);
        }
        else
        {
            // rotate core opposite direction of shell
            transform.eulerAngles = new Vector3(rot.x - randRot.x * Time.deltaTime, rot.y - randRot.y * Time.deltaTime, rot.z);
        }
        
	}
}
