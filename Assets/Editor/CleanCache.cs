using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// An editor script which deletes cache info.
///
/// NOTE:
///     Delete all AssetBundle content that has been cached by the current application.
///     This function is not available to WebPlayer applications that use the shared cache.
///
/// See Also:
///     http://docs.unity3d.com/Documentation/ScriptReference/Caching.CleanCache.html
///
/// </summary>
public class CacheTools : ScriptableObject
{
    [MenuItem("Tools/Cache/Delete PlayerPrefs")]
    public static void PlayerPrefsDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Tools/Cache/Clean")]
    public static void CleanCache()
    {
        if (Caching.ClearCache())
        {
            Debug.LogWarning("Successfully cleaned all caches.");
        }
        else
        {
            Debug.LogWarning("Cache was in use.");
        }
    }
}