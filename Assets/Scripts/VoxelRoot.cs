using System.Collections.Generic;
using UnityEngine;

namespace SimpleVoxed
{
    public class VoxelRoot : MonoBehaviour
    {
        public static List<VoxelRoot> voxelRoots = new List<VoxelRoot>();

        [SerializeField] Transform cameraTransform;
        [SerializeField] int maxRootSize = 4096;
        [SerializeField] int chunkResolution = 32;
        [SerializeField, Range(0, 15)] private byte maxDepth = 12; 
    
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
