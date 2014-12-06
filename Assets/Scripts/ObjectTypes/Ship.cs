using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship
{
    public string shipName;
    public string description; 
    public float maxSpeed;
    public float thrust;
    public float turnSpeed;
    public float cargoSpace;
    public float hullArmor;
    public float shield;
    public float shieldRecharge;
    public int hardPoints;
    public List<CargoItem> cargoList;
    public List<Weapon> weapons;
    public Mesh shipBody;
    public Material shipMaterial;
    public Vector3 scale;
}
