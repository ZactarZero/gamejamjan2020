using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject fillingHealthBar;
    public GameObject totalHealthBar;
    public float scaleToPositionRatio = 0.32f;
    public float healthBarWidth = 2f;

    private float xPosition;

    public void UpdateHealthBar(float healthPercentage)
    {
        fillingHealthBar.transform.localScale = new Vector2((1 - healthPercentage) * healthBarWidth, fillingHealthBar.transform.localScale.y);

        xPosition = totalHealthBar.transform.localScale.x * scaleToPositionRatio - scaleToPositionRatio * fillingHealthBar.transform.localScale.x;

        fillingHealthBar.transform.localPosition = new Vector2(xPosition, fillingHealthBar.transform.localPosition.y);
    }
}
