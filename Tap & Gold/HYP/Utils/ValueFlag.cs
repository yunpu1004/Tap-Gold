public class ValueFlag<T>
{
    private T _value;
    public bool isChanged;

    public T value 
    {
        get => _value;
        set
        {
            if(_value.Equals(value)) return;
            _value = value;
            isChanged = true;
        }
    }

    public ValueFlag(T value)
    {
        _value = value;
        isChanged = false;
    }
}