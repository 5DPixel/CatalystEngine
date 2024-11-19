using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;

internal class Testing : IScript
{
    public async void Start()
    {
        await Time.Wait(1);
        Console.WriteLine("Hello world! This is the first script");
    }

    public async void Update()
    {
        //currentInstance.Position = new Vector3(currentInstance.Position.X, currentInstance.Position.Y + 0.001f, currentInstance.Position.Z);
        //currentCamera.yaw += 0.01f;
    }
}