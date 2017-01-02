using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour {

    public int ArmorThickness;
    public TankDamageController TankDamageController;

    private IBallisticsMath _balisticsMath = new BallisticsMath();

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public PenetrationReport Hit(Collision2D collision, float penValue, Vector2 shellVector, BoxCollider2D shellCollider)
    {
        var normal = collision.contacts[0].normal * -1;
        // Draw lines
        var contactPoint = new Vector2(collision.contacts[0].point.x, collision.contacts[0].point.y);
        Debug.DrawLine(contactPoint, contactPoint + normal, Color.red, 10, false);
        Debug.DrawLine(contactPoint - shellVector.normalized, contactPoint, Color.blue, 10, false);

        var outcome = _balisticsMath.ArmorPenetration(ArmorThickness, normal, shellVector, penValue, collision.relativeVelocity.magnitude);

        Debug.DrawLine(contactPoint, contactPoint + outcome.NewTrajectory.normalized, Color.yellow, 10, false);


        if (outcome.Outcome)
        {
            TankDamageController.Hit(collision);
            Physics2D.IgnoreCollision(collision.collider, shellCollider);
        }
        return outcome;
    }
}
