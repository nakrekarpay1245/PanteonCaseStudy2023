using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UI Manager Parameters")]
    [Header("Information Parameters")]
    [Tooltip("It displays the information of the selected product")]
    [SerializeField]
    private GameObject informationMenu;
    [Tooltip("It displays the information of the selected product")]
    [SerializeField]
    private TextMeshProUGUI productNameText;
    [Tooltip("It displays the information of the selected product")]
    [SerializeField]
    private TextMeshProUGUI productDescriptionText;
    [Tooltip("It displays the information of the selected product")]
    [SerializeField]
    private Image productIcon;

    public void DisplayInformation(string name, string description, Sprite icon)
    {
        productNameText.text = name;
        productDescriptionText.text = description;
        productIcon.sprite = icon;
    }

    public void DisplaySoldiers()
    {
        Debug.Log("Soldiers Displayed!");
    }
}
