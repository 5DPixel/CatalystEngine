using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Graphics
{
    internal class Texture
    {
        public int ID;
        public Texture(string filePath)
        {
            ID = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult texture = ImageResult.FromStream(File.OpenRead($"../../../Textures/{filePath}"), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Srgb8Alpha8, texture.Width, texture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture.Data);

            Unbind();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Delete()
        {
            GL.DeleteTexture(ID);
        }
    }
}
