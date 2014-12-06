using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIShip : MonoBehaviour {

    public Ship aiShip;
    public Slider uiShield;
    public Slider uiHull;
    public string faction;
    public GameObject currentTarget;
    private GameController gameController;
    private float aiShield;
    private float aiHull;
    private float aiShieldRechargeRate;
    private float aiTurnSpeed;
    private StarSystem currentSystem;
    private float rechargeTimer;

	// Use this for initialization
	void Start () {
        aiTurnSpeed = aiShip.turnSpeed / 100;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        currentSystem = gameController.getCurrentSystem();
        currentTarget = GameObject.FindGameObjectsWithTag("Planet")[0];
        aiShieldRechargeRate = aiShip.shieldRecharge;
        Debug.Log("Current AI Target is " + currentTarget.name);

        //Setup UI
        uiHull.maxValue = aiShip.hullArmor;
        uiHull.value = aiShip.hullArmor;
        uiShield.maxValue = aiShip.shield;
        uiShield.value = aiShip.shield;

        //Setup rechargeTime
        rechargeTimer = Time.deltaTime;
    }

    
	
	// Update is called once per frame
	void FixedUpdate () {
        turnToward(currentTarget);

        //Move towards target
        RaycastHit hit;
        Vector3 targetRaycastVector = new Vector3(transform.position.x, currentTarget.transform.position.y, transform.position.z);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 1000;
        Debug.DrawRay(targetRaycastVector, forward, Color.red);
        if (Physics.Raycast(targetRaycastVector, forward, out hit) && hit.collider.gameObject == currentTarget && hit.distance > 5f)
        {
            //AddThrust();
        }

	}

    public void setUpShip(Ship ship)
    {
        aiShip = ship;
        GetComponent<MeshFilter>().sharedMesh = aiShip.shipBody;
        GetComponent<MeshCollider>().sharedMesh = aiShip.shipBody;
        GetComponent<MeshRenderer>().material = aiShip.shipMaterial;
        aiShield = aiShip.shield;
        aiHull = aiShip.hullArmor;
        transform.localScale = aiShip.scale;
        aiShield = aiShip.shield;
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Col = " + col.name);
        if (col.tag == "Weapon")
        {
            
            if (col.GetComponent<LaserController>().instanceID != GetInstanceID())
            {
                float weaponDamage = col.GetComponent<LaserController>().damage;

                if (aiShield > 0)
                {
                    aiShield -= weaponDamage;
                    uiShield.value -= weaponDamage;

                    Debug.Log("AI Ship Shield: " + aiShield);
                }
                else if (aiHull > 0)
                {
                    aiHull -= weaponDamage;
                    uiHull.value -= weaponDamage;
                    Debug.Log("AI Ship Hull: " + aiHull);
                }
                else
                    Destroy(this.gameObject);

                Destroy(col.gameObject);
            }
        }
    }

    void turnToward(GameObject target)
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float step = aiTurnSpeed * Time.deltaTime;
        Vector3 twoardsEnemy = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
        transform.rotation = Quaternion.LookRotation(twoardsEnemy);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    void AddThrust()
    {
        //Accelerate Shipe
        rigidbody.AddForce(transform.forward * Time.deltaTime * aiShip.thrust);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, aiShip.maxSpeed);
    }

    void rechargeShields()
    {
        if (aiShield < aiShip.shield)
        {
            
        }
    }
}
