using System;


public class EnumFlag<T>  where T : unmanaged, Enum
{
    private T _preValue;
    private T _value;
    public bool isChanged;

    public T value 
    {
        get => _value;
        set
        {
            if(_value.Equals(value)) return;
            _preValue = _value;
            _value = value;
            isChanged = true;
        }
    }

    public T preValue 
    {
        get => _preValue;
    }

    public EnumFlag(T value)
    {
        _value = value;
        _preValue = value;
        isChanged = false;
    }
}