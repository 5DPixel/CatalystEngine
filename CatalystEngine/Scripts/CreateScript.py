import os

name = input("Choose script name: ")

template = f"""
using CatalystEngine.Models;
using OpenTK.Mathematics;
using CatalystEngine;
using CatalystEngine.Utils;
using CatalystEngine.ScriptsCore;
using CatalystEngine.Components;

internal class {name} : IScript
{{
	public async override void Start()
	{{
		//Code that runs when the application starts
	}}

	public async override void Update()
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