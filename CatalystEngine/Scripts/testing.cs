using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;

internal class Testing : IScript
{
    public async override void Start()
    {
        await Time.Wait(1);
        Console.WriteLine("Hello world! This is the first script");
    }

    public async override void Update()
    {
        
    }
}