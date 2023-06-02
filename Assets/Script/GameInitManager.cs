using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitManager : MonoBehaviour
{
    public string StartStage;
    public string StartPointName;
    public GameObject SpwanObject;

    FadeEffect fadeEffect;
    private GameObject player;
    private Player_Action playerAction;

    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 스폰 위치
        player = GameObject.Find("Player");
        player.transform.position = SpwanObject.transform.position;
    }

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        // ... 씬로드될때마다 계속 실행된다. 문제가 그거임 
        // 챕터1왔을때만 해야하는데 메인으로 갈때도 실행함
        // 그리고 챕터1다시 왔을때는 인스펙터로 대입한것들이 전부 Null 상태이기 때문에
        // 코드가 실행이 될수가 없음. ㅜㅜ
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*GameManager.instance.StartGame();

        //fade 효과
        fadeEffect = GameObject.Find("FadeImage").GetComponent<FadeEffect>();
        fadeEffect.OnFade(FadeState.FadeIn);

        //fade 효과시 캐릭터 멈춤
        
        playerAction = player.GetComponent<Player_Action>();
        playerAction.OnStop(PlayerState.MoveOff);

        //캐릭터 스폰 위치
        player.transform.position = SpwanObject.transform.position;

        //배고픔 피로도 초기화
        StatusManager.instance.Substitution();*/
    }

}
