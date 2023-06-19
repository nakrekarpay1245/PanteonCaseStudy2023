using System;
using UnityEngine;
using UnityEngine.UI;

public class SoldierButton : MonoBehaviour
{
    [Header("Soldier Button Parameters")]
    [Tooltip("The button component underneath the object")]
    [SerializeField]
    private Button buttonComponent;

    [Tooltip("When clicked, the function inside it will be called.")]
    [SerializeField]
    private Barrack barrack;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.interactable = true;
        buttonComponent.onClick.AddListener(ProduceSoldier);
    }

    public void SetBarrack(Barrack barrack)
    {
        this.barrack = barrack;
        //Debug.Log("barrack: " + barrack);
    }

    private void ProduceSoldier()
    {
        barrack?.ProduceSoldier();
    }
}
