using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;

    public void MuteSound()
    {
        AudioListener.pause = true;
        soundOn.SetActive(false);
        soundOff.SetActive(true);
     }
    public void UpSound()
    {
        AudioListener.pause = false;
        soundOff.SetActive(false);
        soundOn.SetActive(true);
       
    }
}
