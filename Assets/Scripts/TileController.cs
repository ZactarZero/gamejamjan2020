using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileController : MonoBehaviour
{
    public bool topIsOut, bottomIsOut, rightIsOut, leftIsOut;
    public GameObject topOut, bottomOut, rightOut, leftOut;
    public GameObject topIn, bottomIn, rightIn, leftIn;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void UpdateSides()
    {
        if (topIsOut)
        {
            topOut.SetActive(true);
            topIn.SetActive(false);
        }
        else
        {
            topOut.SetActive(false);
            topIn.SetActive(true);
        }

        if (bottomIsOut)
        {
            bottomOut.SetActive(true);
            bottomIn.SetActive(false);
        }
        else
        {
            bottomOut.SetActive(false);
            bottomIn.SetActive(true);
        }

        if (rightIsOut)
        {
            rightOut.SetActive(true);
            rightIn.SetActive(false);
        }
        else
        {
            rightOut.SetActive(false);
            rightIn.SetActive(true);
        }

        if (leftIsOut)
        {
            leftOut.SetActive(true);
            leftIn.SetActive(false);
        }
        else
        {
            leftOut.SetActive(false);
            leftIn.SetActive(true);
        }
    }
}
