using Unity.Mathematics;
using UnityEngine;

namespace SimpleVoxed.LOD
{
    public class LodOctreeNode
    {
        public LodOctreeNode[] childs;

        //structure data
        public byte depth;

        public bool isRecentLeaf;
        public bool isDirty;
        public bool isLeaf;
        public MeshChunk meshChunk;

        //node data
        public float3 position;
        public float size;

        public LodOctreeNode(byte depth, float3 position, float nodeSize)
        {
            this.depth = depth;
            this.position = position;
            size = nodeSize;
            childs = new LodOctreeNode[8];

            isRecentLeaf = false;
            isDirty = false;
            isLeaf = false;

            meshChunk = null;
            // meshChunk = CreateMeshChunk(position, nodeSize);
        }
    }
}