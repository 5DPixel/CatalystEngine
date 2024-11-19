using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_1.Graphics
{
    internal class IBO
    {
        public int ID;
        public IBO(List<uint> data)
        {
            ID = GL.GenBuffer();
            this.Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.DeleteBuffer(ID);
        }
    }
}
