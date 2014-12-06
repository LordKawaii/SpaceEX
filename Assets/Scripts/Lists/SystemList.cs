using UnityEngine;
//using UnityEditor;
using System.Collections;

public class SystemList : MonoBehaviour {

	public GameObject planet;

	private StarSystem[] starSystems;
	private int numStarSystems = 2;

	private StarSystem starSystem; 

	public void CrateSystemList()
	{
		starSystems = new StarSystem[numStarSystems];

        //Creating home System
        starSystems[0] = new StarSystem();
        starSystems[0].name = "Home";
        starSystems[0].id = 0;
        starSystems[0].planets.AddFirst(createPlanet("Earth", 1.0f, 0.0f, 0.0f));
        starSystems[0].planets.AddFirst(createPlanet("Moon", 0.4f, 3.0f, 3.0f));

        //Creating Rec System
        starSystems[1] = new StarSystem();
        starSystems[1].name = "Rec";
        starSystems[1].id = 1;
        starSystems[1].planets.AddFirst(createPlanet("Rilec", 1.5f, 0.0f, 0.0f));
        starSystems[1].planets.AddFirst(createPlanet("Risa", 0.5f, 4.0f, -2.0f));
	}

	public StarSystem[] GetStarSystems()
	{
		return starSystems;
	}

	public int GetNumStarSystems()
	{
		return numStarSystems;
	}

	public void CreateSystems(int systemID)
	{

        foreach (Planet systemPlanet in starSystems[systemID].planets)
        {
            
            Instantiate(planet);
            planet.name = systemPlanet.name;
            planet.GetComponent<PlanetController>().setPlanetName(systemPlanet.name);
            planet.GetComponent<PlanetController>().planetPos = systemPlanet.position;
            planet.GetComponent<PlanetController>().planetScale = systemPlanet.scale;
        }

	}

	Planet createPlanet(string name, float scale, float x, float z)
	{
		const int NORMAL_SCALE = 5;

        Planet newPlanet = new Planet();

		//Set PlanetController
        newPlanet.name = name;
        newPlanet.position = new Vector3(x, -4.0f, z);
        newPlanet.scale = new Vector3(scale * NORMAL_SCALE, scale * NORMAL_SCALE, scale * NORMAL_SCALE);

        return newPlanet;

	}

}
