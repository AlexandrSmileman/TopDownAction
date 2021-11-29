using System;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownAction
{
    public class CreatureMovement
    {
        public Action OnStopped = () => { };
        private const float MIN_DISTANCE = 1.1f;
        public bool IsStopped { get { return _agent.isStopped; } }

        //private Transform _transform;
        private NavMeshAgent _agent;
        //private Creature _target;
        //private Vector3 _targetPos;
        private float _distance;
        public float RemainingDistance { get { return _distance; } }

        public CreatureMovement(Creature creature)
        {
            _agent = creature.GetComponent<NavMeshAgent>();
            creature.OnUpdate += Update;
            creature.OnDeath += () => { _agent.enabled = false;};
        }

        private void Update()
        {
            _distance = Vector3.Distance(_agent.transform.position, _agent.destination);

            if (!_agent.isStopped &&  _distance< MIN_DISTANCE)
            {
                _agent.isStopped = true;
                OnStopped();
            }
        }

        public void MoveToPosition(Vector3 pos)
        {
            //_targetPos = pos;
            _agent.SetDestination(pos);
            _agent.isStopped = false;
        }


        public void Stop()
        {
            _agent.SetDestination(_agent.transform.position);
            //_agent.isStopped = true;
        }
    }
}