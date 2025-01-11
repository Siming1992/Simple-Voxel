using System;
using UnityEngine;
using Unity.Mathematics;

namespace SimpleVoxed.LOD
{
    public class MeshChunk : MonoBehaviour
    {
        float3 chunkSize;

        public void Init(Transform parent,float3 localPosition,float3 chunkSize)
        {
            transform.parent = parent;
            transform.name = localPosition.ToString() + chunkSize;
            transform.localPosition = localPosition;
            this.chunkSize = chunkSize;
        }

        public void Clear()
        {
            //mesh.Clear();
            //transform.parent = null;
            // meshCollider.sharedMesh = null;
            // meshCollider.enabled = false;

            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            // Destroy(mesh);
            Destroy(this);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position,chunkSize);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position,chunkSize);
        }
    }
}