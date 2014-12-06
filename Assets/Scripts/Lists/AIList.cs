using UnityEngine;
using System.Collections;

public class AIList : MonoBehaviour {

    public GameObject aiPrefab;
    private GameController gameController;

	// Use this for initialization
	void Setup () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetupIAList()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void getAI(int aiID)
    {
        Vector3 instantiatePos = new Vector3(9f, 0f, 5f);
        GameObject newAI = Instantiate(aiPrefab, instantiatePos, new Quaternion()) as GameObject;
        
        switch (aiID)
        { 
            case 1:
                newAI.GetComponent<AIShip>().setUpShip(gameController.GetComponent<ShipList>().GetShip(1));
                newAI.name = newAI.GetComponent<AIShip>().aiShip.shipName;
                newAI.GetComponent<AIShip>().faction = "Empire";
                break;
        }

    }
}
