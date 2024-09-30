using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class PlayerCollisionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {

        float3 playerPosition = PlayerController.Instance.transform.position;
      

        Entities.WithAll<EnemyTag>().
            ForEach((Entity entity,
                    ref Translation translation) =>
        {
            if (math.distance(translation.Value, playerPosition) < PlayerController.Instance.coliborde)
            {

                if (PlayerController.Instance.invencible == false)
                {
                    PlayerController.Instance.dañar();
                }
            }

        });
    }
}
