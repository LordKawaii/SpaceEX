using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private string currentSystem;
	private int currentSystemNum;
	private GUIText warpTarget;
	private string selectedSystem;
	private int selectedSystemNum;
	private string[] starSystems;
	private int numStarSystems;
	private SystemList starList;

	// Use this for initialization
	void Start () {
		//Get SystemList.cs
		starList = GetComponent<SystemList> ();

		//Get list of system names and number of total systems
		numStarSystems = starList.GetNumStarSystems();
		starSystems = new string[numStarSystems];
		starSystems = starList.GetStarSystems ();

		//Set current system at start of game to home
		currentSystem = starSystems[0];
		currentSystemNum = 0;
		selectedSystemNum = 0;

		starList.CreateSystems (0);

		//Set GUI Text to current system
		warpTarget = GameObject.Find("WarpTargetGUI").guiText;
		warpTarget.text = currentSystem;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeWarpTarget() //Cylcle through systme list
	{
		if (selectedSystemNum < numStarSystems-1)
		{
			selectedSystemNum++;
			selectedSystem = starSystems[selectedSystemNum];
		}
		else
		{
			selectedSystemNum = 0;
			selectedSystem = starSystems[selectedSystemNum];
		}

		Debug.Log("Changing system target: selectedSystemNum: " + selectedSystemNum + " currentSystemNum: " + currentSystemNum); 

		warpTarget.text = selectedSystem;
	}
	
	public bool WarpToSystem()
	{
		Debug.Log("Current system ID: " + currentSystemNum +" Selected system ID: " + selectedSystemNum);
		Collider playerCollider = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider>();
		bool canWarp = true;
		foreach (GameObject noWarpZone in GameObject.FindGameObjectsWithTag("NoWarpZone")) {

			if 	(playerCollider.bounds.Intersects(noWarpZone.GetComponent<Collider>().bounds)){
				canWarp = false;
			}
		}

		if (currentSystemNum != selectedSystemNum && canWarp) {
			var planets = GameObject.FindGameObjectsWithTag ("Planet");
			
			foreach (var obj in planets)
			{
				GameObject.Destroy(obj);
			}
			
			starList.CreateSystems (selectedSystemNum);
			currentSystemNum = selectedSystemNum;
			Debug.Log("Post Warp currentSystem: " + currentSystem);
			return true;
		}
		return false;
	}

}
