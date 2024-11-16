#version 330 core

layout (location = 0) in vec3 Position;

//uniform mat4 lightSpaceMatrix;
uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

void main(){
	gl_Position = vec4(Position, 1.0f) * model * view * projection;
}