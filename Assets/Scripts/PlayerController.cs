using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float rotationSpeed;
	public float thrustForce;
	public float maxVelocity;
	private float rotateHorizontal;
	private float thrust;

	private bool canLand;
	private bool canWarp;
	private GameObject landed;

	private GameController gameController;

	// Use this for initialization
	void Start () {
		landed = GameObject.Find("Landed");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent <GameController> ();
		canLand = true;
		canWarp = false;
	}
	
	// Update is called once per frame
	void Update () {
		rotateHorizontal = Input.GetAxis("Horizontal");
		thrust = Input.GetAxis("Vertical");

		//Change selected system for jump drive
		if (Input.GetKeyDown (KeyCode.Tab)) {
			gameController.ChangeWarpTarget();
		}

		if (Input.GetKeyDown (KeyCode.P) && canWarp) 
		{
			if (gameController.WarpToSystem())
			{
				transform.position = new Vector3(-1*(transform.position.x), transform.position.y, -1*(transform.position.z));
			}
			Debug.Log("canWarp = " + canWarp);
		}


	}

	void OnTriggerEnter (Collider col) 
	{
		//Check if player has entered a landing zone
		if (col.gameObject.tag == "Planet") {
			canLand = true;
			Debug.Log ("Can Land");
		}

		//Check if player has entered a no warp zone
		if (col.gameObject.tag == "NoWarpZone") {
			canWarp = false;
			Debug.Log ("Cant Warp");
		}
	}
	
	void OnTriggerExit (Collider col)
	{
		//Check if player has left landing zone
		if(col.gameObject.tag == "Planet")
		{
			canLand = false;
			Debug.Log ("Can't Land");
		}

		//Check if player has left no warp zone
		if (col.gameObject.tag == "NoWarpZone") {// && !(col.bounds.Intersects(collider.bounds))) {
			canWarp = true;
			Debug.Log ("Can Warp");
		}
	}

	void FixedUpdate () {
		//Rotate Player
		transform.Rotate (Vector3.up * rotateHorizontal * Time.deltaTime * rotationSpeed);

		//Accelerate Player
		rigidbody.AddForce (transform.forward * thrust * Time.deltaTime * thrustForce);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);

	}

	void LateUpdate () {

	}
	

}
