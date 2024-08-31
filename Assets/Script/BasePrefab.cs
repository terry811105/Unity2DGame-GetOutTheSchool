using UnityEngine;

public class BasePrefab : MonoBehaviour
{

    public string prefabRootNamePrefix = "LittleGame"; // prefab根物件名稱的前綴

    public void DestroyEntirePrefab(string parentName)
    {
        GameObject prefabRoot = FindPrefabRootByName(parentName);
        if (prefabRoot != null)
        {
            Destroy(prefabRoot);
        }
        else
        {
            Debug.LogWarning("Prefab root not found. Destroying this object instead.");
            Destroy(gameObject);
        }
    }

    GameObject FindPrefabRootByName(string parentName)
    {
        Transform current = transform;
        while (current.parent != null)
        {
            if (current.name.StartsWith(parentName))
            {
                return current.gameObject;
            }
            current = current.parent;
        }
        return current.name.StartsWith(parentName) ? current.gameObject : null;
    }
}
