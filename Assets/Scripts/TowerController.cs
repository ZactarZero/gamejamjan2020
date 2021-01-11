using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

    public HealthBarController healthBar;

    public PlayerController pc;
    public Text gameOverText;

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
            gameOverText.text = "Game Over\n\nscore: " + pc.points;
            gameOverText.gameObject.SetActive(true);
            Debug.Log("Ouch! Você perdeu!");
        }
    }
}
