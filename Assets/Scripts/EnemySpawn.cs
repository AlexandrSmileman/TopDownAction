using System.Collections;
using System.Collections.Generic;
using TopDownAction;
using UnityEngine;
using UnityEngine.Serialization;

namespace TopDownAction
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField]
        private CreatureParameters _parameters;
        private GameObject _enemy;
        [SerializeField] private float respawnTime;
        private float _lastDeathTime;
        private bool _spawnCreature = true;

        private void OnEnable()
        {
            _lastDeathTime = Time.time;
        }

        private void Update()
        {
            if (_spawnCreature && Time.time - _lastDeathTime > respawnTime)
            {
                _spawnCreature = false;
                SpawnCreature();
            }
        }

        private void SpawnCreature()
        {
            Vector3 randomDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;
            Vector3 pos = transform.position + randomDirection * Random.Range(2f, 3f);
            _enemy = Instantiate(_parameters.prefab, transform.position, Quaternion.identity);
            var creature = _enemy.AddComponent<Creature>();
            creature.Constructor(TeamsTypes.Enemy, _parameters);
            creature.SetAi(new EnemyAi(creature)).SetDestination(pos);
            creature.OnDeath += CreatureDead;
        }

        private void CreatureDead()
        {
            _spawnCreature = true;
            _lastDeathTime = Time.time;
        }
    }
}