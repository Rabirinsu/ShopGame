using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "Customer/States/InShop")]

public class InShop : CustomerState
{
    public override bool ExitState(StateControllerCustomer CustomerStateController)
    {
        if ( CustomerStateController.Vcam.Follow == CustomerStateController.gameObject.transform)
            return true;
        return false;
    }
    public override void ExecuteState(StateControllerCustomer CustomerStateController) 
    {
        CustomerStateController.agent.enabled = false;
        CustomerStateController.GetComponent<NavMeshObstacle>().enabled = true;
        CustomerStateController.animator.SetBool("Walk", false);
        CustomerStateController.animator.SetBool("Idle", true);
       
        CustomerStateController.transform.LookAt(CustomerStateController.player.transform);
       
    }
    public override bool CheckRules(StateControllerCustomer CustomerStateController)
    {
        if (!CustomerStateController.isExit && CustomerStateController.isShop)
        {
            if((CustomerStateController.Vcam.Follow != CustomerStateController.gameObject.transform && CustomerStateController.GetComponent<NavMeshObstacle>().enabled) || (CustomerStateController.agent.remainingDistance <= 0 && CustomerStateController.gameObject.GetComponent<CustomerItem>().item != null))
                     return true;
        }
           
            return false;
    }
}
