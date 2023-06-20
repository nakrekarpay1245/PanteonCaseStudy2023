using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UI Manager Parameters")]
    [Header("Information Parameters")]
    [Tooltip("Displays the information of the selected product")]
    [SerializeField]
    private GameObject informationMenu;
    [Tooltip("Displays the name of the selected product")]
    [SerializeField]
    private TextMeshProUGUI productNameText;
    [Tooltip("Displays the descriptions of the selected product")]
    [SerializeField]
    private TextMeshProUGUI productDescriptionText;
    [Tooltip("Displays the icon of the selected product")]
    [SerializeField]
    private Image productIcon;

    [Header("Production Parameters")]
    [Tooltip("Menu displaying producible items.")]
    [SerializeField]
    private GameObject productionMenu;
    [Tooltip("Menu displaying producible buildings.")]
    [SerializeField]
    private GameObject buildingProduceMenu;
    [Tooltip("Menu displaying producible soldiers.")]
    [SerializeField]
    private GameObject soldierProduceMenu;
    [Tooltip("Button producing selected soldiers.")]
    [SerializeField]
    private List<SoldierButton> soldierButtons;

    /// <summary>
    /// Displays the information menu with the specified name, description, and icon.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="icon"></param>
    public void DisplayInformationMenu(string name, string description, Sprite icon)
    {
        informationMenu.SetActive(true);
        productNameText.text = name;
        productDescriptionText.text = description;
        productIcon.sprite = icon;
    }

    /// <summary>
    /// Hides the information menu.
    /// </summary>
    public void HideInformationMenu()
    {
        informationMenu.SetActive(false);
    }

    /// <summary>
    /// Displays soldier produce buttons
    /// </summary>
    public void DisplaySoldierButtons()
    {
        soldierProduceMenu.SetActive(true);
    }

    /// <summary>
    /// Hides soldier produce buttons
    /// </summary>
    public void HideSoldierButtons()
    {
        soldierProduceMenu.SetActive(false);
        RemoveBarrackFromButtons();
    }

    /// <summary>
    /// Adds barracks to soldier produce buttons
    /// </summary>
    public void AddBarrackToButtons(Barracks barrack)
    {
        for (int i = 0; i < soldierButtons.Count; i++)
        {
            SoldierButton button = soldierButtons[i];
            button.SetBarrack(barrack);
        }
    }

    /// <summary>
    /// Removes barracks to soldier produce buttons
    /// </summary>
    public void RemoveBarrackFromButtons()
    {
        for (int i = 0; i < soldierButtons.Count; i++)
        {
            SoldierButton button = soldierButtons[i];
            button.SetBarrack(null);
        }
    }

    /// <summary>
    /// Displays building produce buttons
    /// </summary>
    public void DisplayBuildingButtons()
    {
        buildingProduceMenu.SetActive(true);
    }

    /// <summary>
    /// Hides building produce buttons
    /// </summary>
    public void HideBuildingButtons()
    {
        buildingProduceMenu.SetActive(false);
    }
}
