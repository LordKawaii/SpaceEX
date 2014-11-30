using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		var player = GameObject.Find("Player");
		const int OFFSET_SLOWDOWN = 10; 

		Vector3 playerVelocity = player.rigidbody.position;
		Vector2 offset = new Vector2 (-playerVelocity.x, -playerVelocity.z);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset/OFFSET_SLOWDOWN);
	}
}
