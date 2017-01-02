using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDamageController : MonoBehaviour {

    public int Health;
    public bool Invinsible;
    
    public GameObject Turret;
    public TurretController TurretController;
    public MovementController MovementController;
    public FiringController FiringController;
    public GameObject ExplosionEffect;

    private int _currentHealth;

	// Use this for initialization
	void Start () {
        _currentHealth = Health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hit(Collision2D collision)
    {
        if (enabled && !Invinsible)
        {
            _currentHealth -= 40;
            Debug.Log("hit: " + _currentHealth);

            if (_currentHealth <= 0) Die();
        }
    }

    private void Die()
    {
        Debug.Log("Dead");

        // Disable all user controls
        TurretController.enabled = false;
        MovementController.enabled = false;
        FiringController.enabled = false;

        // Throw the turret in a random direction upwards
        var toss = UnityEngine.Random.insideUnitSphere;
        toss.z = Mathf.Abs(toss.z);
        toss *= 200;
        var turretRigid = Turret.GetComponent<Rigidbody2D>();
        turretRigid.bodyType = RigidbodyType2D.Dynamic;
        turretRigid.simulated = true;
        turretRigid.drag = 1;
        turretRigid.angularDrag = 1;
        turretRigid.gravityScale = 0;
        turretRigid.AddForce(toss);
        turretRigid.AddTorque(UnityEngine.Random.Range(1,10));

        // Put turret collision boxes to ground layer, and tell it to ignore collisions with ground armor.
        var turretArmorFolder = Turret.transform.FindChild("Armor");
        var armorFolder = MovementController.transform.FindChild("Armor");
        for (var x = 0; x < turretArmorFolder.childCount; x++)
        {
            turretArmorFolder.GetChild(x).gameObject.layer = 8;
            foreach(var y in armorFolder.GetComponentsInChildren<BoxCollider2D>())
            {
                Physics2D.IgnoreCollision(turretArmorFolder.GetChild(x).GetComponent<BoxCollider2D>(), y);
            }
        }

        Instantiate(ExplosionEffect, transform.position, transform.rotation);

        this.enabled = false;
    }
}
