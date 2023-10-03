using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.AddForce(Vector2.up * 50,ForceMode2D.Impulse);
    }
}
