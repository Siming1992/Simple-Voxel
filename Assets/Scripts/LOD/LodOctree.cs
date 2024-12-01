

using Unity.Mathematics;

namespace SimpleVoxed.LOD
{
    public class LodOctree
    {
        static readonly float3[] NODE_OFFSETS =
        {
            new float3(-0.5f,-0.5f,-0.5f),
            new float3(-0.5f, -0.5f, 0.5f),
            new float3(-0.5f, 0.5f, -0.5f),
            new float3(-0.5f, 0.5f, 0.5f),
            new float3(0.5f, -0.5f, -0.5f),
            new float3(0.5f, -0.5f, 0.5f),
            new float3(0.5f, 0.5f, -0.5f),
            new float3(0.5f, 0.5f, 0.5f)
        };

        class MeshNode
        {
            public byte depth;
            public float3 position;
            public MeshNode[] childs;
            
        }

        float3 GetChildCenter(float3 parentCenter,float childSize,int childIndex)
        {
            return parentCenter + NODE_OFFSETS[childIndex] * childSize;
        }
    }
}