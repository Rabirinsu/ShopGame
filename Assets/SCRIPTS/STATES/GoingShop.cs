using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customer/States/GoingShop")]

public class GoingShop : CustomerState
{
    public override bool ExitState(StateControllerCustomer CustomerStateController)
    {
        if (CustomerStateController.agent.remainingDistance <= 0)
            return true;
        else return false;
    }
    public override void ExecuteState(StateControllerCustomer CustomerStateController) 
    {
      
    }
    public override bool CheckRules(StateControllerCustomer CustomerStateController)
    {
        return false;
    }
}
