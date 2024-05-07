using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{   

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    public int xSize = 20;
    public int zSize = 20;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
    }

    void Update(){
        UpdateMesh();
    }


    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= zSize; x++)
            {
                float[] octaveFrequencies = new float[] {1,1.5f,2,2.5f} ;
                float[] octaveAmplitudes = new float[] {1,0.9f,0.7f,0} ;
                float y=0;
                for(int j = 0; j < octaveFrequencies.Length; j++){
                    y += octaveAmplitudes[j]* Mathf.PerlinNoise(
                    octaveFrequencies[j] * x + .3f, 
                    octaveFrequencies[j] * z + .3f) * 2f ;
                }

                vertices[i] = new Vector3(x + 10, y, z + 10);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;
        triangles = new int[xSize * zSize * 6];

        for(int z = 0; z < zSize; z++)
        {
            for(int x = 0; x < xSize; x++)
            {
           
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + xSize + 1;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + xSize + 1;
            triangles[tris + 5] = vert + zSize + 2;

            vert++;
            tris +=6;

            }
            vert++;
        }
        

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }


}
