using UnityEngine;

public class AnchorFitter : MonoBehaviour
{
    public AnchorFitterStyle style;

    public void GuiEvent()
    {
        if(style == AnchorFitterStyle.Fit)
            SetAnchorFit();
        else
            SetAnchorPos();
    }

    private void SetAnchorPos()
    {
        var rectTransform = GetComponent<RectTransform>();
        RectTransformUtil.SetAnchorPos(rectTransform, rectTransform.parent.GetComponent<RectTransform>());
    }

    private void SetAnchorFit()
    {
        var rectTransform = GetComponent<RectTransform>();
        RectTransformUtil.SetAnchorFit(rectTransform, rectTransform.parent.GetComponent<RectTransform>());
    }
}

public enum AnchorFitterStyle
{
    Fit, Pos
}