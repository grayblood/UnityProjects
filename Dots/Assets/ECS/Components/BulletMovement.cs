using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct BulletMovement : IComponentData
{
    public float2 direction;
    public float speed;
    public float Dmg;
}
