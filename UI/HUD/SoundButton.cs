using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public SoundsController soundsController;
    public Image soundOnImg;
    public Image soundOffImg;

    private void Awake()
    {
        soundOnImg.gameObject.SetActive(true);
        soundOffImg.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ChangeVisual(SoundsController.isSoundOn);
    }

    public void SoundButtonClick()
    {
        if (soundsController != null)
            soundsController.ChangeVolume(!SoundsController.isSoundOn);
    }

    public void ChangeVisual(bool isSoundOn) 
    {
        soundOnImg.gameObject.SetActive(isSoundOn);
        soundOffImg.gameObject.SetActive(!isSoundOn);
    }

}
