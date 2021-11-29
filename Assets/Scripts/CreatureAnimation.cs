using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownAction
{
    public partial class CreatureAnimation : MonoBehaviour
    {
        private Animator _animator;
        //private Creature _creature;
        //private string _currentState = "";
        public Action OnDestroy = () => { };
        public Action OnHitEvent = () => { };
        public AnimationTypes CurrentType { get; private set; }

        public void Constructor(Creature creature)
        {
            creature.OnDeath += Death;
            //_creature = creature;
            _animator = GetComponent<Animator>();
            
            CurrentType = AnimationTypes.Idle;
        }

        public void Walk()
        {
            if (!CurrentType.Equals(AnimationTypes.Walk))
            {
                CurrentType = AnimationTypes.Walk;
                _animator.SetTrigger(AnimationTypes.Walk.ToString());
            }
        }

        public void Idle()
        {
            _animator.SetTrigger(AnimationTypes.Idle.ToString());
            CurrentType = AnimationTypes.Idle;
        }

        public void Attack()
        {
            if (!CurrentType.Equals(AnimationTypes.Attack))
            {
                CurrentType = AnimationTypes.Attack;
                _animator.SetTrigger(AnimationTypes.Attack.ToString());
            }
        }

        public void Death()
        {
            _animator.SetTrigger(AnimationTypes.Death.ToString());
            CurrentType = AnimationTypes.Death;
        }

        //event in attack animation
        public void HitEvent()
        {
            //_creature.HitTarget();
            OnHitEvent();
        }

        //event in death animation
        public void DestroyEvent()
        {
            Destroy(gameObject);
        }
        

    }
}