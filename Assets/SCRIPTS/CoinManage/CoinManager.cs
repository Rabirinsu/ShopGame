using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoinManager : MonoBehaviour
{
  
    public void StartMoveCoin(Action onComplete)
    {
        Debug.Log("Event Started");
        StartCoroutine(MoveCoins(onComplete));
    }
        
   IEnumerator MoveCoins(Action onComplete)
    {
        yield return new WaitForSeconds(2);
        onComplete.Invoke();
    }



}
