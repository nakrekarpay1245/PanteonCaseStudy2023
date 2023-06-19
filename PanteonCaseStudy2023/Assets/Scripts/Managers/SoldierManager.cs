using UnityEngine;

public class SoldierManager : MonoSingleton<SoldierManager>
{
    [Header("Building Manager Parameters")]
    [Header("Building References")]
    [Tooltip("The target building to attack")]
    [SerializeField]
    private Building targetBuilding;


}
