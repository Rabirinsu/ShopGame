using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customers/New Customer")]
public class Customer : ScriptableObject
{
    public string Type;
    public GameObject customerObject;
   
}
