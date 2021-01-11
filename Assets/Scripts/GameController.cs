using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyStartingTile;
    public int[] enemiesPerWave;
    public float enemySpawnDelay;
    public float waveTransitionDelay;

    private int currentWave = 0;
    private bool haveAllWavesFinished = false;

    private bool hasEnemySpawned = false;
    private float enemySpawnTimer = 0f;

    private bool hasWaveFinished = false;
    private float waveTransitionTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!haveAllWavesFinished)
        {
            if (!hasWaveFinished)
            {
                if (enemiesPerWave[currentWave] > 0)
                {
                    if (!hasEnemySpawned)
                    {
                        enemiesPerWave[currentWave]--;

                        GameObject enemy = Instantiate(enemyPrefab);
                        enemy.GetComponent<EnemyController>().startingTile = enemyStartingTile;

                        hasEnemySpawned = true;
                    }
                }
                else
                {
                    hasWaveFinished = true;
                    currentWave++;

                    if (currentWave > enemiesPerWave.Length - 1)
                    {
                        haveAllWavesFinished = true;
                    }
                }
            }
            else
            {
                if (waveTransitionTimer < waveTransitionDelay)
                {
                    waveTransitionTimer += Time.deltaTime;
                }
                else
                {
                    waveTransitionTimer = 0f;
                    hasWaveFinished = false;
                }
            }

            if (hasEnemySpawned)
            {
                if (enemySpawnTimer < enemySpawnDelay)
                {
                    enemySpawnTimer += Time.deltaTime;
                }
                else
                {
                    enemySpawnTimer = 0f;
                    hasEnemySpawned = false;
                }
            }
        } 
        else
        {
            Debug.Log("Parabéns, você venceu!");
        }
    }
}
