using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownAction
{
    public class DestroyableCollisionEvents : MonoBehaviour
    {
        public Action OnTargetAdded = () => { };
        private List<IDestroyable> _targets = new List<IDestroyable>();
        private TeamsTypes _teamType;
        private Collider _collider;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        public void Constructor(IDestroyable destroyable)
        {
            destroyable.OnDeath += ()=> { _collider.enabled = false; };
            _teamType = destroyable.TeamType;
            _collider.enabled = true;
        }

        public bool HasTarget => _targets.Count > 0;

        public void ClearTargets()
        {
            _targets.Clear();
            print("target clear");
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
            if (other.TryGetComponent(out IDestroyable target))
            {
                if (target.TeamType != _teamType)
                {
                    print(_teamType + " " + target.TeamType);
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