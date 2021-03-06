﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject tilePrefab, cardPrefab;
    public Transform[] cardPositions = new Transform[5];
    public Transform canvas;
    public Sprite topImageIn, topImageOut, bottomImageIn, bottomImageOut, rightImageIn, rightImageOut, leftImageIn, leftImageOut;
    public Sprite assaultTurret;
    public Text pointsUI;

    public CardController selectedCard;
    public TileController[,] grid = new TileController[19, 11];
    public CardController[] cardsInHand = new CardController[5];

    public float points = 0;
    public float enemiesKilled = 0;
    public float lastCardPoints = 0;

    void Start()
    {
        
        for (int i=0; i<19; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                grid[i, j] = null;
            }
        }
        GameObject[] ground_tiles = GameObject.FindGameObjectsWithTag("Ground");
        for (int i = 0; i < ground_tiles.Length; i++)
        {
            grid[(int)ground_tiles[i].transform.position.x + 9, (int)ground_tiles[i].transform.position.y + 5] = ground_tiles[i].GetComponent<TileController>();
        }
        for (int i = 0; i < 5; i++)
        {
            cardsInHand[i] = null;
        }
        StartCoroutine(SpawnCardsDelay());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100) && selectedCard != null)
            {
                Vector2 pos = new Vector2((float)Math.Round(hit.point.x, MidpointRounding.ToEven), (float)Math.Round(hit.point.y, MidpointRounding.ToEven));
                
                if (grid[(int)pos.x + 9, (int)pos.y + 5] != null)
                {
                    Debug.Log("Tile ja existe");
                } 
                else
                {
                    if (!(((((int)pos.x + 9 - 1) < 0) || (grid[(int)pos.x + 9 - 1, (int)pos.y + 5] == null))
                        && ((((int)pos.x + 9 + 1) >= 19) || (grid[(int)pos.x + 9 + 1, (int)pos.y + 5] == null))
                        && ((((int)pos.y + 5 - 1) < 0) || (grid[(int)pos.x + 9, (int)pos.y + 5 - 1] == null))
                        && ((((int)pos.y + 5 + 1) >= 11) || (grid[(int)pos.x + 9, (int)pos.y + 5 + 1] == null))))
                    {
                        if (((((int)pos.x + 9 - 1) < 0) || (grid[(int)pos.x + 9 - 1, (int)pos.y + 5] == null) || (grid[(int)pos.x + 9 - 1, (int)pos.y + 5].rightIsOut != selectedCard.cardSO.leftIsOut))
                            && ((((int)pos.x + 9 + 1) >= 19) || (grid[(int)pos.x + 9 + 1, (int)pos.y + 5] == null) || (grid[(int)pos.x + 9 + 1, (int)pos.y + 5].leftIsOut != selectedCard.cardSO.rightIsOut))
                            && ((((int)pos.y + 5 - 1) < 0) || (grid[(int)pos.x + 9, (int)pos.y + 5 - 1] == null) || (grid[(int)pos.x + 9, (int)pos.y + 5 - 1].topIsOut != selectedCard.cardSO.bottomIsOut))
                            && ((((int)pos.y + 5 + 1) >= 11) || (grid[(int)pos.x + 9, (int)pos.y + 5 + 1] == null) || (grid[(int)pos.x + 9, (int)pos.y + 5 + 1].bottomIsOut != selectedCard.cardSO.topIsOut)))
                        {
                            SpawnTile(pos);
                        }
                    }
                    
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCard();
        }
    }

    void SpawnTile(Vector2 pos)
    {
        TileController newTile = Instantiate(tilePrefab, pos, Quaternion.identity).GetComponent<TileController>();
        newTile.topIsOut = selectedCard.cardSO.topIsOut;
        newTile.bottomIsOut = selectedCard.cardSO.bottomIsOut;
        newTile.rightIsOut = selectedCard.cardSO.rightIsOut;
        newTile.leftIsOut = selectedCard.cardSO.leftIsOut;
        newTile.turretType = selectedCard.cardSO.turretType;
        newTile.UpdateSides();

        grid[(int)pos.x + 9, (int)pos.y + 5] = newTile;
        Debug.Log((pos.x + 9) + ", " + (pos.y + 5));

        Destroy(selectedCard.gameObject);
        selectedCard = null;
    }

    void SpawnCard()
    {
        CardSO cardSO = ScriptableObject.CreateInstance("CardSO") as CardSO;
        System.Random rand = new System.Random();
        cardSO.topIsOut = rand.NextDouble() >= 0.5;
        cardSO.bottomIsOut = rand.NextDouble() >= 0.5;
        cardSO.rightIsOut = rand.NextDouble() >= 0.5;
        cardSO.leftIsOut = rand.NextDouble() >= 0.5;
        cardSO.topImage = cardSO.topIsOut ? topImageOut : topImageIn;
        cardSO.bottomImage = cardSO.bottomIsOut ? bottomImageOut : bottomImageIn;
        cardSO.rightImage = cardSO.rightIsOut ? rightImageOut : rightImageIn;
        cardSO.leftImage = cardSO.leftIsOut ? leftImageOut : leftImageIn;
        cardSO.turretImage = assaultTurret;
        cardSO.turretType = TurretType.Assault;
        Debug.Log(cardSO.topIsOut + ", " + cardSO.bottomIsOut + ", " + cardSO.rightIsOut + ", " + cardSO.leftIsOut);
        for (int i = 0; i < 5; i++)
        {
            if (cardsInHand[i] == null)
            {
                CardController newCard = Instantiate(cardPrefab, cardPositions[i].position, Quaternion.identity, canvas).GetComponent<CardController>();
                newCard.cardSO = cardSO;
                newCard.UpdateImages();
                cardsInHand[i] = newCard;
                break;
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        points += 100;

        if (points - lastCardPoints >= 1250)
        {
            for (int i = 0; i < 5; i++)
            {
                if (cardsInHand[i] == null)
                {
                    SpawnCard();
                    lastCardPoints = points;
                    break;
                }
            }
        }
        pointsUI.text = "Score: " + points;
    }

    IEnumerator SpawnCardsDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnCard();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnCard();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnCard();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnCard();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnCard();
    }
}
