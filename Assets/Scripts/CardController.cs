﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public CardSO cardSO;
    public Image topImage;
    public Image bottomImage;
    public Image rightImage;
    public Image leftImage;
    public Image turretImage;
    public Text cardNameText;

    private MeshCollider clickLayer;
    private PlayerController pc;

    void Start()
    {
        clickLayer = GameObject.FindGameObjectsWithTag("ClickLayer")[0].GetComponent<MeshCollider>();
        pc = GameObject.FindGameObjectsWithTag("PlayerController")[0].GetComponent<PlayerController>();
    }

    public void UpdateImages()
    {
        topImage.sprite = cardSO.topImage;
        bottomImage.sprite = cardSO.bottomImage;
        rightImage.sprite = cardSO.rightImage;
        leftImage.sprite = cardSO.leftImage;
        turretImage.sprite = cardSO.turretImage;
        cardNameText.text = "Assault Turret";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        clickLayer.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clickLayer.enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pc.selectedCard = this;
    }
}
