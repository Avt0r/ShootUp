using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] 
public class BorderWall : MonoBehaviour
{

    [SerializeField] private Transform _transform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector2(_transform.position.x, collision.gameObject.transform.position.y);
            Debug.Log("Collision", collision.gameObject);
        }
    }
}
