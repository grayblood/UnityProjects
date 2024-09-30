using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
[GenerateAuthoringComponent]
public struct BulletSpawner : IComponentData
{
    public Entity prefab;
    public int amount;
    public float cooldown;
    public float elapsedTime;
    public Vector2 direction;
}
