using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using System;

public class PlayerController : MonoBehaviour {

    public Transform Chassis;
    public Transform Turret;
    public FiringController FiringController;
    public MovementController MovementController;
    public TurretController TurretController;

    public Texture2D CursorTexture;

    private float _movementInputValue;
    private float _turnInputValue;
    private float _turretTurnInputValue;

    // Use this for initialization
    void Start () {
        Cursor.SetCursor(CursorTexture, new Vector2(CursorTexture.width / 2, CursorTexture.height / 2), CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
        // Update chassis movement
        _movementInputValue = Input.GetAxis("Vertical");
        _turnInputValue = Input.GetAxis("Horizontal");

        // Update turret movement
        Vector3 turretTurnDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(Turret.position);
        float angle = Mathf.Atan2(
            Vector3.Dot(Turret.forward, Vector3.Cross(Turret.up, turretTurnDirection)),
            Vector3.Dot(Turret.up, turretTurnDirection)
            ) * Mathf.Rad2Deg;
        
        _turretTurnInputValue = (angle / 180);

        // Update firing
        if (Input.GetButtonDown("Fire1")) FiringController.Fire(Input.GetButton("Fire2"));
	}

    void OnEnable()
    {
        // Also reset the input values.
        _movementInputValue = 0f;
        _turnInputValue = 0f;
    }

    void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        MovementController.Move(_movementInputValue);
        MovementController.Turn(_turnInputValue);
        TurretController.Turn(_turretTurnInputValue);
    }
}
