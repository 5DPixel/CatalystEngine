using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalystEngine.Graphics;
using CatalystEngine.Models;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CatalystEngine
{
    internal sealed class Window : GameWindow
    {
        Block block;
        Plane plane;
        VAO vao;
        IBO ibo;
        VBO vbo;
        ShaderProgram program;
        Texture texture;
        Texture wood;
        Scene scene;
        string scenePath = "blocks";

        private int frameCount;
        private double elapsedTime;
        private int fps;

        //Camera
        Camera camera;

        //float yRotation = 0f;

        int width, height;
        public Window(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(width, height));
            this.width = width;
            this.height = height;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            texture = new Texture("Texture.png");
            wood = new Texture("Wood.jpg");

            block = new Block(new Vector3(0, 0 ,0), texture);
            plane = new Plane(new Vector3(0, 0, -0.3f), wood, 90f, 5f); //Coordinate system is really messed up but whatever

            vao = new VAO();
            
            vbo = new VBO(block.vertices);
            vao.LinkToVAO(0, 3, vbo);
            VBO uvVBO = new VBO(block.texCoords);
            vao.LinkToVAO(1, 2, uvVBO);
            ibo = new IBO(block.indices);

            program = new ShaderProgram("Default.vert", "Default.frag");

            GL.Enable(EnableCap.DepthTest);

            scene = new Scene($"../../../Scenes/{scenePath}.json");
            scene.Load();

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }   

        protected override void OnUnload()
        {
            base.OnUnload();

            vao.Delete();
            vbo.Delete();
            ibo.Delete();
            texture.Delete();
            program.Delete();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(new Color4(0.6f, 0.3f, 1f, 1f));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            program.Bind();
            vao.Bind();
            ibo.Bind();
            texture.Bind();

            //transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            //block.Render(modelLocation, viewLocation, projectionLocation, view, projection);
            //plane.Render(modelLocation, viewLocation, projectionLocation, view, projection);
            scene.RenderAll(modelLocation, viewLocation, projectionLocation, view, projection);

            frameCount++;
            elapsedTime += args.Time;

            if (elapsedTime >= 1.0) // Every second
            {
                fps = frameCount; // Capture the FPS
                Console.Write($"\rFPS: {fps}"); // Print to console
                frameCount = 0; // Reset count
                elapsedTime = 0.0; // Reset elapsed time
            }

            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);

            camera.Update(input, mouse, args);
        }
    }
}
