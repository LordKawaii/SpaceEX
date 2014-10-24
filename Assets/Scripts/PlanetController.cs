using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public string planetName;
	public Vector3 planetPos;
	public Vector3 planetScale;

	
	void Awake () {

	}

	void Start () {
		transform.position = planetPos;
		transform.localScale = planetScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetPlanetName () {
		return planetName;
	}

	public void setPlanetName (string name) {
		planetName = name;
	}
}
