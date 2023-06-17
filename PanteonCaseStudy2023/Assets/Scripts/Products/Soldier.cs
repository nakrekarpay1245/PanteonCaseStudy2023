using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [Header("Soldier Parameters")]
    [Header("Soldier Informations")]
    [Tooltip("It holds the name of the soldier")]
    [SerializeField]
    protected string soldierName;
    [Tooltip("It holds the name of the soldier")]
    [TextArea(3, 3)]
    [SerializeField]
    protected string soldierDescription;
    [Tooltip("It holds the icon of the soldier")]
    [SerializeField]
    private Sprite soldierIcon;

    [Header("Soldier Movement")]
    [Tooltip("Soldier moveSpeed")]
    [SerializeField]
    private float speed;

    private void DisplayInformation()
    {
        Debug.Log("Information Displayed!");
        UIManager.singleton.DisplayInformation(soldierName, soldierDescription, soldierIcon);
    }

    public void Select()
    {
        DisplayInformation();
    }
}
