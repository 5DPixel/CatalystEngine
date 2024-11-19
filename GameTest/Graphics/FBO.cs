using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.Graphics
{
    internal class FBO
    {
        public int texture;
        public int ID;

        public FBO()
        {
            ID = GL.GenFramebuffer();
            GL.GenTexture();
        }

        public void BindTexture()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, texture);
        }

        public void UnbindTexture()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
