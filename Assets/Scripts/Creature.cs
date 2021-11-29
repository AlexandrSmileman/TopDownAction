using System;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownAction
{
    [RequireComponent(typeof(NavMeshAgent))]
    //[RequireComponent(typeof(CreatureAnimation))]
    [RequireComponent(typeof(Animator))]
    //[RequireComponent(typeof(CreatureCollisionEvents))]
    public class Creature : MonoBehaviour, IDestroyable
    {
        private TeamsTypes _teamType;
        public TeamsTypes TeamType { get { return _teamType; } }
        private CreatureParameters _parameters;
        public CreatureParameters Parameters { get { return _parameters; } }
        private CreatureCollisionEvents _collisionEvents;
        public CreatureCollisionEvents CollisionEvents{ get { return _collisionEvents; } }
        private CreatureAi _creatureAi;
        private int _health;
        public int Health => _health;
        public Vector3 Position => transform.position;
        public Action OnUpdate = () => { };
        public event Action OnDeath = () => { };
        public event Action OnChangeHealth = () => { };
        public virtual void Constructor(TeamsTypes teamType, CreatureParameters parameters)
        {
            _parameters = parameters;
            _teamType = teamType;
            _health = _parameters.health;
            _collisionEvents = gameObject.AddComponent<CreatureCollisionEvents>();
            _collisionEvents.Constructor(this);

            //_creatureAi = new CreatureAi(this, _movement, _collisionEvents, _animation);
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
            _health += (int)value;
            if (_health <= 0)
            {
                _health = 0;
                OnDeath();
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<Collider>());
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