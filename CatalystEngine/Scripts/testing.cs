using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;
using CatalystEngine.Components;

internal class Testing : IScript
{
    //float yRot = 0;
    public async override void Start()
    {
        gameObject.GetComponent<Rigidbody>().AngularVelocity = new Vector3(360, 360, 0);
    }

    public async override void Update()
    {
        //gameObject.Rotation = QuaternionHelper.CreateFromEulerAnglesDegrees(yRot, yRot, 0f);
        //gameObject.Rotation = QuaternionHelper.CreateFromEulerAnglesDegrees(gameObject.Rotation.X, gameObject.Rotation.Y + 1f * Time.DeltaTime, gameObject.Rotation.Z);
    }
}