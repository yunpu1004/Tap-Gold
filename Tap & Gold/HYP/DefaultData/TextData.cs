using TMPro;
using UnityEngine;


/// TextData는 TextMeshProUGUI Component에 대한 정보를 담고 있습니다.
public class TextData : DefaultData
{
    [HideInInspector] public TextMeshProUGUI tmp;
    
    private ValueFlag<string> text;
    private ValueFlag<float> fontSize;
    private ValueFlag<Color> color;

 
    
    public override void OnInit()
    {
        tmp = GetComponent<TextMeshProUGUI>();

        text = new (tmp.text);
        fontSize = new (tmp.fontSize);
        color = new (tmp.color);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// Text를 반환합니다.
    public string GetText()
    {
        return text.value;
    }

    /// Text를 변경합니다. 
    public void SetText(in string value)
    {
        text.value = value;
    }



    /// FontSize를 반환합니다.
    public float GetFontSize()
    {
        return fontSize.value;
    }

    /// FontSize를 변경합니다.
    public void SetFontSize(float value)
    {
        fontSize.value = value;
    }


    /// VertexColor를 반환합니다.
    public Color GetColor()
    {
        return color.value;
    }

    /// VertexColor를 변경합니다.
    public void SetColor(in Color value)
    {
        color.value = value;
    }



    public bool IsChanged()
    {
        return text.isChanged || fontSize.isChanged || color.isChanged;
    }


    public void Sync()
    {
        if(text.isChanged) 
        {
            tmp.text = text.value.ToString();
            text.isChanged = false; 
        }

        if(fontSize.isChanged) 
        {
            tmp.fontSize = fontSize.value;
            fontSize.isChanged = false; 
        }

        if(color.isChanged) 
        {
            tmp.color = color.value;
            color.isChanged = false; 
        }
    }
}