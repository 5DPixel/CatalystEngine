using System;
using OpenTK.Graphics.OpenGL4;

namespace CatalystEngine.Graphics
{
    internal class FBO
    {
        public int ID { get; private set; }
        private int colorRenderbuffer;
        private int depthRenderbuffer;

        // Constructor with dimensions for the framebuffer
        public FBO(int width, int height)
        {
            // Generate framebuffer
            ID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);

            // Create and attach color renderbuffer
            colorRenderbuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, colorRenderbuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Rgba8, width, height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, RenderbufferTarget.Renderbuffer, colorRenderbuffer);

            // Create and attach depth renderbuffer (if needed)
            depthRenderbuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depthRenderbuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent24, width, height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthRenderbuffer);

            // Check if the framebuffer is complete
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("ERROR::FRAMEBUFFER:: Framebuffer is not complete!");
            }

            // Unbind the framebuffer for now
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void AttachDepthTexture(int depthTextureID)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthTextureID, 0);

            // Optional: Specify no color buffer (for shadow mapping)
            GL.DrawBuffer(DrawBufferMode.None);
            GL.ReadBuffer(ReadBufferMode.None);

            // Check for completeness
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("ERROR::FRAMEBUFFER:: Framebuffer is not complete!");
            }

            // Unbind the framebuffer to prevent accidental modifications
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        // Bind this framebuffer for rendering
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
        }

        // Unbind the framebuffer (binds the default framebuffer)
        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        // Cleanup resources when no longer needed
        public void Delete()
        {
            GL.DeleteFramebuffer(ID);
            GL.DeleteRenderbuffer(colorRenderbuffer);
            GL.DeleteRenderbuffer(depthRenderbuffer);
        }
    }
}
