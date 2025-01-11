using Unity.Mathematics;
using UnityEngine;

namespace SimpleVoxed.LOD
{
    public class LodOctreeNode
    {
        public LodOctreeNode[] childs;

        //structure data
        public byte depth;
        public bool isLeaf;
        public MeshChunk meshChunk;

        //node data
        private readonly Transform parent;
        public float3 position;
        public float size;

        public LodOctreeNode(Transform parent, byte depth, float3 position, float nodeSize)
        {
            this.parent = parent;

            this.depth = depth;
            this.position = position;
            size = nodeSize;
            childs = new LodOctreeNode[8];

            isLeaf = false;
        }

        private MeshChunk CreateMeshChunk(float3 localPosition, float size)
        {
            meshChunk = MeshChunkPool.Instance.GetChunk();
            meshChunk.Init(parent, localPosition, size);
            return meshChunk;
        }
    }
}