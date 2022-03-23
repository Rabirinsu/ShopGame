
using UnityEngine;
using UnityEngine.UI;

public class BossAddButton : MonoBehaviour
{
    [SerializeField] private Button AddVideoButton;
    private UnityAds Ads;
    void Awake()
    {
      Ads = GameObject.Find("AddsManager").GetComponent<UnityAds>();
      
    }

    private void Start()
    {
        AddVideoButton.onClick.AddListener(Ads.ShowInterstitial);
    }
}
