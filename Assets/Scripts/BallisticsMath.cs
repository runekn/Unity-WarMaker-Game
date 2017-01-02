using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticsMath : IBallisticsMath
{
    public static int BOUNCEANGLE = 70; // The angle at which shells will always bounce off any armor
    public static float FLIGHTTIME = 1; //The time it takes for a shell to hit the ground
    public static float ARMORDRAG = 1;  // How much armor slows down a shell
    public static float TURRETTOGROUNDFLIGHTDISTANCE = 0.3f; // How long a shell must fly before transistioning to ground layer is selected.

    public PenetrationReport ArmorPenetration(int armorThickness, Vector2 armorNormal, Vector2 shellVector, float shellPenValue, float impactVelocity)
    {
        // Calculate shell pen based on speed of projectile
        var effectiveShellPenValue = shellPenValue * impactVelocity;

        // Calculate angle of contact
        var angle = Vector2.Angle(shellVector, armorNormal);

        // Calculate relative thickness of armor
        var relativeThickness = armorThickness / Mathf.Cos(angle * Mathf.Deg2Rad);
        
        // Determine outcome
        // Bounce no matter what if angle is very great
        if (angle > BOUNCEANGLE)
        {
            Debug.Log("Forced Ricochet");
            return new PenetrationReport()
            {
                NewTrajectory = Vector2.Reflect(shellVector, armorNormal),
                Outcome = false
            };
        }
        // Successful pen
        else if (effectiveShellPenValue > relativeThickness)
        {
            Debug.Log("Successful pen");
            return new PenetrationReport()
            {
                NewTrajectory = shellVector * (1 / (ARMORDRAG + relativeThickness)),
                Outcome = true
            };
        }
        // No pen, but bounce
        else if (angle > BOUNCEANGLE - 10)
        {
            Debug.Log("Ricochet");
            return new PenetrationReport()
            {
                NewTrajectory = Vector2.Reflect(shellVector, armorNormal),
                Outcome = false
            };
        }
        // Shell stopped
        else
        {
            Debug.Log("No pen");
            return new PenetrationReport()
            {
                NewTrajectory = Vector2.zero,
                Outcome = false
            };
        }
    }
}
