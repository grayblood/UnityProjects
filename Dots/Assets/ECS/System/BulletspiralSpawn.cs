using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class BulletspiralSpawn : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.WithAll<BulletSpawnerspiral>().ForEach((ref Translation translation, ref BulletSpawnerspiral spawner) => {
            spawner.elapsedTime += deltaTime;
            if(spawner.elapsedTime >= spawner.cooldown)
            {
                spawner.elapsedTime -= spawner.cooldown;
                for(int i=0; i<spawner.amount; i++)
                {
                    Entity entity = EntityManager.Instantiate(spawner.prefab);
                    float3 posicionSpawn = new float3(translation.Value.x + i, translation.Value.y + i, 0);
                    EntityManager.SetComponentData(entity, new Translation { Value = posicionSpawn });

                    Entity entity2 = EntityManager.Instantiate(spawner.prefab);
                    float3 posicionSpawnne = new float3((translation.Value.x + i) * -1, translation.Value.y + i, 0);
                    EntityManager.SetComponentData(entity2, new Translation { Value = posicionSpawnne });
                }
            }
        });
    }
}
