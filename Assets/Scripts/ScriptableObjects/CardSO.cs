using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSO : ScriptableObject
{
    public string cardName;

    public Sprite turretImage;
    public TurretType turretType;

    public Sprite topImage, bottomImage, rightImage, leftImage;
    public bool topIsOut, bottomIsOut, rightIsOut, leftIsOut;

}
