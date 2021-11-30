using System;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownAction
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CreatureAnimation))]
    [RequireComponent(typeof(Animator))]
    //[RequireComponent(typeof(CreatureCollisionEvents))]
    public class Creature : MonoBehaviour, IDestroyable
    {
        public int Health { get; private set; }
        public TeamsTypes TeamType { get; private set; }
        public CreatureParameters Parameters { get; private set; }
        private CreatureAi _creatureAi;
        public Vector3 Position => transform.position;
        public Action OnUpdate = () => { };
        public event Action OnDeath = () => { };
        public event Action OnChangeHealth = () => { };
        public virtual void Constructor(TeamsTypes teamType, CreatureParameters parameters)
        {
            Parameters = parameters;
            TeamType = teamType;
            Health = Parameters.health;

            GameElementsView.Instance.AddHealthBar(this, transform);
        }

        public CreatureAi SetAi(CreatureAi ai)
        {
            _creatureAi = ai;
            return _creatureAi;
        }

        private void Update()
        {
            OnUpdate();
        }

        public void ChangeHealth(float value)
        {
            Health += (int)value;
            if (Health <= 0)
            {
                Health = 0;
                OnDeath();
                Destroy(GetComponent<Rigidbody>());
                Destroy(this);
            }

            OnChangeHealth();
        }

        public void Destroyed()
        {
            Destroy(gameObject);
        }
    }
}