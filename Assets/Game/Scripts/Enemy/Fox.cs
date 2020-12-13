using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class Fox : EnemyBase
    {
        private static readonly int VarRun = Animator.StringToHash("run");
        private SpriteRenderer _spriteRenderer;

        protected override void Start()
        {
            base.Start();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            animator.SetBool(VarRun, false);
        }

        private void Update()
        {
            animator.SetBool(VarRun, !rigid2D.IsSleeping());
            _spriteRenderer.flipX = rigid2D.velocity.x < 0;
        }
    }
}