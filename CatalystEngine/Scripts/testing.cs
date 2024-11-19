using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;

[ApplyToName("monkey")]
internal class Testing : ScriptBase
{
    public Camera currentCamera;
    public override void Start()
    {
        Console.WriteLine("Hello world! This is the first script");
    }

    protected override void Update(GameObject currentInstance)
    {
        currentInstance.Position = new Vector3(currentInstance.Position.X, currentInstance.Position.Y + 0.001f, currentInstance.Position.Z);
        currentCamera.yaw += 0.01f;
        Console.WriteLine(Time.GetEpochMilliseconds());
    }
}