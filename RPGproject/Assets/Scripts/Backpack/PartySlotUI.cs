using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySlotUI : MonoBehaviour
{

    [SerializeField] Text pokeText;
    [SerializeField] Image pokeImage;


    
    internal void setData(BasePokemon pokeSlot)
    {
        pokeText.text = pokeSlot.name;
        pokeImage.sprite = pokeSlot.sprite;
    }
}
