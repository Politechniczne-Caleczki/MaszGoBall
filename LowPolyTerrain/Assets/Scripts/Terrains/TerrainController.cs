using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts.GameEngine.Units;

namespace Assets.Scripts.Terrains
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TerrainController : MonoBehaviour
    {
        private float height = .75f;

        private float size = .25f;

        [SerializeField]
        Transform TreeTranform;

        [SerializeField]
        Transform EnemyTranform;

        [SerializeField]
        List<GameObject> Trees;

        [SerializeField]
        List<Nation> Enemies; 

        [SerializeField]
        private MeshFilter meshFilter;

        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private MeshCollider meshCollider;


        [SerializeField]
        private Texture2D texture;
        private Mesh Mesh
        {
            get { return MeshFilter.mesh; }
            set { MeshFilter.mesh = value; }
        }
        private Texture2D Texture
        {
            get { return texture; }

        }
        private MeshFilter MeshFilter
        {
            get { return meshFilter; }
        }
        public IEnumerator GenerateTerrain(Action<float> onProgress)
        {
            Mesh = new Mesh();
            Mesh.subMeshCount = 3;

            List<Vector3> vertices = new List<Vector3>();
            List<int>[] Triangels = new List<int>[4];
            for (int x = 0; x < 4; ++x)
                Triangels[x] = new List<int>();

            int index = 0;
            float progress = 0;

            for (int x = 0; x < Texture.width - 1; ++x)
            {
                for (int z = 0; z < Texture.height - 1; ++z)
                {
                    float h1 = Texture.GetPixel(x, z).grayscale;
                    float h2 = Texture.GetPixel(x + 1, z).grayscale;
                    float h3 = Texture.GetPixel(x, z + 1).grayscale;
                    float h4 = Texture.GetPixel(x + 1, z + 1).grayscale;

                    vertices.Add(new Vector3(x, h1 * height, z));
                    vertices.Add(new Vector3(x + 1, h2 * height, z));
                    vertices.Add(new Vector3(x, h3 * height, z + 1));
                    vertices.Add(new Vector3(x + 1, h4 * height, z + 1));

                    index += 4;

                    int i = (int)((h1 + h2 + h3) / 3 / 0.333f);

                    Triangels[i].Add(index - 3);
                    Triangels[i].Add(index - 4);
                    Triangels[i].Add(index - 2);
                    if (i == 0)
                        AddTree((vertices[index - 3] + vertices[index - 4] + vertices[index-2]) / 3);


                    i = (int)((h2 + h3 + h4) / 3 / 0.333f);

                    if (i == 0)
                        AddEnemy((vertices[index - 3] + vertices[index - 4] + vertices[index - 2]) / 3);

                    Triangels[i].Add(index - 1);
                    Triangels[i].Add(index - 3);
                    Triangels[i].Add(index - 2);

                    onProgress(progress / (texture.width * texture.height));

                    ++progress;
                }
                yield return null;
            }


            Mesh.vertices = vertices.ToArray();
            for (int x = 0; x < 3; ++x)
                Mesh.SetTriangles(Triangels[x], x);

            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();

            TreeTranform.position = EnemyTranform.position = transform.position -= new Vector3(texture.width / 2 * size, 0, texture.height / 2 * size);
            TreeTranform.localScale = EnemyTranform.localScale = transform.localScale = new Vector3(size, 1, size);

            RecalculateCollider();
            EnemyController.Enable();

        }
        public void RecalculateCollider()
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = MeshFilter.mesh;
        }
        private void AddTree(Vector3 center)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                GameObject tree = Instantiate(Trees[UnityEngine.Random.Range(0, 2)], TreeTranform);
                tree.transform.position = center + new Vector3(UnityEngine.Random.Range(-size, size), .12f, UnityEngine.Random.Range(-size, size));
                tree.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0);
                tree.transform.localScale = new Vector3(1 / size, 1, 1 / size);
                tree.isStatic = true;
                tree.layer = 14;
            }
        }
        private void AddEnemy(Vector3 center)
        {
            if (UnityEngine.Random.Range(0, 10) == 0)
            {

                int index = UnityEngine.Random.Range(0, 100) % Enemies.Count;

                Nation enemy = Instantiate(Enemies[index], EnemyTranform);
                enemy.transform.position = center + new Vector3(UnityEngine.Random.Range(-size, size), 1.0f, UnityEngine.Random.Range(-size, size));
                enemy.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0);
                enemy.transform.localScale = new Vector3(.5f / size, .5f, .5f / size);
                enemy.gameObject.SetActive(false);

                EnemyController.Add(enemy);
            }
        }
    }
}
