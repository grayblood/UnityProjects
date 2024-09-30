using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class EnemyCollsionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float3 EnemyPosition = EnemyController.Instance.transform.position;



        Entities.WithAll<BulletTag>().
             ForEach((Entity entity,
                     ref Translation translation) => {
                        
                        if (math.distance(translation.Value, EnemyPosition) < EnemyController.Instance.coliborde)
                        {

                            if (EnemyController.Instance.invencible == false)
                            {
                                 EnemyController.Instance.damage();
                            }
                        }

                    });
    }
}
