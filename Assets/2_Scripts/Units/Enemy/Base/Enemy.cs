using System;
using System.Collections;
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
        [SerializeField] protected SpriteRenderer spriteRenderer;
        protected EnemyState state;
        private Action<Enemy> onDeath;
        protected Fence fence;
        protected float health;
        protected float speed;
        protected float damage;
        protected int reward;

        public void Init(Action<Enemy> onDeath)
        {
            health = config.Health;
            speed = config.Speed;
            damage = config.Damage;
            reward = config.Reward;

            healthView.maxValue = config.Health;
            healthView.value = config.Health;

            this.onDeath += onDeath;

            ChangeState(EnemyState.Walk, true);
        }

        private void Update()
        {
            switch(state)
            {
                case EnemyState.Walk:
                    OnWalk();
                    break;
                case EnemyState.Attack:
                    OnAttack();
                    break;
                case EnemyState.Death:
                    OnDeath();
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

        private void Attack()
        {
            fence.GetDamage(damage);
        }

        protected void OnDeath()
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Death");
            gameObject.SetActive(false);
            fence = null;
            spriteRenderer.color = Color.white;
            onDeath?.Invoke(this);
            Wallet.Instance.AddMoney(reward);
        }

        private void ChangeState(EnemyState newState, bool force = false)
        {
            if (!force)
            {
                if (newState == state || state == EnemyState.Death) return;
            }

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
            else
            {
                healthView.value = health;
                StartCoroutine(DamageAnimation());
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out Fence fence))
            {
                this.fence = fence;
                ChangeState(EnemyState.Attack);
            }
        }

        private IEnumerator DamageAnimation()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            spriteRenderer.color = Color.white;
        }

        protected enum EnemyState
        {
            Walk,
            Attack,
            Death
        }


    }
}

