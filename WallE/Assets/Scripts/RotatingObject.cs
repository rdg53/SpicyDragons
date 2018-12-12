using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {

    public float rotationSpeed = 1;


    private void FixedUpdate()
    {
        transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
