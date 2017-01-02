using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour {

    public float PenValue;
    public bool OnlyOneEffect;
    public GameObject ArmorRicochetEffect;
    public GameObject ArmorHitEffect;
    public GameObject ArmorPenEffect;
    public GameObject DirtHitEffect;
    public GameObject GenericHitEffect;

    private Rigidbody2D _rigid;
    private Vector2 _velocity;
    private BoxCollider2D _collider;
    private float _flightTimer = BallisticsMath.FLIGHTTIME;
    private Vector2 _initialPosition;
    private int _chosenLayer;
    private bool _forcedTurretLayer = true;
    private bool _shownEffect = false;

	// Use this for initialization
	void Start () {
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _chosenLayer = gameObject.layer;
        gameObject.layer = 9;
        _initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        _velocity = _rigid.velocity;

        // Turret to ground layer transistioning
        if(_forcedTurretLayer && Vector2.Distance(_initialPosition, transform.position) > BallisticsMath.TURRETTOGROUNDFLIGHTDISTANCE)
        {
            _forcedTurretLayer = false;
            gameObject.layer = _chosenLayer;
        }

        // Flight time
        _flightTimer -= Time.deltaTime;
        if (_flightTimer <= 0)
        {
            HitEffect(DirtHitEffect, transform.position, transform.up);
            Destroy(gameObject);
        }
	}

    private void HitEffect(GameObject effect, Collision2D collision)
    {
        var contact = collision.contacts[0];
        HitEffect(effect, contact.point, contact.normal);
    }

    private void HitEffect(GameObject effect, Vector2 position, Vector2 vector)
    {
        if (OnlyOneEffect && _shownEffect) return;

        _shownEffect = true;
        var rotation = Quaternion.LookRotation(vector);
        Instantiate(effect, position, rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.tag == "armor")
        {
            // Hit
            var outcome = collision.collider.GetComponent<ArmorController>().Hit(collision, PenValue, _velocity, _collider);

            _rigid.velocity = outcome.NewTrajectory;

            // If it penned
            if (outcome.Outcome)
            {
                HitEffect(ArmorPenEffect, collision);
            }
            // If it didn't
            else
            {
                // If it stopped
                if (outcome.NewTrajectory.Equals(Vector2.zero))
                {
                    HitEffect(ArmorHitEffect, collision);
                    Destroy(gameObject);
                }
                // If it ricochet
                else
                {
                    HitEffect(ArmorRicochetEffect, collision);
                }
            }
        }
        else if (collision.collider.transform.tag == "module")
        {
            // Hit
            var outcome = collision.collider.GetComponent<ModuleController>().Hit(collision, PenValue, _velocity, _collider);

            // If it penned
            if (outcome.Outcome)
            {
                HitEffect(ArmorPenEffect, collision);
            }
            // If it didn't
            else
            {
                // If it stopped
                if (outcome.NewTrajectory.Equals(Vector2.zero))
                {
                    HitEffect(ArmorHitEffect, collision);
                    Destroy(gameObject);
                }
                // If it ricochet
                else
                {
                    HitEffect(ArmorRicochetEffect, collision);
                }
            }
        }
        else
        {
            HitEffect(GenericHitEffect, collision);
            Destroy(gameObject);
        }
    }
}
