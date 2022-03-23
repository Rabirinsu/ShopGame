using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class OnTrade : MonoBehaviour
{
  public GameObject tradeUI;
  private CinemachineVirtualCamera Vcam;
    private void OnEnable()
    {
        Vcam = GameObject.Find("MainVcam").GetComponent<CinemachineVirtualCamera>();
        tradeUI = transform.GetChild(3).gameObject;
    }
    public void SellTrade()
    {
        var itemUI = gameObject.transform.GetChild(2).GetChild(0).gameObject;
        itemUI.SetActive(false);
        var itemDecription = gameObject.transform.GetChild(2).GetChild(1).gameObject;
        itemDecription.SetActive(true);
  
        tradeUI.SetActive(true);

      
         CameraSmoothZoomIn();
    }

    public void OutTrade()
    {
        var itemUI = gameObject.transform.GetChild(2).GetChild(0).gameObject;
        itemUI.SetActive(true);
        var itemDecription = gameObject.transform.GetChild(2).GetChild(1).gameObject;
        itemDecription.SetActive(false);
 
        tradeUI.SetActive(false);

    
    }

    private void CameraSmoothZoomIn()
    {
        Vcam.Follow = this.transform;
        Vcam.LookAt = this.transform;
        var tradeBrain = gameObject.GetComponent<TradeBrain>();
     tradeBrain.inTrade = true;
        tradeBrain.outTrade = false;
    }
}
