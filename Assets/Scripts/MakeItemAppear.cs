using System.Diagnostics;
using UnityEngine;

public class MakeItemAppear : MonoBehaviour
{
    // Finds a GameObject by name (including inactive scene objects) and sets it active.
    // Returns true if an object was found and activated.
    public static bool ShowByName(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;

        // 1) Fast path: active objects
        GameObject go = GameObject.Find(name);
        if (go != null)
        {
            go.SetActive(true);
            return true;
        }

        // 2) Fallback: search all GameObjects (includes inactive). Filter to scene objects.
        var all = Resources.FindObjectsOfTypeAll<GameObject>();
        for (int i = 0; i < all.Length; i++)
        {
            var candidate = all[i];
            if (candidate == null) continue;
            if (candidate.name != name) continue;
            // Only consider scene instances (ignore assets/prefabs)
            if (!candidate.scene.IsValid()) continue;

            candidate.SetActive(true);
            return true;
        }
        return false;
    }
}