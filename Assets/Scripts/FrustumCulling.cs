using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    [SerializeField] float nearClipDistance;
    [SerializeField] float farClipDistance;
    public bool enabled;
    public float NearClipDistance { get { return nearClipDistance; }  }
    public float FarClipDistance { get { return farClipDistance; }  }
}
