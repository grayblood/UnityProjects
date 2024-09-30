using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/create new recovery item")]
public class RecoveryItem : ItemBase
{

    [Header("HP")]
    [SerializeField] int hpAmount;
    [SerializeField] bool restoreMaxHp;

    [Header("PP")]
    [SerializeField] int ppAmount;
    [SerializeField] bool restoreMaxPP;

    [Header("Revive")]
    [SerializeField] bool revive;
    [SerializeField] bool maxRevive;

}
