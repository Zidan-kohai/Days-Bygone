using Base.Data;
using Unit.Enemy.Base;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;
    private int counter = 0;
    public void Init(int damage)
    {
        Invoke("Destroy", lifeTime);
        rb.velocity = transform.up * speed;
        this.damage = damage;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (counter > 0) return;

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            counter++;
            enemy.GetDamage(damage);
            Destroy(gameObject);
        }

        Debug.Log($"Collider with {collision.gameObject.name}");
    }
}
