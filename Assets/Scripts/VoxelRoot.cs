using System.Collections.Generic;
using SimpleVoxed.Common;
using SimpleVoxed.Data;
using SimpleVoxed.LOD;
using Unity.Mathematics;
using UnityEngine;

namespace SimpleVoxed
{
    public class VoxelRoot : MonoBehaviour
    {
        [SerializeField] Transform cameraTransform;

        public VoxelRootConfig rootConfig;
        LodOctree lodOctree;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Assert(Utility.IsPowerOfTwo(rootConfig.ChunkResolution));
            Debug.Assert(Utility.IsPowerOfTwo(rootConfig.MaxRootSize));

            lodOctree = new LodOctree();
            lodOctree.Init(transform, rootConfig.MaxRootSize, rootConfig.ChunkResolution);

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 playerPositionLocalToPlanet = transform.InverseTransformPoint(cameraTransform.position);
            lodOctree.SetLODCenter(playerPositionLocalToPlanet);
        }
    }
}
