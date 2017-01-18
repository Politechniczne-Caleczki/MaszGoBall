using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts.GameEngine.Units;
using UnityEngine.Networking;

namespace Assets.Scripts.Terrains
{
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
        private Terrain terrainPrefab;

        [SerializeField]
        private GameObject borderPrefab;

        [SerializeField]
        private SpawnPoints spawnPointsPrefab;

        [SerializeField]
        private BottomCollider bottomColliderPrefab;

        [SerializeField]
        private Texture2D texture;

        private Terrain Terrain
        {
            get;set;
        }

        private Texture2D Texture
        {
            get { return texture; }

        }

        public IEnumerator GenerateTerrain(Action<float> onProgress)
        {
            Terrain = Instantiate(terrainPrefab);

            

            Terrain.Mesh = new Mesh();
            Terrain.Mesh.subMeshCount = 3;

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
                    if (i == 0 && NetworkServer.active)
                        AddTree((vertices[index - 3] + vertices[index - 4] + vertices[index-2]) / 3);


                    i = (int)((h1 + h3 + h4) / 3 / 0.333f);

                    if (i == 0 && NetworkServer.active)
                    {
                        AddEnemy((vertices[index - 3] + vertices[index - 4] + vertices[index - 2]) / 3);
                    }

                    Triangels[i].Add(index - 1);
                    Triangels[i].Add(index - 3);
                    Triangels[i].Add(index - 2);

                    onProgress(progress / (texture.width * texture.height));

                    ++progress;
                }
                yield return null;
            }


            Terrain.Mesh.vertices = vertices.ToArray();
            for (int x = 0; x < 3; ++x)
                Terrain.Mesh.SetTriangles(Triangels[x], x);

            Terrain.Mesh.RecalculateNormals();
            Terrain.Mesh.RecalculateBounds();

            TreeTranform.position = EnemyTranform.position = Terrain.transform.position -= new Vector3(texture.width / 2 * size, 0, texture.height / 2 * size);
            TreeTranform.localScale = EnemyTranform.localScale = Terrain.transform.localScale = new Vector3(size, 1, size);

            yield return Terrain.RecalculateCollider();
            
            yield return AddBorderCollider();
            yield return EnemyController.Enable();
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
                NetworkServer.Spawn(tree);
            }
        }

        private void AddEnemy(Vector3 center)
        {
            if (UnityEngine.Random.Range(0, 10) == 0)
            {

                int index = UnityEngine.Random.Range(0, 100) % Enemies.Count;

                Nation enemy =  Instantiate(Enemies[index], EnemyTranform);
                enemy.transform.position = center + new Vector3(UnityEngine.Random.Range(-size, size), 0.1f, UnityEngine.Random.Range(-size, size));
                enemy.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0);
                enemy.transform.localScale = new Vector3(.5f / size, .5f, .5f / size);
                enemy.gameObject.SetActive(false);
                NetworkServer.Spawn(enemy.gameObject);

                Terrain.SpawnPoints.Add(Instantiate(spawnPointsPrefab, center*size - new Vector3(texture.width / 2 * size, 0, texture.height / 2 * size), Quaternion.identity));
                NetworkServer.Spawn(Terrain.SpawnPoints[Terrain.SpawnPoints.Count - 1].gameObject);

                EnemyController.Add(enemy);
            }
        }

        private IEnumerator AddBorderCollider()
        {
            GameObject b1 = Instantiate(borderPrefab);
            b1.transform.position = Terrain.transform.position + new Vector3(0, 0, size * Texture.height / 2);
            b1.layer = 14;
            BoxCollider coll1 = b1.GetComponent<BoxCollider>();
            
            coll1.size = new Vector3(1, 10, size * Texture.height);
           // NetworkServer.Spawn(b1);
            yield return null;

            GameObject b2 = Instantiate(borderPrefab);
            b2.transform.position = Terrain.transform.position + new Vector3(size * texture.width, 0, size * Texture.height / 2);
            b2.layer = 14;
            BoxCollider coll2 = b2.GetComponent<BoxCollider>();
            coll2.size = new Vector3(1, 10, size * Texture.height);
           // NetworkServer.Spawn(b2);

            yield return null;


            GameObject b3 = Instantiate(borderPrefab);
            b3.transform.position = Terrain.transform.position + new Vector3(size * texture.width/2, 0, size * Texture.height);
            b3.layer = 14;
            BoxCollider coll3 = b3.GetComponent<BoxCollider>();
            coll3.size = new Vector3(size * Texture.width, 10, 1);
           // NetworkServer.Spawn(b3);

            yield return null;

            GameObject b4 = Instantiate(borderPrefab);
            b4.transform.position = Terrain.transform.position + new Vector3(size * texture.width / 2, 0,0);
            b4.layer = 14;
            BoxCollider coll4 = b4.GetComponent<BoxCollider>();
            coll4.size = new Vector3(size * Texture.width, 10, 1);

            // NetworkServer.Spawn(b4);

            BottomCollider bC = Instantiate(bottomColliderPrefab);
            bC.transform.position = transform.position - new Vector3(0, 0.5f, 0);
            bC.gameObject.layer = 14;
            BoxCollider collbC = bC.GetComponent<BoxCollider>();
            collbC.size = new Vector3(size * Texture.width, .4f, size*Texture.height);

            yield return null;


        }
        
        public void CreateLight()
        {
            GameObject oldLight = GameObject.Find("Directional Light");
            GameObject.Destroy(oldLight);

            GameObject newlight = new GameObject("Directional Light");
            newlight.transform.eulerAngles = new Vector3(70, 10, 0);
            Light light = newlight.AddComponent<Light>();
            light.type = LightType.Directional;
        }
    }
}
