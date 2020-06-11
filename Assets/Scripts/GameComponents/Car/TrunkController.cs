using System;
using UnityEngine;

namespace GameComponents.Car
{
    public class TrunkController : CarController
    {
        protected override void Start()
        {
            base.Start();
            motorTorque *= 4;
        }
    }
}