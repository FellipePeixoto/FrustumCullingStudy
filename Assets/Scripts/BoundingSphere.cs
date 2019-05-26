using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoundingSphere : MonoBehaviour
{
    public bool show;
    public Vector3 position;
    public float radius;

    private void Update()
    {
        position = transform.position;
        radius = transform.localScale.x / 2;

        if (show)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            return;
        }

        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
