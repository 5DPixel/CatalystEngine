import os

name = input("Choose script name: ")

template = f"""
using CatalystEngine.Models;
using OpenTK.Mathematics;

[ApplyToName("name")] //Apply logic to GameObject with name x
internal class {name} : ScriptBase
{{
	public override void Start()
	{{
		//Code that runs when the application starts
	}}

	protected override void Update(GameObject currentInstance)
	{{
		//Code that runs every frame
	}}
}}
"""
filename = f"{name}.cs"

if os.path.exists(filename):
	print(f"Error: {filename} already exists")
else:
	with open(filename, "w") as file:
		file.write(template)
	print(f"{filename} finished writing")