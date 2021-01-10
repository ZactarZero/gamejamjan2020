using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileController : MonoBehaviour
{
    public bool topIsOut, bottomIsOut, rightIsOut, leftIsOut;
    public GameObject topOut, bottomOut, rightOut, leftOut;
    public GameObject topIn, bottomIn, rightIn, leftIn;
    public TurretType turretType;

    public bool isGroundTile;
    public GameObject nextGroundTile;

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateSides()
    {
        topOut.SetActive(topIsOut);
        topIn.SetActive(!topIsOut);

        bottomOut.SetActive(bottomIsOut);
        bottomIn.SetActive(!bottomIsOut);

        rightOut.SetActive(rightIsOut);
        rightIn.SetActive(!rightIsOut);

        leftOut.SetActive(leftIsOut);
        leftIn.SetActive(!leftIsOut);
    }
}
