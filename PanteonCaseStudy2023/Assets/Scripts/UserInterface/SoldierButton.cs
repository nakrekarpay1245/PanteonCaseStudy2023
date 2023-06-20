using UnityEngine;
using UnityEngine.UI;

public class SoldierButton : MonoBehaviour
{
    [Header("Soldier Button Parameters")]
    [Tooltip("The button component underneath the object")]
    [SerializeField]
    private Button buttonComponent;
    [Tooltip("Reference to the soldier that will be created when the button is clicked")]
    [SerializeField]
    private EntityPrefab entityPrefab;

    /// <summary>
    /// Barracks that creates soldiers when clicked.
    /// </summary>
    private Barracks barracks;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.interactable = true;
        buttonComponent.onClick.AddListener(ProduceSoldier);
    }

    public void SetBarrack(Barracks barrack)
    {
        this.barracks = barrack;
    }

    private void ProduceSoldier()
    {
        barracks?.ProduceSoldier(entityPrefab);
    }
}
