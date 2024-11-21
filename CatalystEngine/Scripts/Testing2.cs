
using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;
using CatalystEngine.Components;

internal class Testing2 : IScript
{
	public async override void Start()
	{
		gameObject.GetComponent<Rigidbody>().AngularVelocity = new Vector3(360, 0, 0);	
	}

	public async override void Update()
	{
		//Code that runs every frame
	}
}
