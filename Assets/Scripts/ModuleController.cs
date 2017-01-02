using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleController : MonoBehaviour, IModuleController {

    public bool Active = true;
    public ModuleType ModuleType;
    public int ArmorThickness;

    private IBallisticsMath _ballisticsMath = new BallisticsMath();

    public bool IsActive()
    {
        return Active;
    }

    public void SetActive(bool active)
    {
        Active = active;
    }

    public PenetrationReport Hit(Collision2D collision, float penValue, Vector2 shellVector, BoxCollider2D shellCollider)
    {
        var normal = collision.contacts[0].normal * -1;

        var outcome = _ballisticsMath.ArmorPenetration(ArmorThickness, normal, shellVector, penValue, collision.relativeVelocity.magnitude);

        if (outcome.Outcome)
        {
            Physics2D.IgnoreCollision(collision.collider, shellCollider);
        }
        return outcome;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
