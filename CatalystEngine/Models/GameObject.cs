using CatalystEngine.Graphics;
using CatalystEngine.ScriptsCore;
using OpenTK.Mathematics;
using CatalystEngine.Components;

namespace CatalystEngine.Models
{
    internal abstract class GameObject
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
        public Texture _Texture { get; set; }
        public string physicsType { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }

        private List<IScript> _scripts = new List<IScript>();
        private static int nextID = 0;

        private List<Component> _components = new List<Component>();

        protected GameObject()
        {
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = 1f;

            ID = nextID++;
        }

        public T AddScript<T>() where T : IScript, new()
        {
            T script = new T();
            _scripts.Add(script);
            script.gameObject = this;

            return script;
        }

        public void Start()
        {
            foreach(IScript script in _scripts)
            {
                script.Start();
            }
        }

        public void Update()
        {
            foreach(IScript script in _scripts)
            {
                script.Update();
            }
        }

        public void UpdateComponents()
        {
            foreach (Component component in _components)
            {
                component.Update();
            }
        }

        public void StartComponents()
        {
            foreach (Component component in _components)
            {
                component.Start();
            }
        }

        public T GetScript<T>() where T : IScript
        {
            return (T)_scripts.Find(script => script is T);
        }

        public T AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            component.gameObject = this;
            _components.Add(component);

            return component;
        }

        public void AddComponent(Type componentType)
        {
            Component component = (Component)Activator.CreateInstance(componentType);
            component.gameObject = this;
            _components.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)_components.Find(component => component is T);
        }
        public abstract void Render(int modelLocation, int viewLocation, int projectionLocation, Matrix4 view, Matrix4 projection);
    }
}
