using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameAnalyticsSDK;
public class ShopManager : MonoBehaviour
{
   
    private AudioSource audioSource;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Image levelImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private ParticleSystem levelUpParticle;
    private void Awake()
    {
        GameAnalytics.Initialize();
        SetLevelUI();
        audioSource = GetComponent<AudioSource>();
    }
    public void RuleLevelUp(Item item)
    {
        levelManager.currentExp += item.Exp;
        if(levelManager.currentExp >= levelManager.needExp)
        {
            levelManager.currentLevel++;
            levelManager.currentExp = 0;
            levelManager.needExp += levelManager.needExp * .2f;
            audioSource.Play();
            levelUpParticle.Play();
        }
        SetLevelUI();
    }
    public void SetLevelUI()
    {
        levelImage.fillAmount = levelManager.currentExp / levelManager.needExp;
        levelText.text = levelManager.currentLevel.ToString();
    }
}
