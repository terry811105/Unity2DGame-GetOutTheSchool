using UnityEngine;

public class BasePrefab : MonoBehaviour
{

    public string prefabRootNamePrefix = "LittleGame"; // prefab根物件名稱的前綴

    public void DestroyEntirePrefab()
    {
        GameObject prefabRoot = FindPrefabRootByName();
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

    GameObject FindPrefabRootByName()
    {
        Transform current = transform;
        while (current.parent != null)
        {
            if (current.name.StartsWith(prefabRootNamePrefix))
            {
                return current.gameObject;
            }
            current = current.parent;
        }
        return current.name.StartsWith(prefabRootNamePrefix) ? current.gameObject : null;
    }
}
