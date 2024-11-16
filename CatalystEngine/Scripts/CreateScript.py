import os

name = input("Choose script name: ")

template = f"""
using System;

public class {name} {{
	public void Start()
	{{
		//Code that runs when the application starts
	}}

	public void Update()
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