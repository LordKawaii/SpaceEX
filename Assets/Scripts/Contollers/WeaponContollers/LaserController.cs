using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

    public float weaponVelocity;
    public int instanceID;

	// Use this for initialization
	void Start () {
        transform.Rotate(90, 0, 0);
        rigidbody.velocity = transform.up * weaponVelocity;
	}
	
}
