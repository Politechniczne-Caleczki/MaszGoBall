using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts.Terrains
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TerrainController : MonoBehaviour
    {
        private float size = 15;

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
        public IEnumerator GenerateMesh()
        {
            Mesh = new Mesh();
            Mesh.subMeshCount = 3;

            

            List<Vector3> vertices = new List<Vector3>();
            List<int>[] Triangels = new List<int>[4];
            for (int x = 0; x < 4; ++x)
                Triangels[x] = new List<int>();

            int index = 0;

            for (int x = 0; x < Texture.width - 1; ++x)
            {
                for (int z = 0; z < Texture.height - 1; ++z)
                {
                    float h1 = Texture.GetPixel(x, z).grayscale;
                    float h2 = Texture.GetPixel(x + 1, z).grayscale;
                    float h3 = Texture.GetPixel(x, z + 1).grayscale;
                    float h4 = Texture.GetPixel(x + 1, z + 1).grayscale;

                    vertices.Add(new Vector3(x, h1 * size, z));
                    vertices.Add(new Vector3(x + 1, h2 * size, z));
                    vertices.Add(new Vector3(x, h3 * size, z + 1));
                    vertices.Add(new Vector3(x + 1, h4 * size, z + 1));

                    index += 4;

                    int i = (int)((h1 + h2 + h3) / 3 / 0.333f);

                    Triangels[i].Add(index - 3);
                    Triangels[i].Add(index - 4);
                    Triangels[i].Add(index - 2);

                    i = (int)((h2 + h3 + h4) / 3 / 0.333f);

                    Triangels[i].Add(index - 1);
                    Triangels[i].Add(index - 3);
                    Triangels[i].Add(index - 2);
                }
                yield return null;
            }


            Mesh.vertices = vertices.ToArray();
            for (int x = 0; x < 3; ++x)
                Mesh.SetTriangles(Triangels[x], x);
            Mesh.RecalculateBounds();
            Mesh.RecalculateNormals();
            ;
        }
        public void RecalculateCollider()
        {
            Mesh.RecalculateBounds();
            meshCollider.sharedMesh = Mesh;

        }
        public void GenerateTrees()
        {

        }
    }
}
