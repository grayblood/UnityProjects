using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class BulletPlayerSpawnSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float3 playerPosition = PlayerController.Instance.transform.position;
        float deltaTime = Time.DeltaTime;

        Entities.WithAll<PlayerSpawnerBullet>().ForEach((ref Translation translation, ref PlayerSpawnerBullet spawner) => {
            spawner.elapsedTime += deltaTime;
            if (spawner.elapsedTime >= spawner.cooldown)
            {
                spawner.elapsedTime -= spawner.cooldown;
                if (PlayerController.Instance.shooting) { 
                    Entity entity = EntityManager.Instantiate(spawner.prefab);
                    float3 posicionSpawn = new float3(playerPosition.x, playerPosition.y, 0);
                    EntityManager.SetComponentData(entity, new Translation { Value = posicionSpawn });
                }
            }
        });
    }
}
