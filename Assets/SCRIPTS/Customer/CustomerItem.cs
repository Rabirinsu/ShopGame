using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CustomerItem : MonoBehaviour
{
    public Item item;
    public float currentItemValue;
    [SerializeField] private Items items;
    private void OnEnable()
    {
        item = items.ItemList[Random.Range(0, items.ItemList.Count)];
        var itemUI = this.gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        itemUI.GetComponent<Image>().sprite = item.itemUI;
        SetItemPrice();
        InTradeUI();
    }

    private void InTradeUI()
    {
        var itemUI2 = this.gameObject.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).gameObject;
        var itemName = this.gameObject.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).gameObject;
        var itemValue = this.gameObject.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(2).gameObject;
       
        itemUI2.GetComponent<Image>().sprite = item.itemUI;
        itemName.GetComponent<TextMeshProUGUI>().text = item.Name;
        itemValue.GetComponent<TextMeshProUGUI>().text = currentItemValue.ToString();
    }

    private void SetItemPrice()
    {
        if (gameObject.CompareTag("BuyerBoss"))
        {
            currentItemValue = item.Value * 30;
        }
     else   if (gameObject.CompareTag("Buyer"))
        {
            currentItemValue = item.Value;
        }
        else currentItemValue = (int)(item.Value / 3); // TODO : Set Seller Dynamic Item Divide Amount 
    }
}
