using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Item")]
public class Item : ScriptableObject
{
    public Sprite itemUI;
    public string Name;
    public float Value;
    public GameObject itemObject;
    public string Description;
    public float Exp;
}
