using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoundingSphere : MonoBehaviour
{
    [HideInInspector] public bool show;

    private void Update()
    {
        if (show)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            return;
        }

        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
