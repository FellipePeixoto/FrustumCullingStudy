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

        DrawPlane(nearClip, frustum.NearClip);
        DrawPlane(farClip, frustum.FarClip);
        DrawPlane(top, frustum.Top);
        DrawPlane(right, frustum.Right);
        DrawPlane(bottom, frustum.Bottom);
        DrawPlane(left, frustum.Left);

        Debug.DrawLine(frustum.Right.center, frustum.Right.center + (-frustum.Right.Normal));
        Debug.DrawLine(frustum.Left.center, frustum.Left.center + (-frustum.Left.Normal));
        Debug.DrawLine(frustum.Top.center, frustum.Top.center + (-frustum.Top.Normal));
        Debug.DrawLine(frustum.Bottom.center, frustum.Bottom.center + (frustum.Bottom.Normal));
        Debug.DrawLine(frustum.NearClip.center, frustum.NearClip.center + (-frustum.NearClip.Normal));
        Debug.DrawLine(frustum.FarClip.center, frustum.FarClip.center + (frustum.FarClip.Normal));
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