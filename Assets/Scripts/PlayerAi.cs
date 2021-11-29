using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace TopDownAction
{
    public class PlayerAi : CreatureAi
    {
        public PlayerAi(Creature creature) : base(creature)
        {
            var selectable = creature.gameObject.AddComponent<Selectable>();
            selectable.Constructor(creature.transform);
            creature.OnDeath += selectable.OnDestroy;
            selectable.OnSelect += () => { InputManager.OnSetDestination += SetDestination; };
            selectable.OnDeselect += () => { InputManager.OnSetDestination -= SetDestination; };
        }
        
    }
}