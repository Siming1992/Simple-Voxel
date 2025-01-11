using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace SimpleVoxed.LOD
{
    public class MeshChunkPool
    {
        private static MeshChunkPool _instance;

        public static MeshChunkPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MeshChunkPool();
                }
                return _instance;
            }
        }
        
        public List<MeshChunk> meshChunkPool = new List<MeshChunk>();
        
        const int maxCapacity = 1000;

        private GameObject meshChunkPrefab;
        
        public void Destroy(MeshChunk meshChunk)
        {
            if (meshChunkPool.Count > maxCapacity)
            {
                meshChunk.Dispose();
                return;
            }

            meshChunk.Clear();
            meshChunkPool.Add(meshChunk);
        }
        
        public MeshChunk GetChunk()
        {
            if (meshChunkPool.Count == 0)
            {
                if (meshChunkPrefab == null)
                {
                    meshChunkPrefab = (GameObject)Resources.Load("Prefabs/MeshChunk");
                }
                var meshChunkObject = GameObject.Instantiate(meshChunkPrefab);
                return meshChunkObject.GetComponent<MeshChunk>();
            }

            MeshChunk meshChunk = meshChunkPool[0];
            meshChunkPool.RemoveAtSwapBack(0);

            return meshChunk;
        }
    }
}