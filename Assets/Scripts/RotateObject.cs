using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private RectTransform _rt;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rt.Rotate(0, 0, 180 * Time.deltaTime * rotationSpeed);
    }
}
