using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyWaypoint : MonoBehaviour
    {
        [SerializeField] private List<Vector2> path;
        [SerializeField] private float radius = 0.5f;

        [SerializeField] private Rigidbody2D target;
        private Queue<Vector2> _queuePos = new Queue<Vector2>();
        private readonly Queue<Vector2> _reverseQueuePos = new Queue<Vector2>();

        private Vector2? _currentTargetPos = null;
        [SerializeField] private float speed = 5f;

        private void Start()
        {
            if (path.Count <= 0) return;
            target.transform.position = path[0];
            foreach (Vector2 pos in path) _queuePos.Enqueue(pos);
        }

        private void OnDrawGizmosSelected()
        {
            if (path.Count <= 0) return;
            Gizmos.DrawSphere(path[0], radius);
            for (var i = 1; i < path.Count; ++i)
            {
                Gizmos.DrawSphere(path[i], radius);
                Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }

        private void Update()
        {
            if (!_currentTargetPos.HasValue)
            {
                if (_queuePos.Count > 0)
                {
                    _currentTargetPos = _queuePos.Dequeue();
                    _reverseQueuePos.Enqueue(_currentTargetPos.Value);
                }
                else
                {
                    _queuePos = new Queue<Vector2>(_reverseQueuePos.Reverse());
                }

                return;
            }

            Vector2 dir = _currentTargetPos.GetValueOrDefault() - target.position;
            target.velocity = dir.normalized * speed;
        }

        private void LateUpdate()
        {
            if (!target)
            {
                Destroy(this);
                return;
            }

            if (!_currentTargetPos.HasValue) return;
            var distance = Vector2.Distance(_currentTargetPos.GetValueOrDefault(), target.position);
            if (distance <= radius)
            {
                _currentTargetPos = null;
                target.velocity = Vector2.zero;
            }
        }
    }
}