using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour
{
    #region Singleton
    public static MapDictionary instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public Dictionary<string, string> dict;

    private void Start()
    {
        dict = new Dictionary<string, string>();
        dict.Add("PS1-1", "CameraPos1");
        dict.Add("PS1-2", "CameraPos2");
        dict.Add("PS1-3", "CameraPos3");
        dict.Add("PS1-4", "CameraPos4");

        dict.Add("PE1-1", "CameraPos1");
        dict.Add("PE1-2", "CameraPos2");
        dict.Add("PE1-3", "CameraPos3");
        dict.Add("PE1-4", "CameraPos4");
    }
}
