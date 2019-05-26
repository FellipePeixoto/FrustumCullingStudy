using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawFrustum : MonoBehaviour
{
    [SerializeField] FrustumCulling frustum;
    [SerializeField] LineRenderer nearClip;
    [SerializeField] LineRenderer farClip;
    [SerializeField] float lineWidth;

    private void Update()
    {
        if (frustum == null)
            return;

        if (!frustum.enabled)
        {
            nearClip.enabled = false;
            farClip.enabled = false;
            return;
        }

        nearClip.enabled = true;
        farClip.enabled = true;

        DrawNearClip();
        DrawFarClip();
    }

    public void DrawNearClip()
    {
        if (nearClip == null)
            return;

        nearClip.positionCount = 4;
        nearClip.startWidth = lineWidth;
        nearClip.endWidth = lineWidth;
        nearClip.material = Resources.Load<Material>("Materials/NearClip");
        nearClip.loop = true;

        nearClip.SetPositions(
            new Vector3[]
            {
                Vector3.up + Vector3.left +
                    transform.position + transform.forward * frustum.NearClipDistance,
                Vector3.up + Vector3.right +
                    transform.position + transform.forward * frustum.NearClipDistance,
                Vector3.down + Vector3.right +               
                    transform.position + transform.forward * frustum.NearClipDistance,
                Vector3.down + Vector3.left +                
                    transform.position + transform.forward * frustum.NearClipDistance
            }
            );
    }

    public void DrawFarClip()
    {
        if (farClip == null)
            return;

        farClip.positionCount = 4;
        farClip.startWidth = lineWidth;
        farClip.endWidth = lineWidth;
        farClip.material = Resources.Load<Material>("Materials/NearClip");
        farClip.loop = true;
        
        farClip.SetPositions(
            new Vector3[]
            {
                Vector3.up + Vector3.left +
                    transform.position + transform.forward * frustum.FarClipDistance,
                Vector3.up + Vector3.right +                 
                    transform.position + transform.forward * frustum.FarClipDistance,
                Vector3.down + Vector3.right +               
                    transform.position + transform.forward * frustum.FarClipDistance,
                Vector3.down + Vector3.left +                
                    transform.position + transform.forward * frustum.FarClipDistance
            }
            );
    }
}