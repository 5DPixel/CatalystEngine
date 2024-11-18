using System;

[AttributeUsage(AttributeTargets.Class)]
public class ApplyToName : Attribute
{
    public string Name { get; }

    public ApplyToName(string name)
    {
        Name = name;
    }

    public bool Matches(string meshName) => meshName == Name;
}