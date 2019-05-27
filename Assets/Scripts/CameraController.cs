using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSpeed = 1f;
    public float cameraSpeed = 4f;
    public GameObject cameraTarget;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        

        transform.eulerAngles -= new Vector3(Input.GetAxis("Mouse Y") * mouseSpeed, -Input.GetAxis("Mouse X") * mouseSpeed, 0.0f);


        if (Input.GetKey(KeyCode.W)){
            transform.position += cameraTarget.transform.forward * (Time.deltaTime * cameraSpeed);
        }


        if (Input.GetKey(KeyCode.S)) {
            transform.position -= cameraTarget.transform.forward * (Time.deltaTime * cameraSpeed);
        }


        if (Input.GetKey(KeyCode.A)) {

            transform.position += Vector3.left * (Time.deltaTime * cameraSpeed);
        }


        if (Input.GetKey(KeyCode.D)) {

            transform.position += Vector3.right * (Time.deltaTime * cameraSpeed);
        }

    }
}
