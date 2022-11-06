using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class MemScript : MonoBehaviour, IPointerClickHandler
{
    public VideoPlayer videoPlayer;
    public RectTransform panelRectTransform;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        panelRectTransform.anchorMin = new Vector2(1, 0);
        panelRectTransform.anchorMax = new Vector2(0, 1);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }
}
