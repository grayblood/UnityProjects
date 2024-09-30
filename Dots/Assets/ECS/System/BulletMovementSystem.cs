using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BulletMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.WithAll<BulletMovement>().ForEach
        (
            (
                ref Translation translation, 
                in BulletMovement bulletMovement
            ) =>{
                translation.Value.x += deltaTime* bulletMovement.speed* bulletMovement.direction.x;
                translation.Value.y += deltaTime * bulletMovement.speed * bulletMovement.direction.y;
            }
        ).ScheduleParallel();
    }
}
