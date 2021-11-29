using System;
using UnityEngine;

namespace TopDownAction
{
    public interface IDestroyable
    {
        public event Action OnDeath;
        public event Action OnChangeHealth;
        
        public int Health { get; }
        public void ChangeHealth(float value);

        public TeamsTypes TeamType { get; }
        
        public Vector3 Position { get; }

        public void Destroyed();
    }
}