using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine.ScriptsCore;

internal class testing : IScript {
	public void Start()
	{
		Console.WriteLine("Hello world! This is the first script");
	}

	public void Update(Mesh currentInstance)
	{
		if(currentInstance.Name == "monkey")
		{
            currentInstance.Position = new Vector3(currentInstance.Position.X, currentInstance.Position.Y + 0.001f, currentInstance.Position.Z);
        }
	}
}
