using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    float time = 5f;
    int side = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {   if (side == 0)
            {
                side = 1;
            }
            else {
                side = 0;
            }
            time = 5f;
        }

        if (side == 0)
        {
            this.transform.position += new Vector3(-0.2f, 0);
        }
        else {
            this.transform.position += new Vector3(0.2f, 0);
        }
    }
}
