using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ExtandManager : MonoBehaviour
{
    [SerializeField] private float fractionDelay;
    [SerializeField] private ChooseCustomer rayInfo;
    [HideInInspector]public int saleCoin;
    [SerializeField] private bool isVisible;
    public ExtandableObject extandableObject;
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject UIGameObject;
    [SerializeField] private TextMeshProUGUI salecoinTxt;
    [SerializeField] private TextMeshProUGUI totalcoinTxt;
    [SerializeField] private LevelManager levelManager;
    private float timer;
    private const float exactTime = 3;

    void Awake()
    {
        saleCoin = extandableObject.saleCoin;
        salecoinTxt.text = saleCoin.ToString();
        timer = exactTime;
    }

   

    void Update()
    {
        if( !isVisible && saleCoin <=0)
        {
            ActiveExtandable();
        }
        if(!isVisible && levelManager.TotalCoin > saleCoin && Input.touchCount >0 &&  rayInfo.hit.transform.gameObject.CompareTag("Extandable"))
        {
            AnimateCoins();
            saleCoin--;
            salecoinTxt.text = saleCoin.ToString();
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                ActiveExtandable();
            }
        }
    }

    private void AnimateCoins()
    {
        var coinClone = Instantiate(Coin, transform.position, Quaternion.identity);
        coinClone.transform.DOMove(UIGameObject.transform.position, fractionDelay).OnComplete(() => { Destroy(coinClone); SetCoinUI(); }); 
    }
    private void SetCoinUI()
    {
        levelManager.TotalCoin--;
        totalcoinTxt.text = levelManager.TotalCoin.ToString();
    }
    private void ActiveExtandable()
    {
        UIGameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        isVisible = true;
        levelManager.TotalCoin-= saleCoin--;
        SetCoinUI();
        timer = exactTime;
    }
}
