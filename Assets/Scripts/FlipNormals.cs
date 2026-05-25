using UnityEngine;
using System.Collections;

public class FlipNormals : MonoBehaviour
{
    public GameObject objectToFlip;

    void Awake()
    {
        InvertSphere();
    }

    void InvertSphere()
    {
        Vector3[] normals = objectToFlip.GetComponent<MeshFilter>().mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        objectToFlip.GetComponent<MeshFilter>().sharedMesh.normals = normals;

        int[] triangles = objectToFlip.GetComponent<MeshFilter>().sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }

        objectToFlip.GetComponent<MeshFilter>().sharedMesh.triangles = triangles;
    }
}