using UnityEngine;

public static class GameObjectExtensions
{
    public static T FindComponentAcrossChilds<T>(this GameObject obj) where T : Component
    {
        var parent = obj.transform;
        
        var component = parent.GetComponent<T>();
        
        if (component != null)
        {
            // Found the component in the current parent
            return component;
        }
        
        // Iterate through each child
        foreach (Transform child in parent)
        {
            // Recursively search in each child
            component = child.gameObject.FindComponentAcrossChilds<T>();
            
            if (component != null)
            {
                // Found the component in one of the children
                return component;
            }
        }
        
        return null;
    }

}