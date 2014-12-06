using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Ship playerShip;

    private float rotateHorizontal;
	private float thrust;
    private float rotationSpeed;
    private float thrustForce;
    private float maxVelocity;

	private bool canLand;
	private bool canWarp;
	private GameObject landed;

    private Weapon currentWeapon;
    private float fireSpeed;
    private float fireDelay;
    private const float MIN_WEAPON_DELAY = 4;

	private GameController gameController;
    private Slider uiShields;
    private Slider uiHull;


	// Use this for initialization
	void Start () {
		landed = GameObject.Find("Landed");
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent <GameController> ();

        //Setup player's current ship
        GetComponent<MeshFilter>().mesh = playerShip.shipBody;
        GetComponent<MeshRenderer>().material = playerShip.shipMaterial;
        transform.localScale = playerShip.scale;
        rotationSpeed = playerShip.turnSpeed;
        thrustForce = playerShip.thrust;
        maxVelocity = playerShip.maxSpeed;
        currentWeapon = playerShip.weapons[0];
        fireSpeed = currentWeapon.fireRate;
        fireDelay = 0;

        canLand = true;
		canWarp = false;

        //Setup UI
        foreach (Slider uiSlider in FindObjectsOfType<Slider>())
        {
            if (uiSlider.tag == "GUI")
            {
                if (uiSlider.name == "Shields")
                    uiShields = uiSlider;
                if (uiSlider.name == "Hull")
                    uiHull = uiSlider;
            }
                
        }
        Debug.Log(uiHull.name);
        uiShields.maxValue = playerShip.shield;
        uiShields.value = playerShip.shield;

        uiHull.maxValue = playerShip.hullArmor;
        uiHull.value = playerShip.hullArmor;
        
	}
	
	// Update is called once per frame
	void Update () {
		rotateHorizontal = Input.GetAxis("Horizontal");
		thrust = Input.GetAxis("Vertical");

		//Change selected system for jump drive
		if (Input.GetButtonDown ("CycleWarp")) {
			gameController.ChangeWarpTarget();
		}

        //Warp to selected system
        if (Input.GetButtonDown("Warp") && canWarp) 
		{
			if (gameController.WarpToSystem())
			{
				transform.position = new Vector3(-1*(transform.position.x), transform.position.y, -1*(transform.position.z));
			}
			Debug.Log("canWarp = " + canWarp);
		}

        //Fire selected primary weapon
        if (Input.GetButtonDown("Fire1"))
        {
            if (fireDelay < Time.time   )
            {
                GameObject projectile = currentWeapon.projectile;
                Debug.Log(currentWeapon.weaponName);
                projectile.GetComponent<LaserController>().instanceID = GetInstanceID();
                projectile.GetComponent<LaserController>().damage = currentWeapon.damage;
                projectile.GetComponent<LaserController>().weaponVelocity = currentWeapon.projectileSpeed;
                Instantiate(projectile, transform.position, transform.rotation); 
                fireDelay = Time.time + fireSpeed;
            }
        }
	}

    void FixedUpdate()
    {
        //Rotate Player
        transform.Rotate(Vector3.up * rotateHorizontal * Time.deltaTime * rotationSpeed);

        //Accelerate Player
        rigidbody.AddForce(transform.forward * thrust * Time.deltaTime * thrustForce);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);

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
		if (col.gameObject.tag == "NoWarpZone") 
        {
			canWarp = true;
			Debug.Log ("Can Warp");
		}
	}



	void LateUpdate () {

	}
	

}
