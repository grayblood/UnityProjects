using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasePokemon : ScriptableObject
{
    public Sprite sprite;
    public string name;
    public int m_level;
    public int m_ps;
    public int m_psMax;
    public int m_pp;
    public int m_ppMax;
    public int m_exp;
    public int m_atk;
    public int m_def;
    public float m_speed;
    public bool m_owner;
    public bool m_ally;
    public Tipo tipo;
    //[SerializeField]
    //ItemsScriptable item;
    public Attack[] m_attacks;
}

public enum Tipo{
    Fuego,
    Agua,
    Planta,
    Normal
}
