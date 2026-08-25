using System;

public class WeightedChoice<T>
{
    public T value {get; private set;}
    public int weight {get; private set;}

    public WeightedChoice(T value, int weight)
    {
        this.value = value;
        this.weight = weight;
    }
}
