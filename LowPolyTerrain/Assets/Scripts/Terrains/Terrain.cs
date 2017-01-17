using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Terrains
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(Rigidbody))]
    class Terrain : NetworkBehaviour
    {
        [SerializeField]
        private MeshFilter meshFilter;

        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private MeshCollider meshCollider;

        private List<SpawnPoints> spawnPoints;

        public List<SpawnPoints> SpawnPoints
        {
            get {
                if (spawnPoints == null) spawnPoints = GameObject.FindObjectsOfType<SpawnPoints>().ToList();

                return spawnPoints;
            }
            
        }
        private MeshFilter MeshFilter
        {
            get { return meshFilter; }
        }

        public Mesh Mesh
        {
            get
            {
                return meshFilter.mesh;
            }
            set
            {
                meshFilter.mesh = value;
            }
        }

        public IEnumerator RecalculateCollider()
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = MeshFilter.mesh;
            yield return null;
        }
        
    }
}
