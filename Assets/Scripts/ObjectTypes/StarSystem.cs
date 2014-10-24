using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarSystem {

	public string name;
	public float id;
	public LinkedList<GameObject> connectedSyetems = new LinkedList<GameObject>();
	public LinkedList<GameObject> planets = new LinkedList<GameObject>();

}
