using UnityEngine;
using System.Collections;

public class WeaponList : MonoBehaviour {

    public GameObject weapon1Projectile;

    public Weapon getWeapon(int id)
    {

        Weapon newWeapon = new Weapon();

        switch (id)
        {
            case 1:
                newWeapon.weaponName = "Light Laser";
                newWeapon.description = "A low damage partical weapon";
                newWeapon.damage = 10f;
                newWeapon.projectile = weapon1Projectile;
                newWeapon.projectileSpeed = 5;
                newWeapon.type = 1;
                newWeapon.fireRate = 1;

                break;
        }
        return newWeapon;
    }

    public void fireWeapon(int id, Transform shipTransform)
    {
        switch (id)
        { 
            case 1:

                break;
        }
    }

}
