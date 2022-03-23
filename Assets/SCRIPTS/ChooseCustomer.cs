using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ChooseCustomer : MonoBehaviour
{
    public RaycastHit hit;
    private int layerMask;
    [SerializeField] private CinemachineVirtualCamera Vcam;
    void Update()
    {
        RaycastOnCustomer();
    }
    private void Start()
    {
        layerMask = 1 << 7;
    }
    private void RaycastOnCustomer()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
  
            if (Physics.Raycast(ray, out hit, layerMask))
            {
             
                Debug.Log(hit.transform.name);
                if ((Vcam.m_Lens.FieldOfView >= (CameraValue.maxFOV - 1) || (Vcam.m_Lens.FieldOfView <= (CameraValue.talkFOV + 1))) && (hit.transform.gameObject.CompareTag("BuyerBoss") || hit.transform.gameObject.CompareTag("Buyer") || hit.transform.gameObject.CompareTag("Seller")))
                {
                    var customerAgent = hit.transform.gameObject.GetComponent<StateControllerCustomer>().agent;
                    if(!customerAgent.enabled )
                    {
                        // TODO : check customer type : Buyer  ? Seller 
                        hit.transform.gameObject.SendMessage("SellTrade", SendMessageOptions.DontRequireReceiver); // This is Actualy InTrade ExecuteState code
                        GameObject touchedObject = hit.transform.gameObject;

                    }
                }
            }
        }
    }
}
