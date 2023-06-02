using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour

    // 타이틀 화면 NewGame 버튼의 스크립트
{   public void InGame()
    {
        SceneManager.LoadScene(1);
    }
}
