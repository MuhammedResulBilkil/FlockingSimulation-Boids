using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

    public class BoidsBoxSizeAuthoring : MonoBehaviour
    {
        public float3 boxSize;
    }
    
    public class BoidsBoxSizeBaker : Baker<BoidsBoxSizeAuthoring>
    {
        public override void Bake(BoidsBoxSizeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new BoidsBoxSizeComponent
            {
                boxSize = authoring.boxSize
            });
        }
    }