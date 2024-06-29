using Unit.Enemy.Base;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;
    private int counter = 0;
    private void Start()
    {
        Invoke("Destroy", lifeTime);
        rb.velocity = transform.up * speed;
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
