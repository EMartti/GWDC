using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    [SerializeField] private Button perkButton;
    [SerializeField] private Image buttonImage;

    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    [SerializeField] private bool isButtonActive;


    void Start()
    {
        perkButton = GetComponent<Button>();
        isButtonActive = false;
        buttonImage = perkButton.GetComponent<Image>();     
    }


    public void OnClicked()
    {
        if (isButtonActive == false)
        {
            buttonImage.sprite = activeSprite;
            isButtonActive = true;
        }
        else
        {
            buttonImage.sprite = inactiveSprite;
            isButtonActive = false;
        }
    }
}
