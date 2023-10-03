using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GunController : Initializable
{
    [SerializeField] private GunPreset preset;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;

    [SerializeField] private int rotationSpeed = 0;
    [SerializeField] private int speedChange;
    [SerializeField] private int drag;

    [SerializeField] private int ammo;
    [SerializeField] private int score;

    private void OnValidate()
    {
        bullet = bullet != null ? bullet : Resources.Load<GameObject>("Prefabs/Bullet");
        rb = rb != null ? rb : GetComponent<Rigidbody2D>();
    }

    public override void OnInit()
    {
        ammo = preset.ClipSize;
        score = 0;
    }

    private void FixedUpdate()
    {
        RotateObject();
        if(transform.position.y > score)
        {
            Bootstrap.Score = (int)transform.position.y;
            score = Bootstrap.Score;
        }
    }

    public void Shoot()
    {
        if (ammo <= 0) return;

        Destroy(Instantiate(bullet, shotPoint.position, transform.rotation),1f);

        rb.AddForce(transform.up * preset.ForcePower, ForceMode2D.Impulse);

        if (transform.rotation.z >= 0 && transform.rotation.z < 180)
        {
            rotationSpeed = -speedChange;
        }
        else
        {
            rotationSpeed = speedChange;
        }

        ammo--;
    }

    private void RotateObject()
    {

        float rotationAmount = rotationSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation - rotationAmount);

        rotationSpeed = rotationSpeed == 0 ? 0 : rotationSpeed > 0 ? rotationSpeed - drag : rotationSpeed + drag;
    }

    public void AddForce(Vector2 vector)
    {
        rb.AddForce(vector * preset.ForcePower, ForceMode2D.Impulse);
    }

    public void RefillAmmo()
    {
        ammo = preset.ClipSize;
    }

    public string GetInfoAboutAmmo()
    {
        return ammo + "/" + preset.ClipSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Coin":
                {
                    Bootstrap.EarnCoin();
                    Destroy(collision.gameObject);
                }break;
            case "Ammo":
                {
                    RefillAmmo();
                    Destroy(collision.gameObject);
                }break;
            case "Boost":
                {
                    AddForce(Vector2.up);
                    Destroy(collision.gameObject);
                }break;
        }

    }
}
