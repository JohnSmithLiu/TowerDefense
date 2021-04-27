using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private float speed = 15;
    private float mouseSpeed = 200;
    // Update is called once per frame
    void Update()
    {
        float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(v*speed, mouse*mouseSpeed, h*speed)*Time.deltaTime, Space.World);
    }
}
