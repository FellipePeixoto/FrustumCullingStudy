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
    public Vector3 normal;

    public float Plane
    {
        get
        {
            return Vector3.Dot(-normal, center);
        }
    }

    public void CalcCenter()
    {
        center = (topLeft + topRight + bttRight + bttLeft) / 4;
    }

    public void CalcNormal()
    {
        Vector3 dir = Vector3.Cross(topRight - topLeft, bttRight - topRight);
        normal = dir.normalized;
    }

    public float SignedDistance(Vector3 point)
    {
        return Vector3.Dot(normal, point);
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

    /// <summary>
    /// 0 = near, 1 = far, 2 = top, 3 = right, 4 = bottom, 5 = left
    /// </summary>
    PlaneStruct[] planes = new PlaneStruct[6];

    public float FieldOfView { get => fieldOfView; }
    public float NearClipDistance { get => nearClipDistance; }
    public float FarClipDistance { get => farClipDistance; }
    public bool IsEnabled { get => isEnabled; }
    /// <summary>
    /// 0 = near, 1 = far, 2 = top, 3 = right, 4 = bottom, 5 = left
    /// </summary>
    public PlaneStruct[] Planes { get => planes; }

    private void Update()
    {
        if (!isEnabled)
            return;

        planes[0] = new PlaneStruct();
        planes[1] = new PlaneStruct();
        planes[2] = new PlaneStruct();
        planes[3] = new PlaneStruct();
        planes[4] = new PlaneStruct();
        planes[5] = new PlaneStruct();

        planes[0].height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * nearClipDistance;
        planes[0].width = planes[0].height * ratio;

        planes[1].height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * farClipDistance;
        planes[1].width = planes[1].height * ratio;

        //Near plane
        planes[0].center = transform.position + transform.forward.normalized * nearClipDistance;
        planes[0].topLeft = planes[0].center + (transform.up * planes[0].height / 2) - (transform.right * planes[0].width / 2);
        planes[0].topRight = planes[0].center + (transform.up * planes[0].height / 2) + (transform.right * planes[0].width / 2);
        planes[0].bttRight = planes[0].center - (transform.up * planes[0].height / 2) + (transform.right * planes[0].width / 2);
        planes[0].bttLeft = planes[0].center - (transform.up * planes[0].height / 2) - (transform.right * planes[0].width / 2);
        planes[0].CalcNormal();

        //Far plane
        planes[1].center = transform.position + transform.forward.normalized * farClipDistance;
        planes[1].topLeft = planes[1].center + (transform.up * planes[1].height / 2) + (transform.right * planes[1].width / 2);
        planes[1].topRight = planes[1].center + (transform.up * planes[1].height / 2) - (transform.right * planes[1].width / 2);
        planes[1].bttRight = planes[1].center - (transform.up * planes[1].height / 2) - (transform.right * planes[1].width / 2);
        planes[1].bttLeft = planes[1].center - (transform.up * planes[1].height / 2) + (transform.right * planes[1].width / 2);
        planes[1].CalcNormal();

        //Top plane
        planes[2].topLeft = planes[1].topRight;
        planes[2].topRight = planes[1].topLeft;
        planes[2].bttRight = planes[0].topRight;
        planes[2].bttLeft = planes[0].topLeft;
        planes[2].CalcCenter();
        planes[2].CalcNormal();

        //Right Plane
        planes[3].topLeft = planes[1].topLeft;
        planes[3].topRight = planes[1].bttLeft;
        planes[3].bttRight = planes[0].bttRight;
        planes[3].bttLeft = planes[0].topRight;
        planes[3].CalcCenter();
        planes[3].CalcNormal();

        //Bottom Plane
        planes[4].topRight = planes[1].bttRight;
        planes[4].topLeft = planes[1].bttLeft;
        planes[4].bttLeft = planes[0].bttRight;
        planes[4].bttRight = planes[0].bttLeft;
        planes[4].CalcCenter();
        planes[4].CalcNormal();

        //Left Plane
        planes[5].topLeft = planes[1].bttRight;
        planes[5].topRight = planes[1].topRight;
        planes[5].bttRight = planes[0].topLeft;
        planes[5].bttLeft = planes[0].bttLeft;
        planes[5].CalcCenter();
        planes[5].CalcNormal();

        var bdnSphere = FindObjectsOfType<BoundingSphere>();

        if (bdnSphere != null && bdnSphere.Length == 0)
            return;

        foreach (BoundingSphere b in bdnSphere)
        {
            bool[] vetVer = new bool[5] { true, true, true, true, true};

            if (b.position.z + b.radius < transform.position.z + nearClipDistance ||
                b.position.z - b.radius > transform.position.z + farClipDistance)
            {
                vetVer[0] = false;
            }

            for (int i = 2; i < 6; i++)
            {
                if (-planes[i].SignedDistance(b.position) < -b.radius)
                {
                    vetVer[i - 1] = false;
                }
            }

            bool showornot = true;

            foreach (bool vf in vetVer)
            {
                if (vf == false)
                {
                    showornot = false;
                    break;
                }
            }

            b.show = showornot;
        }

        Debug.Log("Near Clip: " + planes[0].SignedDistance(bdnSphere[0].position) * -1);
        Debug.Log("Far Clip: " + planes[1].SignedDistance(bdnSphere[0].position) * -1);
        Debug.Log("Top: " + planes[2].SignedDistance(bdnSphere[0].position) * -1);
        Debug.Log("Right: " + planes[3].SignedDistance(bdnSphere[0].position) * -1);
        Debug.Log("Bottom Plane: " + planes[4].SignedDistance(bdnSphere[0].position) * -1);
        Debug.Log("Left: " + planes[5].SignedDistance(bdnSphere[0].position) * -1);
    }
}