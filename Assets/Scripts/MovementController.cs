using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public int PlayerNumber;
    public int TopSpeed;
    public int AccelerationSpeed;
    public int TurnSpeed;
    public ModuleController[] Engines;
    public ModuleController[] Transmissions;
    
    private string _movementAxisName;
    private string _turnAxisName;
    private Rigidbody2D _rigidbody;              // Reference used to move the tank.

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        _rigidbody.isKinematic = false;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
    }

    public void Move(float movementInputValue)
    {
        var engineCapacity = 1;
        foreach (var engine in Engines) if (!engine.IsActive()) engineCapacity -= (1 / Engines.Length);
        int usedTopSpeed = engineCapacity > 0 ? TopSpeed / engineCapacity : 0;

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector2 movement = transform.up * movementInputValue * usedTopSpeed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }


    public void Turn(float turnInputValue)
    {
        var engineCapacity = 1;
        foreach (var engine in Engines) if (!engine.IsActive()) engineCapacity -= (1 / Engines.Length);
        int usedTurnSpeed = engineCapacity > 0 ? TurnSpeed / engineCapacity : 0;

        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = turnInputValue * usedTurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        float turnRotation = turn;

        // Apply this rotation to the rigidbody's rotation.
        _rigidbody.MoveRotation(_rigidbody.rotation - turnRotation);
    }
}
