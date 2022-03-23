using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customer/States/GoExit")]

public class GoExit : CustomerState
{
    public override bool ExitState(StateControllerCustomer CustomerStateController)
    {
        if (CustomerStateController.agent.remainingDistance <= 1)
        {
       Destroy(CustomerStateController.gameObject);

        }

        return false;
    }
    public override void ExecuteState(StateControllerCustomer CustomerStateController)
    {
        Debug.Log("IN EXIT STATE");
        CustomerStateController.animator.SetBool("Idle", false);
        CustomerStateController.animator.SetBool("Walk", true);
        CustomerStateController.agent.enabled = true;
        CustomerStateController.agent.SetDestination(CustomerStateController.SpawnerCustomer.spawnPoints[Random.Range(0, CustomerStateController.SpawnerCustomer.spawnPoints.Count)]);


    }
    public override bool CheckRules(StateControllerCustomer CustomerStateController)
    {
        if (CustomerStateController.isExit && CustomerStateController.animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            return true;
        return false;
    }
}
