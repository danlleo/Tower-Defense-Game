using System;
using Logic;
using UnityEngine;

namespace Tower
{
    public class Ballista : TowerTarget
    {
        public override void LaunchProjectileAt(IHealth enemy, Action onHitAction) => 
            onHitAction();

        protected override void OnShoot() =>
            Debug.Log("Shooting");
    }
}