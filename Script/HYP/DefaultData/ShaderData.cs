using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;
using float4 = Unity.Mathematics.float4;
using System;
using System.Collections.Generic;

/// ShaderData는 쉐이더에 대한 정보를 담고 있습니다.
public class ShaderData : DefaultData
{
    private MeshRenderer meshRenderer;
    private SpriteRenderer spriteRenderer;
    public int propertyCount { get; private set; }
    private ShaderProperty[] propertyArray;
    private ValueFlag<Color> color;

    
    public override void OnInit()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = null;
        if(meshRenderer != null){
            material = GetComponent<MeshRenderer>().material;
            color = new ValueFlag<Color>(meshRenderer.material.color);
        }
        else if(spriteRenderer != null){
            material = GetComponent<SpriteRenderer>().material;
            color = new ValueFlag<Color>(spriteRenderer.material.color);
        }
        else{
            throw new Exception("ShaderData: MeshRenderer 또는 SpriteRenderer 이 존재하지 않습니다.");
        }
        
        var shader = material.shader;

        var count = shader.GetPropertyCount();

        var propertyList = new List<ShaderProperty>();

        for (int i = 0; i < count; i++)
        {
            string propertyName = shader.GetPropertyName(i);
            int id = shader.GetPropertyNameId(i);
        
            if (material.HasInteger(id))
            {
                string name = propertyName;
                float4 value = float4(material.GetInt(propertyName), 0, 0, 0);
                ShaderProperty.InputType type = ShaderProperty.InputType.Int;
                ShaderProperty shaderProperty = new ShaderProperty(name, type, value);
                propertyList.Add(shaderProperty);
            }
            else if(material.HasFloat(id))
            {
                string name = propertyName;
                float4 value = float4(material.GetFloat(propertyName), 0, 0, 0);
                ShaderProperty.InputType type = ShaderProperty.InputType.Float;
                ShaderProperty shaderProperty = new ShaderProperty(name, type, value);
                propertyList.Add(shaderProperty);
            }
            else if(material.HasVector(id))
            {
                string name = propertyName;
                float4 value = material.GetVector(propertyName);
                ShaderProperty.InputType type = ShaderProperty.InputType.Vector;
                ShaderProperty shaderProperty = new ShaderProperty(name, type, value);
                propertyList.Add(shaderProperty);
            }
        }
        propertyCount = propertyList.Count;
        propertyArray = propertyList.ToArray();
        ComponentManager.AddComponent(hierarchyName, this);
    }




    /// 프로퍼티의 값을 반환합니다.
    /// 만약 해당 이름의 프로퍼티가 없다면 float4(0, 0, 0, 0)을 반환합니다.
    public float4 GetPropertyValue(string name)
    {
        if(propertyCount == 0) return -1;

        for(int i = 0; i < propertyCount; i++)
        {
            if(propertyArray[i].name.Equals(name))
            {
                return propertyArray[i].GetValue();
            }
        }
        Debug.Log($"The Material Property '{name}' 가 존재하지 않습니다.");
        return -1;
    }



    /// 프로퍼티의 값을 변경합니다.
    public void SetPropertyValue(string name, in float4 value)
    {
        if(propertyCount == 0) return;

        for(int i = 0; i < propertyCount; i++)
        {
            if(propertyArray[i].name.Equals(name))
            {
                propertyArray[i].SetValue(value);
                return;
            }
        }
        Debug.Log($"The Material Property '{name}' 가 존재하지 않습니다.");
    }


    /// 색상 값을 반환합니다.
    public Color GetColor()
    {
        return color.value;
    }


    /// 색상 값을 변경합니다.
    public void SetColor(in Color color)
    {
        this.color.value = color;
    }


    public bool IsChanged()
    {
        for(int i = 0; i < propertyCount; i++)
        {
            if(propertyArray[i].IsChanged()) return true;
        }

        if(color.isChanged) return true;

        return false;
    }


    public void Sync()
    {
        if(meshRenderer)
        {
            for(int i = 0; i < propertyCount; i++)
            {
                propertyArray[i].Sync(meshRenderer.material);
            }

            if(color.isChanged)
            {
                meshRenderer.material.color = color.value;
                color.isChanged = false;
            }
        }

        else if(spriteRenderer)
        {
            for(int i = 0; i < propertyCount; i++)
            {
                propertyArray[i].Sync(spriteRenderer.material);
            }

            if(color.isChanged)
            {
                spriteRenderer.material.color = color.value;
                color.isChanged = false;
            }
        }
    }
}


public struct ShaderProperty : IEquatable<ShaderProperty>
{
    public readonly string name;
    public readonly InputType inputType;
    private ValueFlag<float4> propertyValue;

    public ShaderProperty(string name, in InputType inputType, in float4 value)
    {
        this.name = name;
        this.inputType = inputType;

        if(this.inputType == InputType.Float) this.propertyValue = new (math.float4(value.x, 0, 0, 0));
        else if(this.inputType == InputType.Vector) this.propertyValue = new (value);
        else this.propertyValue = new (math.float4((int)value.x, 0, 0, 0));
    }

    public float4 GetValue()
    {
        return propertyValue.value;
    }

    public void SetValue(in float4 input)
    {
        if(inputType == InputType.Float) propertyValue.value = math.float4(input.x, 0, 0, 0);
        else if(inputType == InputType.Vector) propertyValue.value = input;
        else if(inputType == InputType.Int) propertyValue.value = math.float4((int)input.x, 0, 0, 0);
    }


    public bool IsChanged()
    {
        return propertyValue.isChanged;
    }


    public void Sync(Material material)
    {
        if(!propertyValue.isChanged) return;
        var value = propertyValue.value;
        var name_temp = name.ToString();
        if(inputType == InputType.Float) material.SetFloat(name_temp, value.x);
        else if(inputType == InputType.Vector) material.SetVector(name_temp, new Vector4(value.x, value.y, value.z, value.w));
        else if(inputType == InputType.Int) material.SetInt(name_temp, (int)value.x);
        propertyValue.isChanged = false;
    }


    public enum InputType
    {
        Int,
        Float,
        Vector,
    }


    public bool Equals(ShaderProperty other)
    {
        return name.Equals(other.name) && inputType == other.inputType && propertyValue.Equals(other.propertyValue);
    }
}