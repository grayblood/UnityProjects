using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/create new ball item")]
public class BallItem : ItemBase
{

    [Header("Ratio Captura")]
    [SerializeField] int ratio;
    [SerializeField] bool instantaneo;

    

}
