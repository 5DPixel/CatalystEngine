#version 330 core

in vec2 texCoord;
in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

uniform sampler2D texture0;
uniform vec3 lightPos;
uniform vec3 lightColor;

void main()
{
	float ambientStrength = 0.5;
	vec3 ambient = ambientStrength * lightColor;
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(lightPos - FragPos);

	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;

	vec3 texColor = texture(texture0, texCoord).rgb;
	vec3 result = (ambient + diffuse) * texColor;

	FragColor = vec4(result, 1.0);
}