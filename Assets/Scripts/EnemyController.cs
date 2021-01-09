using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public float tileEnteringDistance = 0.1f;

    public int maxHealth = 100;
    private int curHealth;

    public GameObject startingTile;
    private GameObject currentTile;
    private GameObject nextTile;

    public HealthBarController healthBar;

    private float targetX;
    private float targetY;

    private int xDirection;
    private int yDirection;

    // Start is called before the first frame update
    void Start()
    {
        SetNextTile(startingTile);

        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < targetX + tileEnteringDistance
            && transform.position.x > targetX - tileEnteringDistance
            && transform.position.y < targetY + tileEnteringDistance
            && transform.position.y > targetY - tileEnteringDistance) 
        {
            SetNextTile(nextTile);
        }

        transform.Translate(new Vector2(xDirection, yDirection) * speed * Time.deltaTime);
    }

    private void SetNextTile(GameObject nextCurrentTile)
    {
        currentTile = nextCurrentTile;
        nextTile = currentTile.GetComponent<TileController>().nextGroundTile;

        if (nextTile == null)
        {
            Destroy(gameObject);
        }

        targetX = nextTile.transform.position.x;
        targetY = nextTile.transform.position.y;

        SetNextDirections();
    }

    private void SetNextDirections()
    {
        Vector2 vectorDirection = (new Vector2(targetX, targetY) - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (vectorDirection.x > 0.5)
        {
            xDirection = 1;
            yDirection = 0;
        }
        else if (vectorDirection.x < -0.5)
        {
            xDirection = -1;
            yDirection = 0;
        }
        else if (vectorDirection.y > 0.5)
        {
            xDirection = 0;
            yDirection = 1;
        }
        else if (vectorDirection.y < -0.5)
        {
            xDirection = 0;
            yDirection = -1;
        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;

        healthBar.UpdateHealthBar((float) curHealth / maxHealth);

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}


