using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerState : ScriptableObject
{
    public List<CustomerState> transitions;

    public abstract bool ExitState(StateControllerCustomer CustomerStateController);
    public abstract void ExecuteState(StateControllerCustomer CustomerStateController);
    public abstract bool CheckRules(StateControllerCustomer CustomerStateController);


}
