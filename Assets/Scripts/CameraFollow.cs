using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    public bool follow = false;

    private void FixedUpdate()
    {
        if (!follow) return;

        if (target.position.y >= 0)
        {
            transform.position = new Vector2(0, target.transform.position.y);
        }
    }
}
