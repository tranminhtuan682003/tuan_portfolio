using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenDatabase", menuName = "Scriptable Objects/ScreenDatabase")]
public class ScreenDatabase : ScriptableObject
{
    [System.Serializable]
    public class ScreenData
    {
        public string nameScreen;
        public GameObject prefab;
    }

    public List<ScreenData> screens;
}
