﻿using System;
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
        ShaderProgram program;
        ShaderProgram skyboxProgram;
        ShaderProgram depthProgram;
        FBO depthMapFBO;
        DepthTexture depth;
        Mesh skybox;
        Scene scene;
        string sceneName;

        private int frameCount;
        private double elapsedTime;
        private int fps;
        private Vector3 _lightPos;
        private Vector3 _lightColor;

        //Camera
        Camera camera;

        //float yRotation = 0f;

        int width, height;
        public Window(int width, int height, string scenename) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(width, height));
            this.width = width;
            this.height = height;
            this.sceneName = scenename;
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

            depthMapFBO = new FBO(1024, 1024);
            depth = new DepthTexture();
            depthMapFBO.AttachDepthTexture(depth.ID);

            skybox = new Mesh(new Vector3(0, 0, 0), new Texture("px.png"), "../../../OBJs/cube.obj", 90f, 1f);


            string scenePath = $"../../../Scenes/{sceneName}.json";
            scene = new Scene(scenePath);
            scene.Load();

            // Initialize shaders
            skyboxProgram = new ShaderProgram("skybox.vert", "skybox.frag");
            program = new ShaderProgram("Default.vert", "Default.frag");
            depthProgram = new ShaderProgram("simpleDepthShader.vert", "simpleDepthShader.frag");

            _lightPos = scene.lightPos;
            _lightColor = scene.lightColor;

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // Initialize the camera
            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }



        protected override void OnUnload()
        {
            base.OnUnload();

            //texture.Delete();
            program.Delete();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(new Color4(0.6f, 0.3f, 1f, 1f));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Set transformation matrices for your scene
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            int skyboxModelLocation = GL.GetUniformLocation(skyboxProgram.ID, "model");
            int skyboxViewLocation = GL.GetUniformLocation(skyboxProgram.ID, "view");
            int skyboxProjectionLocation = GL.GetUniformLocation(skyboxProgram.ID, "projection");

            int lightPosLocation = GL.GetUniformLocation(program.ID, "lightPos");
            int lightColorLocation = GL.GetUniformLocation(program.ID, "lightColor");
            int viewPosLocation = GL.GetUniformLocation(program.ID, "viewPos");

            GL.Uniform3(lightPosLocation, _lightPos.X, _lightPos.Y, _lightPos.Z);
            GL.Uniform3(lightColorLocation, _lightColor.X, _lightColor.Y, _lightColor.Z);
            GL.Uniform3(viewPosLocation, camera.position.X, camera.position.Y, camera.position.Z);

            skyboxProgram.Bind();


            Matrix4 skyboxView = camera.GetViewMatrix();
            // Remove translation component for skybox view
            skyboxView.M41 = 0.0f;
            skyboxView.M42 = 0.0f;
            skyboxView.M43 = 0.0f;

            // Set uniforms specific to skybox shader
            GL.UniformMatrix4(skyboxModelLocation, false, ref model);
            GL.UniformMatrix4(skyboxViewLocation, false, ref skyboxView);
            GL.UniformMatrix4(skyboxProjectionLocation, false, ref projection);

            GL.Enable(EnableCap.FramebufferSrgb);


            // Disable depth testing for the skybox render
            GL.Disable(EnableCap.DepthTest);
            skybox.Render(skyboxModelLocation, skyboxViewLocation, skyboxProjectionLocation, skyboxView, projection);
            GL.Enable(EnableCap.DepthTest); // Re-enable depth testing for main scene

            depthMapFBO.Bind();

            depthMapFBO.AttachDepthTexture(depth.ID);

            program.Bind();
            // Render the scene
            foreach (var gameObject in scene.gameObjects)
            {
                if (gameObject is Mesh mesh)
                {
                    mesh.vao.Bind();
                    mesh.vbo.Bind();
                    mesh.uvVBO.Bind();
                    mesh.normalsVBO.Bind();
                    mesh.ibo.Bind();
                    mesh.Render(modelLocation, viewLocation, projectionLocation, view, projection);
                }
            }

            // FPS calculation and buffer swap
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
