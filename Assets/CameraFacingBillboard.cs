﻿using System.Collections;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    private void Update()
    {
        Camera cam = Camera.main;

        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
