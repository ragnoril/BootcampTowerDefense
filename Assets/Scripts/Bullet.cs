using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;

    public float Speed;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
    }
    public void Go()
    {
        rigidbody.velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();

            GameManager.Instance.Events.EnemyHit(enemy, Damage);
        }

        Destroy(this.gameObject);
    }
}
