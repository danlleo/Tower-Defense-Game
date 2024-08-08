using Enemy;
using Logic;
using UnityEngine;

namespace Mobs
{
    public class CastPower : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _castZone;
        [Range(0, 100)] [SerializeField] private float _castBuffPrecent;

        private void OnEnable()
        {
            _castZone.TriggerEnter += CastZoneEnter;
            _castZone.TriggerExit += CastZoneExit;
        }

        private void OnDisable()
        {
            _castZone.TriggerEnter -= CastZoneEnter;
            _castZone.TriggerExit -= CastZoneExit;
        }

        private void CastZoneEnter(Collider obj)
        {
            print("Кастонул!"); 
            
            if (obj.TryGetComponent(out EnemyHealth enemyHealth)) 
                enemyHealth.StartRegeneration(1, 5);

            //TODO
            //speed buff
            //damage buff
        }

        private void CastZoneExit(Collider obj)
        {
            print("Коил полетел мимо!");
            
            if (obj.TryGetComponent(out EnemyHealth enemyHealth)) 
                enemyHealth.StopRegeneration();
        }
    }
}
