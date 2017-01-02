using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public Transform Chassis;
    public Transform Turret;
    public Transform Enemy;
    public FiringController FiringController;
    public MovementController MovementController;
    public TurretController TurretController;

    private float _movementInputValue;
    private float _turnInputValue;
    private float _turretTurnInputValue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Update turret movement
        Vector3 turretTurnDirection = (Turret.position - Enemy.position) * -1;
        float angle = Mathf.Atan2(
            Vector3.Dot(Turret.forward, Vector3.Cross(Turret.up, turretTurnDirection)),
            Vector3.Dot(Turret.up, turretTurnDirection)
            ) * Mathf.Rad2Deg;
        
        Debug.Log(angle);
        _turretTurnInputValue = (angle / 180);

        FiringController.Fire(false);
    }

    void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        MovementController.Move(_movementInputValue);
        MovementController.Turn(_turnInputValue);
        TurretController.Turn(_turretTurnInputValue);
    }

    void OnEnable()
    {
        // Also reset the input values.
        _movementInputValue = 0f;
        _turnInputValue = 0f;
    }
}
