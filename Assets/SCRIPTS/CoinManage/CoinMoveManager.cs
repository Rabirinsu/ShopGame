using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoinMoveManager : MonoBehaviour
{

    private void Start()
    {
        gameObject.GetComponent<CoinManager>().StartMoveCoin( () => 
            {
                Debug.Log("Action Completed");
            }  
        );
    }
}
