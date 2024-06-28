using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Unit.Enemy.Base
{
    public class Enemy : Unit.Base.Unit
    {
        [SerializeField] protected EnemyConfig config;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected Slider healthView;
        [SerializeField] protected Animator animator;
        protected EnemyState state;
        protected float health;
        protected float speed;
        protected float damage;
        protected float reward;

        public void Init()
        {
            health = config.Health;
            speed = config.Speed;
            damage = config.Damage;
            reward = config.Reward;

            healthView.maxValue = config.Health;
        }

        private void Update()
        {
            switch(state)
            {
                case EnemyState.Walk:
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Death:
                    break;
            }
        }

        protected void OnWalk()
        {
            rb.velocity = new Vector3(-speed, 0, 0);
            animator.SetFloat("Speed", rb.velocity.x);
        }

        protected void OnAttack()
        {
            rb.velocity = Vector3.zero;
            animator.SetTrigger("Attack");
        }

        protected void OnDeath()
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Death");
            Destroy(gameObject);
        }

        private void ChangeState(EnemyState newState)
        {
            if (newState == state || state == EnemyState.Death) return;

            state = newState;

            switch(state)
            {
                case EnemyState.Walk:
                    break;

                case EnemyState.Attack:
                    break;

                case EnemyState.Death:
                    break;
            }
        }

        public void GetDamage(float damage)
        {
            health -= damage;

            health = Mathf.Clamp(health, 0, config.Health);

            if(health == 0)
            {
                OnDeath();
            }
        }

        protected enum EnemyState
        {
            Walk,
            Attack,
            Death
        }
    }
}

