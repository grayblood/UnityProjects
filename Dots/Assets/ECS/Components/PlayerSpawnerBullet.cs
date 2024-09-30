using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
[GenerateAuthoringComponent]
public struct PlayerSpawnerBullet : IComponentData
{
    public Entity prefab;
    public float cooldown;
    public float elapsedTime;
    public Vector2 direction;
}
