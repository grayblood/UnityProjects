using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public class BombEffect : ComponentSystem
{
    protected override void OnUpdate()
    {

        float3 playerPosition = PlayerController.Instance.transform.position;
        float deltaTime = Time.DeltaTime;
        if (PlayerController.Instance.bombactiva == true)
        {
            Entities.WithAll<EnemyTag>().
                ForEach((Entity entity,
                        ref Translation translation) =>
                {
                    PostUpdateCommands.DestroyEntity(entity);

                });
        }
        PlayerController.Instance.bombactiva = false;
    }
}
