using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

    public HealthBarController healthBar;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        curHealth -= damage;

        healthBar.UpdateHealthBar((float)curHealth / maxHealth);

        if (curHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Ouch! Você perdeu!");
        }
    }
}
