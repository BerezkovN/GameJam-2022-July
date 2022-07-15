using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    //cube to rotate
    public GameObject cube;
    //Assign dge pos from the editor
    public Transform edgePivotPoint;

    public float rotSpeed = 60f;

    private void Update()
    {
        cube.transform.RotateAround(edgePivotPoint.position, Vector3.back, rotSpeed * Time.deltaTime);
    }
}
