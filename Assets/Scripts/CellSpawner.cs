using System;
using UnityEngine;

namespace BattleCity
{
    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _cellPrefab;

        public Vector3 cellPosition;

        private void Start()
        {
            var cell = Instantiate(_cellPrefab, cellPosition, Quaternion.identity).AddComponent<Cell>();
        }
    }
}