using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cinemachine;
using TMPro;
public class TradeBrain : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private LevelManager levelManager;
    private ShopManager ShopManager;
    private TextMeshProUGUI totalCoinText;
    private CustomerItem customerItem;
    private CustomerSpawner customerSpawner;
    private int maxParticle = 4;
    public  bool outTrade;
    public  bool inTrade;
    [SerializeField] private ParticleSystem coinParticle;
    [SerializeField] private ParticleSystem refuseParticle;
    [SerializeField] private CinemachineVirtualCamera Vcam;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sellClip;
    [SerializeField] private AudioClip buyClip;
    [SerializeField] private AudioClip refuseClip;
    private float clipVolume = .4f;
    [SerializeField]private Button sellButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button refuseButton;
    [SerializeField] private Button waitButton;
    private StateControllerCustomer stateController;
    private void OnEnable()
    {
       
        inTrade = false;
        outTrade = false;
        Vcam = GameObject.Find("MainVcam").GetComponent<CinemachineVirtualCamera>();
        audioSource = GetComponent<AudioSource>();
        var shopManagerObject = GameObject.Find("ShopManager");
        inventory = shopManagerObject.GetComponent<Inventory>();
        customerSpawner = shopManagerObject.GetComponent<CustomerSpawner>();
        ShopManager = shopManagerObject.GetComponent<ShopManager>();
        totalCoinText = GameObject.Find("MainUI").transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        totalCoinText.text = levelManager.TotalCoin.ToString();
        customerItem = GetComponent<CustomerItem>();
        sellButton.onClick.AddListener(SellItem);
        buyButton.onClick.AddListener(BuyItem);
        refuseButton.onClick.AddListener(RefuseItem);
        waitButton.onClick.AddListener(CamZoomSmooth);
        stateController = GetComponent<StateControllerCustomer>();
    }
    public void SellItem()
    {
        RuleExitState();
        audioSource.PlayOneShot(sellClip, 1);
        ShopManager.RuleLevelUp(customerItem.item);

        StartCoroutine(nameof(SellAction));


    }
    IEnumerator SellAction()
    {
        coinParticle.Play();
        var perCoinAmount = customerItem.currentItemValue / maxParticle;
       var selledItem = inventory.SellItemInInventory(this.gameObject, customerItem.item);
        yield return new WaitForSeconds(1.2f);
        Destroy(selledItem);
        for (var i=0;i< maxParticle; i++)
        {
            yield return new WaitForSeconds(.2f);
            levelManager.TotalCoin += (int)perCoinAmount;
              totalCoinText.text = levelManager.TotalCoin.ToString();
        }
    
        DeleteCustomerOnList();
    }

    public void CheckTradeState()
    {
        if(gameObject.CompareTag("Seller"))
        {
            if( levelManager.TotalCoin < customerItem.currentItemValue)
            {
                buyButton.interactable = false;
                return;
            } 
        }
        else if(gameObject.CompareTag("Buyer")|| gameObject.CompareTag("BuyerBoss"))
        {
            for(var i=0; i<inventory.Items.Count;i++)
            {
                if(inventory.Items[i].Name == customerItem.item.Name)
                {
                    return;
                } 
            } sellButton.interactable = false;
        }
      
    }
    public void BuyItem()
    {
        RuleExitState();
        audioSource.PlayOneShot(buyClip, clipVolume);
        ShopManager.RuleLevelUp(customerItem.item);
        StartCoroutine(nameof(BuyAction));
    }
    IEnumerator BuyAction()
    {
        coinParticle.Play();
        var perCoinAmount = customerItem.currentItemValue / maxParticle;
        inventory.BuyItemInInventory(this.gameObject, customerItem.item);
        for (var i = 0; i < maxParticle; i++)
        {
            yield return new WaitForSeconds(.2f);
            levelManager.TotalCoin -= (int)perCoinAmount;
            totalCoinText.text = levelManager.TotalCoin.ToString();
        }
      
        DeleteCustomerOnList();
    }

    public void RefuseItem()
    {
        RuleExitState();
        audioSource.PlayOneShot(refuseClip, clipVolume);
        refuseParticle.Play();
        DeleteCustomerOnList();
    }
    private void LateUpdate()
    {
        if (Vcam.m_Lens.FieldOfView >= CameraValue.maxFOV-1)
            outTrade = false;
        if (outTrade)
        {
            Vcam.m_Lens.FieldOfView = Mathf.Lerp(Vcam.m_Lens.FieldOfView, CameraValue.maxFOV, CameraValue.SmoothDelay);
         
        }
        if (Vcam.m_Lens.FieldOfView <= CameraValue.talkFOV +1)
            inTrade = false;
        if (inTrade)
        {
            Vcam.m_Lens.FieldOfView = Mathf.Lerp(Vcam.m_Lens.FieldOfView, CameraValue.talkFOV, CameraValue.SmoothDelay);

        }
    }
    public void WaitInShop()
    {
        CamZoomSmooth();
    }

    private void CamZoomSmooth()
    {
        Vcam.Follow = null;
        Vcam.LookAt = null;
        outTrade = true;
        inTrade = false;
    }
    private void RuleExitState()
    {
        CamZoomSmooth();
        stateController.animator.SetTrigger("By");
        gameObject.tag = "Done";
       Destroy(gameObject.GetComponent<NavMeshObstacle>());
        Destroy(gameObject.GetComponent<BoxCollider>());
        stateController.customerItemUI.SetActive(false);
        stateController.customerUI.SetActive(false);
        stateController.isExit = true;
    }
    private void DeleteCustomerOnList()
    {
        customerSpawner.SpawnedCustomers.RemoveAt(customerSpawner.SpawnedCustomers.IndexOf(this.gameObject));
    }

}
