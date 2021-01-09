using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject teste;
    public TileController[,] grid = new TileController[19, 11];

    void Start()
    {
        for (int i=0; i<19; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                grid[i, j] = null;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector2 pos = new Vector2((float)Math.Round(hit.point.x, MidpointRounding.ToEven), (float)Math.Round(hit.point.y, MidpointRounding.ToEven));
                
                if (grid[(int)pos.x + 9, (int)pos.y + 5] != null)
                {
                    Debug.Log("Tile ja existe");
                } 
                else
                {
                    TileController newTile = Instantiate(teste, pos, Quaternion.identity).GetComponent<TileController>();
                    newTile.topIsOut = true;
                    newTile.bottomIsOut = false;
                    newTile.rightIsOut = true;
                    newTile.leftIsOut = false;
                    newTile.UpdateSides();
                    grid[(int)pos.x + 9, (int)pos.y + 5] = newTile;
                    Debug.Log((pos.x + 9) + ", " + (pos.y + 5));
                }
            }
        }
    }
}
