using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/create new stat item")]
public class StatObject : ItemBase
{

    [Header("Atk")]
    [SerializeField] int atk;
    [Header("def")]
    [SerializeField] int def;
    [Header("Vel")]
    [SerializeField] int vel;
    [Header("HP")]
    [SerializeField] int hp;

    [Header("HP%")]
    [SerializeField] int hpPorcentaje;



}
