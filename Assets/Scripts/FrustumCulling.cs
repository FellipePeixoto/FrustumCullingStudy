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

    public float FieldOfView { get => fieldOfView; }
    public float NearClipDistance { get => nearClipDistance; }
    public float FarClipDistance { get => farClipDistance; }
    public bool IsEnabled { get => isEnabled; }
    public PlaneStruct[] Planes { get => planes; }

    private void Update()
    {
        planes[0] = new PlaneStruct();
        planes[1] = new PlaneStruct();
        planes[2] = new PlaneStruct();
        planes[3] = new PlaneStruct();
        planes[4] = new PlaneStruct();
        planes[5] = new PlaneStruct();

        if (!isEnabled)
            return;

        planes[0].height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * nearClipDistance;
        planes[0].width = planes[0].height * ratio;

        planes[1].height = 2 * Mathf.Tan((Mathf.Deg2Rad * fieldOfView) / 2) * farClipDistance;
        planes[1].width = planes[1].height * ratio;

        planes[0].center = transform.position + transform.forward.normalized * nearClipDistance;
        planes[0].topLeft = planes[0].center + (transform.up * planes[0].height / 2) - (transform.right * planes[0].width / 2);
        planes[0].topRight = planes[0].center + (transform.up * planes[0].height / 2) + (transform.right * planes[0].width / 2);
        planes[0].bttRight = planes[0].center - (transform.up * planes[0].height / 2) + (transform.right * planes[0].width / 2);
        planes[0].bttLeft = planes[0].center - (transform.up * planes[0].height / 2) - (transform.right * planes[0].width / 2);

        planes[1].center = transform.position + transform.forward.normalized * farClipDistance;
        planes[1].topLeft = planes[1].center + (transform.up * planes[1].height / 2) - (transform.right * planes[1].width / 2);
        planes[1].topRight = planes[1].center + (transform.up * planes[1].height / 2) + (transform.right * planes[1].width / 2);
        planes[1].bttRight = planes[1].center - (transform.up * planes[1].height / 2) + (transform.right * planes[1].width / 2);
        planes[1].bttLeft = planes[1].center - (transform.up * planes[1].height / 2) - (transform.right * planes[1].width / 2);

        planes[2].topLeft = planes[1].topLeft;
        planes[2].topRight = planes[1].topRight;
        planes[2].bttRight = planes[0].topRight;
        planes[2].bttLeft = planes[0].topLeft;
        planes[2].CalcCenter();

        planes[3].topLeft = planes[1].topRight;
        planes[3].topRight = planes[1].bttRight;
        planes[3].bttRight = planes[0].bttRight;
        planes[3].bttLeft = planes[0].topRight;
        planes[3].CalcCenter();

        planes[4].topLeft = planes[1].bttLeft;
        planes[4].topRight = planes[1].bttRight;
        planes[4].bttRight = planes[0].bttRight;
        planes[4].bttLeft = planes[0].bttLeft;
        planes[4].CalcCenter();

        planes[5].topLeft = planes[1].bttLeft;
        planes[5].topRight = planes[1].topLeft;
        planes[5].bttRight = planes[0].topLeft;
        planes[5].bttLeft = planes[0].bttLeft;
        planes[5].CalcCenter();

        var bdnSphere = FindObjectsOfType<BoundingSphere>();

        for (int i = 0; i < 6; i++)
        {
            foreach (BoundingSphere b in bdnSphere)
            {
                if (i == 1 || i == 4)
                {
                    if (planes[i].DistanceNormalPositive(b.position) < -b.radius)
                    {
                        b.show = false;
                        continue;
                    }
                    else if (planes[i].DistanceNormalPositive(b.position) < b.radius)
                    {
                        b.show = true;
                        continue;
                    }

                    b.show = false;
                }
                else
                {
                    if (planes[i].DistanceNormalNegative(b.position) < -b.radius)
                    {
                        b.show = false;
                        continue;
                    }
                    else if (planes[i].DistanceNormalNegative(b.position) < b.radius)
                    {
                        b.show = true;
                        continue;
                    }

                    b.show = false;
                }
            }
        }
    }
}
