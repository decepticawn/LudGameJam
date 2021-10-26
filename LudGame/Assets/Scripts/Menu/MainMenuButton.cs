using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : Button
{
    public TextMeshProUGUI textMeshProText;
    public TMP_FontAsset normalFontAsset;
    public TMP_FontAsset hoverFontAsset;
    public AudioSource hoverAudioSource;
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverAudioSource != null)
        {
            hoverAudioSource.Play();
        }
        if (textMeshProText != null)
        {
            ChangeFontAssetOfTextItem(textMeshProText, hoverFontAsset);
        }
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (textMeshProText != null)
        {
            ChangeFontAssetOfTextItem(textMeshProText, normalFontAsset);
        }
        
    }

    private void ChangeFontAssetOfTextItem(TextMeshProUGUI textItem, TMP_FontAsset fontAsset)
    {
        textItem.font = fontAsset;
    }
}
