using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

[GenerateAuthoringComponent] //monobehaviour
public struct RandomMovement : IComponentData
{
    public float2 direction;
    public float speed;

}
