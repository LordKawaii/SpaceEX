using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour{
    public string weaponName;
    public string description;
    public float damage;
    public int type;
    public float projectileSpeed;
    public Transform shipTransform;
    public GameObject projectile;
    public float shipInstanceID;
    public float fireRate;

    void Start()
    {
    
    }

    public void fire()
    {
        transform.Rotate(90, 0, 0);
        rigidbody.velocity = shipTransform.up * projectileSpeed;
    }
}
