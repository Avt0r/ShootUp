using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunPreset")]
public class GunPreset : ScriptableObject
{
    [SerializeField] private int clipSize;
    [SerializeField] private float forcePower;
    [SerializeField] private int price;

    public int ClipSize { get { return clipSize; } }

    public float ForcePower { get { return forcePower; } } 

    public int Price { get { return price; } }
}
