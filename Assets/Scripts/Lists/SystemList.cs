using UnityEngine;
using UnityEditor;
using System.Collections;

public class SystemList : MonoBehaviour {

	public GameObject planet;

	private string[] starSystems;
	private int numStarSystems = 2;

	private StarSystem starSystem; 

	void Awake()
	{
		starSystems = new string[numStarSystems];
		starSystems [0] = "Home";
		starSystems [1] = "Rec";
	}

	public string[] GetStarSystems()
	{
		return starSystems;
	}

	public int GetNumStarSystems()
	{
		return numStarSystems;
	}

	public void CreateSystems(int systemID)
	{
		//Creating home System
		starSystem = new StarSystem();


		switch (systemID) {
		case 0:
			starSystem.name = "Sol";
			starSystem.id = 0;

			//Adding planets to home 
			GameObject home1 = Instantiate(planet) as GameObject;
			starSystem.planets.AddFirst (createPlanet(home1, "Earth", 1.0f, 0.0f, 0.0f));
			GameObject home2 = Instantiate(planet) as GameObject;
			starSystem.planets.AddFirst (createPlanet(home2, "Moon", 0.4f, 3.0f, 3.0f));

			break;
		case 1:
			starSystem.name = "Rec";
			starSystem.id = 1;

			//Adding planets to Rec 
			GameObject rec1 = Instantiate(planet) as GameObject;
			starSystem.planets.AddFirst (createPlanet(rec1, "Rilec", 1.5f, 0.0f, 0.0f));

			GameObject rec2 = Instantiate(planet) as GameObject;
			starSystem.planets.AddFirst (createPlanet(rec2, "Risa", 0.5f, 4.0f, -2.0f));

			break;
		}

	}

	GameObject createPlanet(GameObject planet, string name, float scale, float x, float z)
	{
		const int NORMAL_SCALE = 5;
		planet.name = name;
	
		//Set PlanetController
		PlanetController planetCtrl = planet.GetComponent<PlanetController>();
		planetCtrl.planetName = name;
		planetCtrl.planetPos =  new Vector3(x, -4.0f, z);
		planetCtrl.planetScale = new Vector3(scale * NORMAL_SCALE, scale * NORMAL_SCALE, scale * NORMAL_SCALE);
	
		return planet;

	}

}
