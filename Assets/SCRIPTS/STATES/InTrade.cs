using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customer/States/InTrade")]

public class InTrade : CustomerState
{
    public override bool ExitState(StateControllerCustomer CustomerStateController)
    {
        if (CustomerStateController.isExit || CustomerStateController.Vcam.Follow != CustomerStateController.gameObject.transform)
        {
            CustomerStateController.SendMessage("OutTrade", SendMessageOptions.DontRequireReceiver);
            Debug.Log("IM OUT TRADE");
            return true;
        }
            
        return false;
    }
    public override void ExecuteState(StateControllerCustomer CustomerStateController) 
    {
        CustomerStateController.GetComponent<TradeBrain>().CheckTradeState();
        Debug.Log("IM IN TRADE");
    }
    public override bool CheckRules(StateControllerCustomer CustomerStateController)
    {
        if (!CustomerStateController.isExit && CustomerStateController.Vcam.Follow == CustomerStateController.gameObject.transform && !CustomerStateController.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return true;
            return false;
    }   
}
