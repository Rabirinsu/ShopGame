
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Inventory : MonoBehaviour
{
    [SerializeField] private float animateDelay;
   public  List<Item> Items;
    private GameObject itemClone;
    private float offsetX = -0.7f;
    private Vector3 offset = new Vector3(2.2f, 1f, -5.65f);
    [SerializeField] private int tableCapacity;
    [SerializeField] private Transform table;
    public List<GameObject> TableItems;

   
    public void BuyItemInInventory(GameObject customer, Item item)
    { 
        Items.Add(item);
        ItemOnTable(customer, item);
    }
    public GameObject SellItemInInventory(GameObject customer, Item item)
    {
        var itemspawnPosition = offset;
        var hand = customer.transform.GetChild(5).gameObject;
        itemClone = Instantiate(item.itemObject, itemspawnPosition, Quaternion.identity, hand.transform);
        itemClone.transform.DOMove(hand.transform.position, animateDelay);
        for (var i =0; i < Items.Count; i++)
        {
            if(Items[i].Name == item.Name )
            {
                Items.RemoveAt(i);
                ItemOutTable(item);
                break;
            }
        }
        return itemClone;
    }

    private void ItemOnTable(GameObject customer,Item item)
    {
        var itemspawnPosition = customer.transform.GetChild(4).position;
        if (TableItems.Count < tableCapacity)
        {
            switch (item.Name)
            {
                case "Axe":
                    itemClone = Instantiate(item.itemObject, itemspawnPosition, Quaternion.Euler(-90, 180, 0));
                    itemClone.transform.DOMove(offset, animateDelay);
                    break;
                case "Shield":
                    itemClone = Instantiate(item.itemObject, itemspawnPosition, Quaternion.Euler(-90,0,0));
                    itemClone.transform.DOMove(offset, animateDelay);
                    break;
                default:
                    itemClone = Instantiate(item.itemObject, itemspawnPosition, transform.rotation);
                    itemClone.transform.DOMove(offset, animateDelay);
                    break;
            }
            
            TableItems.Add(itemClone);
            offset.x += offsetX;
        }
        else return;
    }
   

    private void ItemOutTable(Item item)
    {
        if(Items.Count <= tableCapacity)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].itemObject == TableItems[i])
                {
                    Destroy(TableItems[i]);
                    break;
                }
            }
        }
    }
}
