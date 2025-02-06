using Unity.Mathematics;
using UnityEngine;
using static SimpleVoxed.Common.GeometryUtility;
using UnityEngine.Rendering;

namespace SimpleVoxed.LOD
{
    public class LodOctree
    {
        private static class Profiling
        {
            private const string k_Name = nameof(LodOctree);
            public static readonly ProfilingSampler SetLODCenter = new ProfilingSampler($"{k_Name}.{nameof(SetLODCenter)}");
        }
        
        private static readonly float3[] NODE_OFFSETS =
        {
            new(-0.5f, -0.5f, -0.5f),
            new(-0.5f, -0.5f, 0.5f),
            new(-0.5f, 0.5f, -0.5f),
            new(-0.5f, 0.5f, 0.5f),
            new(0.5f, -0.5f, -0.5f),
            new(0.5f, -0.5f, 0.5f),
            new(0.5f, 0.5f, -0.5f),
            new(0.5f, 0.5f, 0.5f)
        };

        private byte chunkDepth; //chunkDepth 5 equals chunkResolution 32*32*32

        private byte maxDepth; //max depth (in chunks)

        private int maxRootSize;
        private Transform parent;
        private LodOctreeNode rootNode;
        private float voxelSize;

        public void Init(Transform parent, int maxRootSize, int chunkResolution)
        {
            this.maxRootSize = maxRootSize;
            this.parent = parent;
            rootNode = new LodOctreeNode(0, float3.zero, maxRootSize);

            chunkDepth = (byte)math.log2(chunkResolution);
            maxDepth = (byte)(math.log2(maxRootSize) - chunkDepth);

            voxelSize = maxRootSize / math.exp2(maxDepth + chunkDepth);
        }

        public void SetLODCenter(float3 center)
        {
            if (math.length(center) > maxRootSize) center = math.normalize(center) * maxRootSize;

            SetLODCenter(rootNode, center);
        }

        private void SetLODCenter(LodOctreeNode node, float3 lodCenter)
        {
            using (new ProfilingScope(Profiling.SetLODCenter))
            {
                if (node.depth == maxDepth || !IsWithinReach(lodCenter, node))  //只有在节点内部才会触发递归，否则设置为叶子结点（isLeaf == true）
                {
                    if (!node.isLeaf)
                    {
                        node.isLeaf = true;
                        node.isRecentLeaf = true;
                    }
                }
                else
                {
                    node.isLeaf = false;
                    var nodeSize = maxRootSize / math.exp2(node.depth + 1);

                    for (var i = 0; i < 8; i++)
                    {
                        if (node.childs[i] == null)
                            node.childs[i] = new LodOctreeNode((byte)(node.depth + 1), GetChildCenter(node.position, nodeSize, i), nodeSize);

                        SetLODCenter(node.childs[i], lodCenter);
                    }
                }
            }
        }

        void SetRecentLeafsToDirty(LodOctreeNode node)
        {
            if (node.isRecentLeaf)
            {
                node.isRecentLeaf = false;
                node.isDirty = true;
                
                return;
            }

            for (int i = 0; i < node.childs.Length; i++)
            {
                if (node.childs[i] != null)
                {
                    SetRecentLeafsToDirty(node);
                }
            }
        }

        MeshChunk CreateMeshChunk(float3 localPosition, float size)
        {
            MeshChunk meshChunk = MeshChunkPool.Instance.GetChunk();
            meshChunk.Init(parent, localPosition, size);

            return meshChunk;
        }
        
        private bool IsWithinReach(float3 targetPosition, LodOctreeNode node)
        {
            var nodeSize = maxRootSize / math.exp2(node.depth);
            var distanceToNode = GetDistanceToSurface(targetPosition, GetABB(node.position, nodeSize));
            return distanceToNode < nodeSize;
        }

        private float3 GetChildCenter(float3 parentCenter, float childSize, int childIndex)
        {
            return parentCenter + NODE_OFFSETS[childIndex] * childSize;
        }
    }
}