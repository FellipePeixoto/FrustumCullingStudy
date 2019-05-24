using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawFrustum : MonoBehaviour
{
    Camera c_camera;
    [SerializeField] LineRenderer nearClip;
    [SerializeField] LineRenderer farClip;

    private void Awake()
    {
        c_camera = GetComponent<Camera>();
    }

    private void Update()
    {
        //Near Clip
        DrawNearClip();
        DrawFarClip();
    }

    public void DrawNearClip()
    {
        if (nearClip == null)
            return;

        nearClip.positionCount = 4;
        nearClip.startWidth = 0.015f;
        nearClip.endWidth = 0.015f;
        nearClip.material = Resources.Load<Material>("Materials/NearClip");
        nearClip.loop = true;

        nearClip.SetPositions(
            new Vector3[]
            {
                Vector3.up + Vector3.left +
                    transform.position + transform.forward * c_camera.nearClipPlane,
                Vector3.up + Vector3.right +
                    transform.position + transform.forward * c_camera.nearClipPlane,
                Vector3.down + Vector3.right +
                    transform.position + transform.forward * c_camera.nearClipPlane,
                Vector3.down + Vector3.left +
                    transform.position + transform.forward * c_camera.nearClipPlane
            }
            );
    }

    public void DrawFarClip()
    {
        if (farClip == null)
            return;

        farClip.positionCount = 4;
        farClip.startWidth = 0.015f;
        farClip.endWidth = 0.015f;
        farClip.material = Resources.Load<Material>("Materials/NearClip");
        farClip.loop = true;
        
        farClip.SetPositions(
            new Vector3[]
            {
                Vector3.up + Vector3.left +
                    transform.position + transform.forward * c_camera.farClipPlane,
                Vector3.up + Vector3.right +                          
                    transform.position + transform.forward * c_camera.farClipPlane,
                Vector3.down + Vector3.right +                        
                    transform.position + transform.forward * c_camera.farClipPlane,
                Vector3.down + Vector3.left +                         
                    transform.position + transform.forward * c_camera.farClipPlane
            }
            );
    }
}