using Unity.Mathematics;
using UnityEngine;
using static SimpleVoxed.Common.GeometryUtility;

namespace SimpleVoxed.LOD
{
    public class LodOctree
    {
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
            rootNode = new LodOctreeNode(parent, 0, float3.zero, maxRootSize);

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
            var nodeSize = maxRootSize / math.exp2(node.depth);

            if (node.depth == maxDepth || !IsWithinReach(lodCenter, node))
            {
                if (!node.isLeaf) node.isLeaf = true;
            }
            else
            {
                node.isLeaf = false;

                for (var i = 0; i < 8; i++)
                {
                    if (node.childs[i] == null)
                        node.childs[i] = new LodOctreeNode(parent, (byte)(node.depth + 1), GetChildCenter(node.position, nodeSize * 0.5f, i), nodeSize * 0.5f);

                    SetLODCenter(node.childs[i], lodCenter);
                }
            }
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