using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float factor = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertAngle = factor * Input.GetAxis("CamVertical");
        float horizAngle = factor * Input.GetAxis("CamHorizontal");
        
        this.transform.Rotate(new Vector3(0,1,0), horizAngle);
        this.transform.Rotate(new Vector3(1,0,0), vertAngle);

    }
}
