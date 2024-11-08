using OpenTK.Graphics.OpenGL4;

namespace CatalystEngine.Graphics
{
    internal class DepthTexture
    {
        public int ID { get; private set; }
        private const int ShadowWidth = 1024;
        private const int ShadowHeight = 1024;

        public DepthTexture()
        {
            // Generate texture ID
            ID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, ID);

            // Allocate storage for the texture but without any data (NULL as last argument)
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, ShadowWidth, ShadowHeight, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);

            // Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
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
