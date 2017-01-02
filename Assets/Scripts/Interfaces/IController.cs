using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IController
    {
        FiringController FiringController { get; set; }
        MovementController MovementController { get; set; }
        TurretController TurretController { get; set; }
    }
}
