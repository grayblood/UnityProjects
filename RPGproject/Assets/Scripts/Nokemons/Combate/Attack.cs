using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Attack : ScriptableObject
{
    public int m_damage;
    public int m_pp;
    public Tipo m_type;
    public float m_cooldown;
    public GameObject m_sprite;
}
