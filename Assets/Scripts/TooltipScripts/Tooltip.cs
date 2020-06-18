using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;

    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        //transform.localPosition = localPoint;
    }


    public void ShowTooltip(string tooltipString, Transform position)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), position.position, null, out localPoint);
        transform.localPosition = localPoint;
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 8f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize, tooltipText.preferredHeight + textPaddingSize);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

}
