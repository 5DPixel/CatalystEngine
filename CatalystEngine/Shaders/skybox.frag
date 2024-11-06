#version 330 core

in vec2 texCoord;  // Texture coordinates passed from the vertex shader
out vec4 FragColor;  // Final color output to the screen

uniform sampler2D texture0;  // Skybox texture sampler

void main()
{
    // Simply sample the texture at the given texture coordinates
    vec3 texColor = texture(texture0, texCoord).rgb;
    texColor = pow(texColor, vec3(2.2)); // Gamma correction to brighten colors
    FragColor = vec4(texColor, 1.0);


    // Output the color of the skybox
    //FragColor = vec4(texColor, 1.0);
}
