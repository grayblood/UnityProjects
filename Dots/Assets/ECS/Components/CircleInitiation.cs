using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class CircleInitiation : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
      
        float3 flo = new float3(UnityEngine.Random.Range(-10f,10f), UnityEngine.Random.Range(1f,-1f), 0); 
        dstManager.SetComponentData(entity, new Unity.Transforms.Translation { Value = flo });

        Vector2 randomdirection = new Vector2(UnityEngine.Random.Range(0.5f, -0.5f), -1f);
        dstManager.SetComponentData(entity, new RandomMovement { direction = randomdirection.normalized,speed = 3 });
        
    }
}
