using UnityEngine;
using UnityEngine.AI;

namespace TopDownAction
{
    public class CreatureAi : IMovable
    {
        protected Creature _creature;
        protected Movement _movement;
        private DestroyableCollisionEvents _collisionEvents;
        protected IDestroyable _target;
        private CreatureAnimation _animation;

        private const float MIN_ATTACK_DISTANCE = 2.5f;
        
        private bool attacking = false;
        private bool ignoreTargets = false;
        private Vector3 _destination;

        public CreatureAi(Creature creature) 
        {
            _creature = creature;
            _creature.OnUpdate += OnUpdate;
            
            _collisionEvents = creature.gameObject.GetComponentInChildren<DestroyableCollisionEvents>();
            _collisionEvents.Constructor(creature);
            _collisionEvents.OnTargetAdded += OnTargetAdded;
            
            _animation = _creature.gameObject.GetComponent<CreatureAnimation>();
            _animation.Constructor(_creature);
            _animation.OnHitEvent += HitTarget;
            
            _movement = new Movement(_creature.GetComponent<NavMeshAgent>(), _creature, _creature);
            _movement.OnStopped += OnStopped;
            
        }

        public virtual void OnUpdate()
        {
            if (_target != null && _target.Equals(null))
            {
                _target = null;//target is dead
                _movement.MoveToPosition(_destination);
                _animation.Walk();
            }
            
            if (_target == null  && _collisionEvents.HasTarget)
            {
                _target = _collisionEvents.GetTarget();
            }
            
            if (_target != null)
            {
                float distance = _movement.RemainingDistance;
                if (distance > MIN_ATTACK_DISTANCE)
                {
                    attacking = false;
                    GetCloseToTarget(); //may not update every frame, but e.g. twice a second
                }
                else
                    Attack();
            }
        }

        private void OnStopped()
        {
            ignoreTargets = false;
            if(!_animation.CurrentType.Equals(AnimationTypes.Attack))
                _animation.Idle();
        }
        
        private void GetCloseToTarget()
        {
            if(_target.Equals(null))
                return;
            _movement.MoveToPosition(_target.Position);
            _animation.Walk();
        }

        private void Attack()
        {
            if (!attacking)
            {
                attacking = true;
                _animation.Attack();
            }
        }
        public void HitTarget()
        {
            if (_target != null && !_target.Equals(null))
            {
                _target.ChangeHealth(-_creature.Parameters.damage);
            }
        }

        private void OnTargetAdded()
        {
            if (!ignoreTargets && _target == null)
            {
                _target = _collisionEvents.GetTarget();
                if(_target != null)
                    GetCloseToTarget();
            }
        }

        public void SetDestination(Vector3 position)
        {
            //if a target exists, ignore the target and move to a position
            ignoreTargets = _target == null ? false : true;
            if(ignoreTargets)
                _collisionEvents.ClearTargets();
            _movement.MoveToPosition(position);
            _animation.Walk();
            _destination = position;
        }
    }
}