using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    const float MAX_HEALTH = 1;

    float _maxHealth;
    float _healthMultiplier;
    float _curHealth;

    void Start()
    {
        this._maxHealth = MAX_HEALTH;
        this._curHealth = MAX_HEALTH;
        this._healthMultiplier = 1;
    }

    public float GetMaxHealth() => this._maxHealth * this._healthMultiplier;
    public float GetHealth() => this._curHealth;
    public void SetHealth(float value) => this._curHealth = Mathf.Min(value, this.GetMaxHealth());


    public void SetHealthMultiplier(float value)
    {
        float curHealthPercent = this.GetHealth() / this.GetMaxHealth();
        this._healthMultiplier = value;
        this.SetHealth(this.GetMaxHealth() * curHealthPercent);
    }

    public void InflictDamage(float damage)
    {
        this.SetHealth(this._curHealth - damage);
        if (this._curHealth <= 0) this.Die();
    }

    public void Heal(float value)
    {
        this.SetHealth(this._curHealth + value);
    }

    protected void Die()
    {
        Debug.Log("Urh I'm dead");
        GameObject.Destroy(this.gameObject);
    }
}
