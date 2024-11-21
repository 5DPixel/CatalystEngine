using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;

internal class Testing : IScript
{
    float yRot = 0;
    public async override void Start()
    {
        await Time.Wait(1);
        Debug.Log("First script");
    }

    public async override void Update()
    {
        yRot += 20f * Time.DeltaTime;
        gameObject.Rotation = QuaternionHelper.CreateFromEulerAnglesDegrees(yRot, yRot, 0f);
        //gameObject.Rotation = QuaternionHelper.CreateFromEulerAnglesDegrees(gameObject.Rotation.X, gameObject.Rotation.Y + 1f * Time.DeltaTime, gameObject.Rotation.Z);
    }
}