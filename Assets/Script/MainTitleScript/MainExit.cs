using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainExit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        //System.Diagnostics.Process.GetCurrentProcess().Kill(); 아니 유니티도 꺼지면 어카냐고 ㅋㅋ
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
