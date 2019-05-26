using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FrustumCulling : MonoBehaviour
{
    [SerializeField] [Range(1, 179)] float fieldOfView;
    [SerializeField] float ratio;
    [SerializeField] float nearClipDistance;
    [SerializeField] float farClipDistance;

    bool enabled;
    public class NearClipStruct
    {
        public float height;
        public float width;
        public Vector3 center;
        public Vector3 topLeft;
        public Vector3 topRight;
        public Vector3 bttRight;
        public Vector3 bttLeft;
    }
    NearClipStruct nearClip;

    public class FarClipStruct
    {
        public float height;
        public float width;
        public Vector3 center;
        public Vector3 topLeft;
        public Vector3 topRight;
        public Vector3 bttRight;
        public Vector3 bttLeft;
    }
    FarClipStruct farClip;

    public float FieldOfView { get => fieldOfView; }
    public float NearClipDistance { get => nearClipDistance; }
    public float FarClipDistance { get => farClipDistance; }
    public bool Enabled { get => enabled; }
    public NearClipStruct NearClip { get => nearClip; set => nearClip = value; }
    public FarClipStruct FarClip { get => farClip; set => farClip = value; }

    private void Update()
    {
        nearClip = new NearClipStruct();
        farClip = new FarClipStruct();

        nearClip.height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * nearClipDistance;
        nearClip.width = nearClip.height * ratio;

        farClip.height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * farClipDistance;
        farClip.width = farClip.height * ratio;

        nearClip.center = transform.position + transform.forward.normalized * nearClipDistance;
        nearClip.topLeft = nearClip.center + (transform.up * nearClip.height/2) - (transform.right * nearClip.width / 2);
        nearClip.topRight = nearClip.center + (transform.up * nearClip.height/2) + (transform.right * nearClip.width / 2);
        nearClip.bttRight = nearClip.center - (transform.up * nearClip.height/2) + (transform.right * nearClip.width / 2);
        nearClip.bttLeft = nearClip.center - (transform.up * nearClip.height/2) - (transform.right * nearClip.width / 2);

        farClip.center = transform.position + transform.forward.normalized * farClipDistance;
        farClip.topLeft = farClip.center + (transform.up * farClip.height / 2) - (transform.right * farClip.width / 2);
        farClip.topRight = farClip.center + (transform.up * farClip.height / 2) + (transform.right * farClip.width / 2);
        farClip.bttRight = farClip.center - (transform.up * farClip.height / 2) + (transform.right * farClip.width / 2);
        farClip.bttLeft = farClip.center - (transform.up * farClip.height / 2) - (transform.right * farClip.width / 2);
    }
}
