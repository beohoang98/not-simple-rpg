using System;
using UnityEngine;

namespace Game.Scripts.Objects
{
    [DisallowMultipleComponent]
    public class PositionSortLayer : MonoBehaviour
    {
        private int _sortingBase = 5000;
        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            _renderer.sortingOrder = _sortingBase - (int) transform.position.y;
        }
    }
}