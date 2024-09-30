using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonParty : MonoBehaviour
{
   
    [SerializeField] List<BasePokemon> pokemons;

    public List<BasePokemon> PokeSlots => pokemons;

    public static PokemonParty GetPokemonParty()
    {
        return FindObjectOfType<PlayerController>().GetComponent<PokemonParty>();
    }


}


[Serializable]
public class PokeSlot
{
    [SerializeField] BasePokemon poke;
    [SerializeField] Image image;


    public BasePokemon Poke => poke;
    public Image pokeImage => image;
}


