using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetcameraFollow;
    public void Setup(Func<Vector3> GetcameraFollow)
    {
        this.GetcameraFollow =GetcameraFollow;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollow = GetcameraFollow();
        cameraFollow.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollow - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollow, transform.position);
        float cameraMoveSpeed = 1f;


        transform.position = transform.position+cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
    }
}
