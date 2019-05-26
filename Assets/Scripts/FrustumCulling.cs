using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStruct
{
    public float height;
    public float width;
    public Vector3 center;
    public Vector3 topLeft;
    public Vector3 topRight;
    public Vector3 bttRight;
    public Vector3 bttLeft;
    public Vector3 Normal
    {
        get
        {
            Vector3 dir = Vector3.Cross(topRight - topLeft, bttRight - topRight);
            return dir.normalized;
        }
    }
    public float Plane
    {
        get
        {
            return Vector3.Dot(-Normal, center);
        }
    }
    
    public void CalcCenter()
    {
        center = (topLeft + topRight + bttRight + bttLeft) / 4;
    }

    public float DistanceNormalNegative(Vector3 point)
    {
        return Vector3.Dot(-Normal, point);
    }

    public float DistanceNormalPositive(Vector3 point)
    {
        return Vector3.Dot(Normal, point);
    }
}

[ExecuteInEditMode]
public class FrustumCulling : MonoBehaviour
{
    [SerializeField] [Range(1, 179)] float fieldOfView;
    [SerializeField] float ratio;
    [SerializeField] float nearClipDistance;
    [SerializeField] float farClipDistance;
    [SerializeField] bool isEnabled;

    PlaneStruct[] planes = new PlaneStruct[6];
    PlaneStruct nearClip;
    PlaneStruct farClip;
    PlaneStruct top;
    PlaneStruct right;
    PlaneStruct bottom;
    PlaneStruct left;

    public float FieldOfView { get => fieldOfView; }
    public float NearClipDistance { get => nearClipDistance; }
    public float FarClipDistance { get => farClipDistance; }
    public bool IsEnabled { get => isEnabled; }
    public PlaneStruct NearClip { get => nearClip; }
    public PlaneStruct FarClip { get => farClip; }
    public PlaneStruct Top { get => top; }
    public PlaneStruct Right { get => right; }
    public PlaneStruct Bottom { get => bottom; }
    public PlaneStruct Left { get => left; }

    private void Update()
    {
        nearClip = new PlaneStruct();
        farClip = new PlaneStruct();
        top = new PlaneStruct();
        right = new PlaneStruct();
        bottom = new PlaneStruct();
        left = new PlaneStruct();

        if (!isEnabled)
            return;

        nearClip.height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * nearClipDistance;
        nearClip.width = nearClip.height * ratio;

        farClip.height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * farClipDistance;
        farClip.width = farClip.height * ratio;

        nearClip.center = transform.position + transform.forward.normalized * nearClipDistance;
        nearClip.topLeft = nearClip.center + (transform.up * nearClip.height / 2) - (transform.right * nearClip.width / 2);
        nearClip.topRight = nearClip.center + (transform.up * nearClip.height / 2) + (transform.right * nearClip.width / 2);
        nearClip.bttRight = nearClip.center - (transform.up * nearClip.height / 2) + (transform.right * nearClip.width / 2);
        nearClip.bttLeft = nearClip.center - (transform.up * nearClip.height / 2) - (transform.right * nearClip.width / 2);

        farClip.center = transform.position + transform.forward.normalized * farClipDistance;
        farClip.topLeft = farClip.center + (transform.up * farClip.height / 2) - (transform.right * farClip.width / 2);
        farClip.topRight = farClip.center + (transform.up * farClip.height / 2) + (transform.right * farClip.width / 2);
        farClip.bttRight = farClip.center - (transform.up * farClip.height / 2) + (transform.right * farClip.width / 2);
        farClip.bttLeft = farClip.center - (transform.up * farClip.height / 2) - (transform.right * farClip.width / 2);

        top.topLeft = farClip.topLeft;
        top.topRight = farClip.topRight;
        top.bttRight = nearClip.topRight;
        top.bttLeft = nearClip.topLeft;
        top.CalcCenter();

        right.topLeft = farClip.topRight;
        right.topRight = farClip.bttRight;
        right.bttRight = nearClip.bttRight;
        right.bttLeft = nearClip.topRight;
        right.CalcCenter();

        bottom.topLeft = farClip.bttLeft;
        bottom.topRight = farClip.bttRight;
        bottom.bttRight = nearClip.bttRight;
        bottom.bttLeft = nearClip.bttLeft;
        bottom.CalcCenter();

        left.topLeft = farClip.bttLeft;
        left.topRight = farClip.topLeft;
        left.bttRight = nearClip.topLeft;
        left.bttLeft = nearClip.bttLeft;
        left.CalcCenter();

        var bdnSphere = FindObjectsOfType<BoundingSphere>();

        foreach(BoundingSphere b in bdnSphere)
        {
        }
    }
}
