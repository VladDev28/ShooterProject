using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 200;
    private HealthSystem healthSystem;
    private HealthBar healthBar;
    private Vector3 StartingPos;

    void Start()
    {
        StartingPos = transform.position;
        healthSystem = new HealthSystem(maxHealth);


        healthBar = GetComponentInChildren<HealthBar>();


        if (healthBar != null)
        {
            healthBar.Setup(healthSystem);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
        if (healthSystem.IsDead())
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    public Vector3 GetRoamingPosition()
    {
        return StartingPos + GetRandomDir() * Random.Range(10f,50f);
    }
}
