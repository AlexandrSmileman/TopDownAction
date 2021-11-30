using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownAction
{
    public class EnemyAi : CreatureAi
    {
         private Vector3 BasePoint { get; set; }
         private float _waitingTime;

         private const float MAXWaitingTime = 5f;

        public EnemyAi(Creature creature) : base(creature)
        {
            BasePoint = creature.transform.position;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Patrol();
        }

        private void Patrol()
        {
            if (_target == null && _movement.IsStopped)
            {
                _waitingTime += Time.deltaTime;
                if (_waitingTime > MAXWaitingTime)
                {
                    Vector3 randomDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;
                    Vector3 pos = BasePoint + randomDirection * Random.Range(2f, 3f);
                    SetDestination(pos);
                    _waitingTime = 0;
                }
            }
        }
        
    }
}