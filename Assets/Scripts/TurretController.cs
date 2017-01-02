using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public int TurretSpeed;

    private Rigidbody2D _rigidBody;
    private Vector2 _turretCursor;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Turn(float turnDirection)
    {
        float turn = turnDirection * TurretSpeed * Time.deltaTime;
        _rigidBody.MoveRotation(_rigidBody.rotation + turn);
    }
}
