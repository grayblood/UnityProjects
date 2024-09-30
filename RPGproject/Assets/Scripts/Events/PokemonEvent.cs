using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonEvent : MonoBehaviour
{
    [SerializeField]
    private ScriptableBool m_pokebool;
    [SerializeField]
    private GameEvent m_OnPokemon;

    private void Awake()
    {
        m_pokebool.OpenMenu = false;
    }

    public void OnMenu()
    {

        m_pokebool.OpenMenu = true;
        m_OnPokemon.Raise();

    }

}
