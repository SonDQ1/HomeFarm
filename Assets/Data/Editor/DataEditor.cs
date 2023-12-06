using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DataEditor  {
    [MenuItem("Data/Data/NongSan")]
    public static void DetailSeeds()
    {
        DataNongSan seed = ScriptableObject.CreateInstance<DataNongSan>();
        AssetDatabase.CreateAsset(seed, "Assets/Data/Data/NongSan.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = seed;
    }
    [MenuItem("Data/Data/Trangtri")]
    public static void DetailSeeds1()
    {
        DataTrangtri seed = ScriptableObject.CreateInstance<DataTrangtri>();
        AssetDatabase.CreateAsset(seed, "Assets/Data/Data/Trangtri.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = seed;
    }

    [MenuItem("Data/Data/Thanhtich")]
    public static void DetailSeeds2()
    {
        DataThanhtich seed = ScriptableObject.CreateInstance<DataThanhtich>();
        AssetDatabase.CreateAsset(seed, "Assets/Data/Data/Thanhtich.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = seed;
    }
    [MenuItem("Data/Data/Level")]
    public static void DetailSeeds3()
    {
        DataLevel seed = ScriptableObject.CreateInstance<DataLevel>();
        AssetDatabase.CreateAsset(seed, "Assets/Data/Data/Level.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = seed;
    }
    
}
