using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Collider2D bulletCollider;

    void Start()
    {
        bulletCollider = GetComponent<Collider2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();

            if (playerCollider != null)
            {
                // Ignore collisions between the bullet and the player
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponect))
        {
            int damage = UnityEngine.Random.Range(20, 30);
            enemyComponect.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
