using Unit.Enemy.Base;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;

    private void Start()
    {
        Invoke("Destroy", lifeTime);
        rb.velocity = transform.up * speed;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnColliderEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.GetDamage(damage);
            Destroy(gameObject);
        }

        Debug.Log($"Collider with {other.gameObject.name}");
    }
}
