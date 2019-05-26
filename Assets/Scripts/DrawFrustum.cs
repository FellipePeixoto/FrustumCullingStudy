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