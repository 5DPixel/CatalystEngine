using CatalystEngine.Models;
using CatalystEngine.ScriptsCore;

internal abstract class ScriptBase : IScript
{
    public abstract void Start();

    public void ExecuteUpdate(GameObject currentInstance)
    {
        // Check for the ApplyToName attribute on the derived class
        var attribute = (ApplyToName)Attribute.GetCustomAttribute(GetType(), typeof(ApplyToName));

        if (attribute != null && attribute.Matches(currentInstance.Name))
        {
            Update(currentInstance);
        }
    }


    protected abstract void Update(GameObject currentInstance);
}
