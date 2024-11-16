#version 330 core

layout(location = 0) in vec3 _Position;
layout(location = 1) in vec2 _TexCoord;
layout(location = 2) in vec3 _Normal;

out vec2 texCoord;
out vec3 Normal;
out vec3 FragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = vec4(_Position, 1.0f) * model * view * projection; //NOTE: MULTIPLICATIVE ORDER DOES MATTER!
	FragPos = vec3(model * vec4(_Position, 1.0));
	texCoord = _TexCoord;
	Normal = _Normal;
}