using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace CatalystEngine.Graphics
{
    internal class ShadowCubeMapFBO
    {
        public int depthBufferID;
        public int cubeMapID;
        public int fboID;
        private int size;

        public ShadowCubeMapFBO(int size)
        {
            this.size = size;
            depthBufferID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, depthBufferID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, size, size, 0, PixelFormat.DepthComponent, PixelType.Float, new nint());

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            //Create cube map
            cubeMapID = GL.GenTexture();

            GL.BindTexture(TextureTarget.TextureCubeMap, cubeMapID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            for(uint i = 0; i < 6; i++)
            {
                GL.TexImage2D((TextureTarget)((int)TextureTarget.TextureCubeMapPositiveX + i), 0, PixelInternalFormat.R32f, size, size, 0, PixelFormat.Red, PixelType.Float, new nint());
            }

            //Create FBO
            fboID = GL.GenFramebuffer();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthBufferID, 0);

            GL.DrawBuffer(DrawBufferMode.None);

            GL.ReadBuffer(ReadBufferMode.None);
        }

        public void BindForWriting(TextureTarget CubeFace)
        {
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, fboID);

            GL.Viewport(0, 0, size, size);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, CubeFace, cubeMapID, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
        }

        public void BindForReading(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.TextureCubeMap, cubeMapID);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboID);
        }
        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
