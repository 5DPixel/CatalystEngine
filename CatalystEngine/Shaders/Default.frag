#version 330 core

in vec2 texCoord;
in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

uniform sampler2D texture0;
uniform vec3 lightPos;
uniform vec3 lightColor;
uniform vec3 viewPos;

void main()
{
	float specularStrength = 0.5;
	float ambientStrength = 0.5;
	float shininess = 32;
	float distance = length(lightPos - FragPos);
	float attenuation = 1.0 / distance;  

	vec3 ambient = ambientStrength * lightColor;
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(lightPos - FragPos);
	vec3 viewDir = normalize(viewPos - FragPos);
	vec3 halfwayDir = normalize(lightDir + viewDir);
	vec3 reflectDir = reflect(-lightDir, norm);

	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;

	float spec = pow(max(dot(viewDir, halfwayDir), 0.0), shininess);
	vec3 specular = specularStrength * spec * lightColor;

	diffuse *= attenuation;
	specular *= attenuation;

	vec3 texColor = texture(texture0, texCoord).rgb;

	vec3 result = (ambient + diffuse + specular) * texColor;
	FragColor = vec4(result, 1.0);
}