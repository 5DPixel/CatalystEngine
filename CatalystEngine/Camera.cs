using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalystEngine
{
    internal class Camera
    {
        //CONSTANTS
        private float speed = 8f;
        private float screenWidth;
        private float screenHeight;
        private float sensitivity = 180f;
        private float fov = 60.0f;

        //position variables
        public Vector3 Position;

        Vector3 up = Vector3.UnitY;
        Vector3 front = -Vector3.UnitZ;
        Vector3 right = Vector3.UnitX;

        // view rotations
        public float pitch;
        public float yaw;
        private bool isFreeCamera;

        private bool firstMove = true;
        public Vector2 lastPosition;
        public Camera(float width, float height, Vector3 position, bool isFreeCamera, float pitch, float yaw) {
            screenWidth = width;
            screenHeight = height;
            this.Position = position;
            this.pitch = pitch;
            this.yaw = yaw;
            this.isFreeCamera = isFreeCamera;
        }
        public Matrix4 GetViewMatrix() {
            return Matrix4.LookAt(Position, Position + front, up);
        }
        public Matrix4 GetProjectionMatrix() {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), screenWidth / screenHeight, 0.1f, 100f);
        }

        public void UpdateVectors()
        {
            if(pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            if(pitch < -89.0f)
            {
                pitch = -89.0f;
            }
            front.X = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Sin(MathHelper.DegreesToRadians(yaw));

            front = Vector3.Normalize(front);
            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }

        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e) {
            if (input.IsKeyDown(Keys.W))
            {
                Position += front * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                Position -= right * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                Position -= front * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                Position += right * speed * (float)e.Time;
            }

            if (input.IsKeyDown(Keys.E))
            {
                Position.Y += speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.Q))
            {
                Position.Y -= speed * (float)e.Time;
            }

            if (firstMove)
            {
                lastPosition = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            } else
            {
                var deltaX = mouse.X - lastPosition.X;
                var deltaY = mouse.Y - lastPosition.Y;
                lastPosition = new Vector2(mouse.X, mouse.Y);

                yaw += deltaX * sensitivity * (float)e.Time;
                pitch -= deltaY * sensitivity * (float)e.Time;
            }
            UpdateVectors();
        }

        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            if (isFreeCamera)
            {
                InputController(input, mouse, e);
            }
        }
    }
}
