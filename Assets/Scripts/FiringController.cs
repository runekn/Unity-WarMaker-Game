using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class FiringController : MonoBehaviour {

    public GameObject FiringEffect;     // Effect when firing
    public GameObject Shell;            // Shell that is fired
    public Transform FiringPoint;       // Shell spawn point
    public Rigidbody2D RelativeObject;  // Object that the shell is fired relative to.
    public float ReloadTime;            // Time it takes to reload in seconds
    public ModuleController[] Ammoracks;// All ammo rack modules in the tank
    public float ShellVelocity;         // Initial shell velocity
    public float Inaccuracy;            // 0-1. 0 is 100% accurate. At 1 it shoots in a random direction within 45 degrees.
    
    private float _reloadTimer;
    private bool _canFire = true;

	// Use this for initialization
	void Start () {
        _reloadTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!_canFire)
            if (_reloadTimer > 0) _reloadTimer -= Time.deltaTime;
            else _canFire = true; 
    }

    public void Fire(bool turretMode)
    {
        if (!_canFire) return;

        var shell = Instantiate<GameObject>(Shell, FiringPoint.position, FiringPoint.rotation, null);

        Instantiate(FiringEffect, FiringPoint.position, FiringPoint.rotation, transform);

        if (turretMode) shell.layer = 9;
        else shell.layer = 8;
        
        // Calculate inaccuracy
        var firingVector = FiringPoint.up;
        var random = UnityEngine.Random.Range(-Inaccuracy, Inaccuracy);
        var firingVectorNormal = new Vector3(-firingVector.y, firingVector.x);
        firingVector += firingVectorNormal * random;

        Debug.DrawLine(FiringPoint.position, FiringPoint.position + firingVector, Color.gray, 10, false);

        // Add tank velocity
        var relativeVector = RelativeObject.velocity;
        var relativeVector3D = new Vector3(relativeVector.x, relativeVector.y);

        // Add shell force
        shell.GetComponent<Rigidbody2D>().velocity = firingVector * ShellVelocity + relativeVector3D;

        // Set reload timer based on ammoracks operational
        _canFire = false;
        float ammoCapacity = 1;
        foreach (var ammoracks in Ammoracks)
            if (!ammoracks.IsActive())
                ammoCapacity = ammoCapacity - (1f / (float)Ammoracks.Length);
        if (ammoCapacity == 0) _canFire = false;
        else
        {
            var usedReloadTime = ReloadTime / ammoCapacity;
            _reloadTimer = usedReloadTime;
        }
    }
}
