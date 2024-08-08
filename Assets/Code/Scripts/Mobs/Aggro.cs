using Logic;
using UnityEngine;

namespace Mobs
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _aggroZone;

        private void OnEnable()
        {
            _aggroZone.TriggerEnter += AggroZoneEnter;
            _aggroZone.TriggerExit += AggroZoneExit;
        }

        private void OnDisable()
        {
            _aggroZone.TriggerEnter -= AggroZoneEnter;
            _aggroZone.TriggerExit -= AggroZoneExit;
        }

        private void AggroZoneEnter(Collider obj) =>
            print("Attack!");

        private void AggroZoneExit(Collider obj) =>
            print("Stop Attack! Я гражданский!");
    }
}
