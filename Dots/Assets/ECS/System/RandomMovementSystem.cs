using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RandomMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.WithAll<RandomMovement>().ForEach
        (
            (
                ref Translation translation,
                in RandomMovement randomMovement
            ) =>{
                translation.Value.x += deltaTime*randomMovement.speed* randomMovement.direction.x;
                translation.Value.y += deltaTime * randomMovement.speed * randomMovement.direction.y;
            }
        ).ScheduleParallel();
    }
}
