using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipList : MonoBehaviour {

    //Ship one mesh and texture
    public Mesh ship1Mesh;
    public Material ship1Material;
    //Ship one mesh and texture
    public Mesh ship2Mesh;
    public Material ship2Material;

    private WeaponList weaponList;

    public void setupShipList ()
    {
        weaponList = gameObject.GetComponent<WeaponList>();
    }

    public Ship GetShip(int id)
    {
        Ship newShip = new Ship();

        switch (id)
        {
            case 1:
                newShip.shipName = "AK5";
                newShip.maxSpeed = 10;
                newShip.turnSpeed = 50;
                newShip.thrust = 50;
                newShip.hardPoints = 10;
                newShip.cargoSpace = 10;
                newShip.hullArmor = 100;
                newShip.shield = 50;
                newShip.shieldRecharge = 2;
                newShip.description = "Light patrol ship";
                newShip.shipMaterial = ship1Material;
                newShip.shipBody = ship1Mesh;
                newShip.scale = new Vector3(0.1f, 0.1f, 0.1f);
                newShip.weapons = new List<Weapon>();
                newShip.weapons.Add(weaponList.getWeapon(1));
                break;
            case 2:
                newShip.shipName = "LargeFrigate";
                newShip.maxSpeed = 8;
                newShip.turnSpeed = 30;
                newShip.thrust = 50;
                newShip.hardPoints = 10;
                newShip.cargoSpace = 10;
                newShip.hullArmor = 100;
                newShip.shield = 50;
                newShip.shieldRecharge = 2;
                newShip.description = "Heavy confederate frigate";
                newShip.shipMaterial = ship2Material;
                newShip.shipBody = ship2Mesh;
                newShip.scale = new Vector3(1f, 1f, 1f);
                newShip.weapons = new List<Weapon>();
                newShip.weapons.Add(weaponList.getWeapon(1));
                break;
        }
        return newShip;
    }

}
