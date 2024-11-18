using CatalystEngine.Models;
using OpenTK.Mathematics;

[ApplyToName("monkey")]
internal class Testing : ScriptBase
{
    public override void Start()
    {
        Console.WriteLine("Hello world! This is the first script");
    }

    protected override void Update(GameObject currentInstance)
    {
        currentInstance.Position = new Vector3(currentInstance.Position.X, currentInstance.Position.Y + 0.001f, currentInstance.Position.Z);
    }
}