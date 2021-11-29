using System.ComponentModel;
using UnityEngine;

namespace TopDownAction
{
    public class HomeBase : MonoBehaviour
    {
        [SerializeField]
        private CreatureParameters _creatureParameters;

        public CreatureParameters CreatureParameters { get { return _creatureParameters; } }

        private int _countToProduce;

        public int CountToProduce { get { return _countToProduce; } }
        
        [SerializeField]
        [Description("Unit production time in seconds")]
        [Range(1, 10)]
        private float productionTime = 10f;

        private float _сurrentProductionTime;

        //production value from 0 to 1
        public float ProductionState { get { return _сurrentProductionTime / productionTime; } }

        private void Update()
        {
            ProductionUpdate();
        }

        private void ProductionUpdate()
        {
            if (_countToProduce > 0)
            {
                _сurrentProductionTime += Time.deltaTime;
                if (_сurrentProductionTime >= productionTime)
                {
                    _сurrentProductionTime -= productionTime;
                    _countToProduce--;
                    SpawnUnit();
                }
            }
            else
                _сurrentProductionTime = 0;
        }

        private void SpawnUnit()
        {
            var go = Instantiate(_creatureParameters.prefab, transform.position, Quaternion.identity);
            var creature = go.AddComponent<Creature>();
            creature.Constructor(TeamsTypes.Player, _creatureParameters);
            Vector3 randomDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;
            Vector3 pos = transform.position + randomDirection * 3f;
            creature.SetAi(new PlayerAi(creature)).SetDestination(pos);
        }

        internal int AddUnits(int count)
        {
            _countToProduce += count;
            return _countToProduce;
        }
    }
}