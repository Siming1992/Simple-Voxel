using Unity;
using UnityEngine;

namespace SimpleVoxed.Data
{
    [CreateAssetMenu(fileName = "VoxelRootConfig",menuName = "Voxel/VoxelRootConfig")]
    public class VoxelRootConfig:ScriptableObject
    {
        public int MaxRootSize;
        public int ChunkResolution;
        public int MaxDepth;
    }
}