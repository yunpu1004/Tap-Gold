using UnityEngine;
using System;
using UnityEngine.UI;


/// SpriteData는 스프라이트에 대한 정보를 담고 있습니다.
public class SpriteData : DefaultData
{
    public SpriteRenderer spriteRenderer;
    public Image image;
    private ValueFlag<Sprite> sprite;
    private ValueFlag<Color> color;

    
    public override void OnInit()
    {    
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();

        if(spriteRenderer != null)
        {
            sprite = new ValueFlag<Sprite>(spriteRenderer.sprite);
            color = new ValueFlag<Color>(spriteRenderer.color);
        }
        else if(image != null)
        {
            sprite = new ValueFlag<Sprite>(image.sprite);
            color = new ValueFlag<Color>(image.color);
        }
        else throw new Exception("SpriteData는 SpriteRenderer 또는 Image Component를 가지고 있어야 합니다.");

        ComponentManager.AddComponent(hierarchyName, this);
    }



    /// 스프라이트 이름을 반환합니다.
    public Sprite GetSprite()
    {
        return sprite.value;
    }


    /// 스프라이트 이름을 변경합니다.
    public void SetSprite(Sprite value)
    {
        sprite.value = value;
    }


    /// 색상을 반환합니다.
    public Color GetColor()
    {
        return color.value;
    }


    /// 색상을 변경합니다.
    public void SetColor(Color value)
    {
        color.value = value;
    }


    public bool IsChanged()
    {
        return sprite.isChanged || color.isChanged;
    }


    public void Sync()
    {
        if(sprite.isChanged)
        {
            if(spriteRenderer)
            {
                spriteRenderer.sprite = sprite.value;
            }
            
            else if(image)
            {
                image.sprite = sprite.value;
            }

            sprite.isChanged = false;
        }

        if(color.isChanged)
        {
            if(spriteRenderer)
            {
                spriteRenderer.color = color.value;
            }
            
            else if(image)
            {
                image.color = color.value;
            }

            color.isChanged = false;
        }
    }
}
