using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraDrag : MonoBehaviour {

    float speed = 10.0f;
    int boundary = 1;
    int width;
    int height;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;

    }

    void Update()
    {
        if (Input.mousePosition.x > width - boundary)
        {
            transform.position -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
                                       0.0f, 0.0f);
        }

        if (Input.mousePosition.x < 0 + boundary)
        {
            transform.position -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
                                       0.0f, 0.0f);
        }

        if (Input.mousePosition.y > height - boundary)
        {
            transform.position -= new Vector3(0.0f, 0.0f,
                                       Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
        }

        if (Input.mousePosition.y < 0 + boundary)
        {
            transform.position -= new Vector3(0.0f, 0.0f,
                                       Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
        }

    }
}
