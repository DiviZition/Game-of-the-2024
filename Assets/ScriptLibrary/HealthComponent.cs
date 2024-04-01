using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int _startHealth;
    [SerializeField] private float _damageInvulnerabilityTime;

    private float _invincibilityTimer;

    public int BaseHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    private void Start()
    {
        BaseHealth = _startHealth;
        CurrentHealth = BaseHealth;
    }

    public void TakeDamaget(int damage)
    {
        if (_invincibilityTimer > Time.time)
            return;

        _invincibilityTimer = Time.time + _damageInvulnerabilityTime;

        if(CurrentHealth - damage <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            CurrentHealth -= damage;
        }
    }

    public void Die()
    {
        Debug.Log($"{this.gameObject.name} has died");
    }
}