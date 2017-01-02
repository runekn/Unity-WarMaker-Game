using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBallisticsMath
{
    PenetrationReport ArmorPenetration(int armorThickness, Vector2 armorNormal, Vector2 shellVector, float shellPenValue, float impactVelocity);
}

public class PenetrationReport
{
    public Vector2 NewTrajectory;
    public bool Outcome;
}
