using System.Collections.Generic;
using SimpleVoxed.Data;
using UnityEngine;

namespace SimpleVoxed
{
    public class VoxelRoot : MonoBehaviour
    {
        public static List<VoxelRoot> voxelRoots = new List<VoxelRoot>();

        [SerializeField] Transform cameraTransform;

        public VoxelRootConfig rootConfig;
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            voxelRoots.Add(this);
            
            Debug.Assert(Utility.IsPowerOfTwo(rootConfig.ChunkResolution));
            Debug.Assert(Utility.IsPowerOfTwo(rootConfig.MaxRootSize));
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
