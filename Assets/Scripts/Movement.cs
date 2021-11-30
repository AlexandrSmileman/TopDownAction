using System;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownAction
{
    public class Movement
    {
        public Action OnStopped = () => { };
        private const float MIN_DISTANCE = 1.1f;
        public bool IsStopped => _agent.isStopped;
        private NavMeshAgent _agent;
        public float RemainingDistance { get; private set; }

        public Movement(NavMeshAgent agent, IUpdatable updatable, IDestroyable destroyable)
        {
            _agent = agent;
            updatable.OnUpdate += Update;
            destroyable.OnDeath += () => { _agent.enabled = false;};
        }

        private void Update()
        {
            RemainingDistance = Vector3.Distance(_agent.transform.position, _agent.destination);

            if (!_agent.isStopped &&  RemainingDistance< MIN_DISTANCE)
            {
                _agent.isStopped = true;
                OnStopped();
            }
        }

        public void MoveToPosition(Vector3 pos)
        {
            _agent.SetDestination(pos);
            _agent.isStopped = false;
        }

        public void Stop()
        {
            _agent.SetDestination(_agent.transform.position);
        }
    }
}