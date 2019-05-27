using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawFrustum : MonoBehaviour
{
    [SerializeField] FrustumCulling frustum;
    [SerializeField] LineRenderer nearClip;
    [SerializeField] LineRenderer farClip;
    [SerializeField] LineRenderer top;
    [SerializeField] LineRenderer right;
    [SerializeField] LineRenderer bottom;
    [SerializeField] LineRenderer left;
    [SerializeField] float lineWidth;

    private void Update()
    {
        if (frustum == null)
            return;

        if (!frustum.IsEnabled)
            return;

        nearClip.enabled = true;
        farClip.enabled = true;

        DrawPlane(nearClip, frustum.Planes[0]);
        DrawPlane(farClip, frustum.Planes[1]);
        DrawPlane(top, frustum.Planes[2]);
        DrawPlane(right, frustum.Planes[3]);
        DrawPlane(bottom, frustum.Planes[4]);
        DrawPlane(left, frustum.Planes[5]);

        Debug.DrawLine(frustum.Planes[3].center, frustum.Planes[3].center + (-frustum.Planes[3].normal), Color.red);
        Debug.DrawLine(frustum.Planes[5].center, frustum.Planes[5].center + (-frustum.Planes[5].normal), Color.red);
        Debug.DrawLine(frustum.Planes[2].center, frustum.Planes[2].center + (-frustum.Planes[2].normal), Color.red);
        Debug.DrawLine(frustum.Planes[0].center, frustum.Planes[0].center + (-frustum.Planes[0].normal), Color.red);
        Debug.DrawLine(frustum.Planes[4].center, frustum.Planes[4].center + (-frustum.Planes[4].normal), Color.red);
        Debug.DrawLine(frustum.Planes[1].center, frustum.Planes[1].center + (-frustum.Planes[1].normal), Color.red);
    }

    public void DrawPlane(LineRenderer lineRenderer, PlaneStruct planeStr)
    {
        if (nearClip == null)
            return;

        Vector3 center = transform.forward;

        lineRenderer.positionCount = 4;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = Resources.Load<Material>("Materials/NearClip");
        lineRenderer.loop = true;

        lineRenderer.SetPositions(
            new Vector3[]
            {
                planeStr.topLeft,
                planeStr.topRight,
                planeStr.bttRight,
                planeStr.bttLeft
            }
            );
    }
}