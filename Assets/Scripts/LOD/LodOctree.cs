

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
            //structure data
            public byte depth;
            public float3 position;
            public MeshNode[] childs;
            
            //node data
            public MeshChunk meshChunk;

            public MeshNode(byte depth, float3 position)
            {
                this.depth = depth;
                this.position = position;
                childs = new MeshNode[8];

                meshChunk = null;
            }
        }

        MeshNode rootNode;

        private float maxRootSize;
        private float voxelSize;

        private byte maxDepth;      //max depth (in chunks)
        private byte chunkDepth;    //chunkDepth 5 equals chunkResolution 32*32*32
        
        public void Init(float maxRootSize,byte maxDepth,byte chunkDepth)
        {
            rootNode = new MeshNode(0, float3.zero);
            this.maxRootSize = this.maxRootSize;

            this.maxDepth = maxDepth;
            this.chunkDepth = chunkDepth;

            voxelSize = maxRootSize / math.exp2(maxDepth + chunkDepth);
        }

        float3 GetChildCenter(float3 parentCenter,float childSize,int childIndex)
        {
            return parentCenter + NODE_OFFSETS[childIndex] * childSize;
        }
    }
}