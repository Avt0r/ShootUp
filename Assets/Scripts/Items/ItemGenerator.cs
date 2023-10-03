using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : Initializable
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject boosterPrefab;
    [SerializeField] private GameObject bulletPrefab;

    public float spawnInterval = 2f;

    private float timer = 0f;

    private bool spawning = false;

    public override void OnInit()
    {
        spawning = true;
    }

    private void Update()
    {
        if (!spawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            GenerateObject();
            timer = 0f;
        }
    }

    private void GenerateObject()
    {
        float x = Random.Range(-2, 2);
        float y = (Bootstrap.Player.transform.position.y + 10);

        Vector2 point = new Vector2(x, y);

        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                Instantiate(coinPrefab, point, Quaternion.identity).transform.SetParent(transform);
                break;
            case 1:
                Instantiate(boosterPrefab, point, Quaternion.identity).transform.SetParent(transform);
                break;
            case 2:
                Instantiate(bulletPrefab, point, Quaternion.identity).transform.SetParent(transform);
                break;
        }
    }
}
