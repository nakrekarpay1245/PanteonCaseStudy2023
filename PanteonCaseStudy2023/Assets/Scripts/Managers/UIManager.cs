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

    [Header("Production Parameters")]
    [Tooltip("It displays the products of the selected product")]
    [SerializeField]
    private GameObject productionMenu;
    [Tooltip("It displays the buildings that can be produced")]
    [SerializeField]
    private GameObject buildingProduceMenu;
    [Tooltip("It displays the soldiers that the selected barracks can produce")]
    [SerializeField]
    private GameObject soldierProduceMenu;
    [Tooltip("It displays the soldiers that the selected barracks can produce")]
    [SerializeField]
    private List<SoldierButton> produceSoldierButtonList;

    public void DisplayInformationMenu(string name, string description, Sprite icon)
    {
        informationMenu.SetActive(true);
        productNameText.text = name;
        productDescriptionText.text = description;
        productIcon.sprite = icon;
    }
    public void HideInformationMenu()
    {
        //Debug.Log("Information Menu Hided!");
        informationMenu.SetActive(false);
    }


    public void DisplaySoldierButtons()
    {
        //Debug.Log("Soldiers Displayed!");
        soldierProduceMenu.SetActive(true);
    }

    public void HideSoldierButtons()
    {
        //Debug.Log("Soldiers Hided!");
        soldierProduceMenu.SetActive(false);
        RemoveBarrackToButton();
    }

    public void AddBarrackToButton(Barrack barrack)
    {
        foreach (SoldierButton produceSoldierButton in produceSoldierButtonList)
        {
            produceSoldierButton.SetBarrack(barrack);
        }
    }

    public void RemoveBarrackToButton()
    {
        foreach (SoldierButton produceSoldierButton in produceSoldierButtonList)
        {
            produceSoldierButton.SetBarrack(null);
        }
    }

    public void DisplayBuildingButtons()
    {
        //Debug.Log("Buildings Displayed!");
        buildingProduceMenu.SetActive(true);
    }
    public void HideBuildingButtons()
    {
        //Debug.Log("Buildings Hided!");
        buildingProduceMenu.SetActive(false);
    }
}
