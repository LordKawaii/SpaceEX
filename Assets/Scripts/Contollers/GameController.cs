using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {


	private string currentSystem;
	private int currentSystemNum;
    private Text currentWeaponGUI;
    private Text warpTarget;
	private string selectedSystem;
	private int selectedSystemNum;
	private StarSystem[] starSystems;
	private int numStarSystems;
	private SystemList starList;
    private GameObject player;
    private ShipList shipList;
    private AIList aiList;

	// Use this for initialization
	void Awake () {
        //Setup GUI
        currentWeaponGUI = GameObject.Find("CurrentWeapon").GetComponent<Text>();
        warpTarget = GameObject.Find("WarpTarget").GetComponent<Text>();
        

        //Setup ShipList
        shipList = GetComponent<ShipList>();
        shipList.setupShipList();

        //Setup AIList
        aiList = GetComponent<AIList>();
        aiList.SetupIAList();

		//Get SystemList.cs
		starList = GetComponent<SystemList> ();
        starList.CrateSystemList();

		//Get list of system names and number of total systems
		numStarSystems = starList.GetNumStarSystems();
		starSystems = new StarSystem[numStarSystems];
		starSystems = starList.GetStarSystems ();

		//Set current system at start of game to home
		currentSystem = starSystems[0].name;
		currentSystemNum = 0;
		selectedSystemNum = 0;

        starList.CreateSystems(0);

        
        //Setup Player
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().playerShip = shipList.GetShip(1);
        ChangeWeaponGUI(player.GetComponent<PlayerController>().playerShip.weapons[0].weaponName);

        
        //Create AIShip
        aiList.getAI(1);

		
        //Set GUI Text to current system
		warpTarget.text = currentSystem;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public IEnumerator rechargeShields(float shields, float maxShields, float shieldRechargeRate)
    {
        while (shields <= maxShields)
        {
            for (float timer = 0; timer < 1; timer += Time.deltaTime)
                yield return 0;
            if (shields + shieldRechargeRate > maxShields)
                shields = maxShields;
            else
                shields += shieldRechargeRate;

            Debug.Log(shields);
        }
    }

	public void ChangeWarpTarget() //Cylcle through systme list
	{
		if (selectedSystemNum < numStarSystems-1)
		{
			selectedSystemNum++;
			selectedSystem = starSystems[selectedSystemNum].name;
		}
		else
		{
			selectedSystemNum = 0;
			selectedSystem = starSystems[selectedSystemNum].name;
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

    public void ChangeWeaponGUI(string weaponName)
    { 
        currentWeaponGUI.text = weaponName;
    }

    public StarSystem getCurrentSystem()
    {

        return starSystems[currentSystemNum];
    }

}
