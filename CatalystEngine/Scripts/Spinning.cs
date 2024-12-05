
using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;
using CatalystEngine.Components;

internal class Spinning : IScript
{
	Rigidbody rb;
	public async override void Start()
	{
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.AngularVelocity = new Vector3(360, 0, 0);
		//rb.gravity = -9.81f;
	}

	public async override void Update()
	{
		//Code that runs every frame
	}
}
