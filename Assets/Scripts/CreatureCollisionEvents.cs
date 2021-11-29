using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownAction
{
    public class CreatureCollisionEvents : MonoBehaviour
    {
        public Action OnTargetAdded = () => { };
        //private Creature _creature;
        private List<IDestroyable> _targets = new List<IDestroyable>();
        private TeamsTypes teamType;

        public void Constructor(Creature creature)
        {
            //_creature = creature;
            teamType = creature.TeamType;
            //creature.HitTarget += HitTarget;
        }

        public bool HasTarget
        {
            get { return _targets.Count > 0; }
        }

        public void ClearTargets()
        {
            _targets.Clear();
        }

        public IDestroyable GetTarget()
        {
            while (_targets.Count > 0)
            {
                if (_targets[0] != null && !_targets[0].Equals(null))
                    return _targets[0];
                else
                    _targets.RemoveAt(0);
            }

            return null;
        }

        public void RemoveTarget(IDestroyable creature)
        {
            _targets.Remove(creature);
        }

        private void OnTriggerEnter(Collider other)
        {
            Trigger(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Trigger(collision.collider);
        }

        private void Trigger(Collider other)
        {
            if(teamType.Equals(TeamsTypes.None))
                return;;
            if (other.TryGetComponent(out IDestroyable target))
            {
                if (target.TeamType != teamType)
                {
                    AddTarget(target);
                }
            }
        }

        private void AddTarget(IDestroyable target)
        {
            _targets.Add(target);
            OnTargetAdded();
        }

    }
}