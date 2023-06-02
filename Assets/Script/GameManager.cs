using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    //플레이어 재시작 위치
    public string playerStartingPt;

    //현재 맵 이름
    //playerStatus와 종속되어 있음.. 초기화 안하면 피로도 부여 오류가 생김 ㅜㅜ 고쳐야함
    public string currentMapName;
    public int SceneIndex;

    //인벤토리
    public GameObject inventoryPanel;
    bool activeInventory = false;

    //게임재시작
    [HideInInspector]
    public bool isGameover;

    //게임오버 UI
    public GameObject OverUI;

    void Start()
    {
        isGameover = false;
        SceneIndex = 1;
        //inventoryPanel.SetActive(activeInventory);
    }

    void Update()
    {
        // 게임오버가 아닌 동안
        if (!isGameover)
        {
            // R 키를 눌렀을 경우 맵 재시작
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneIndex);
            }
            // I 키를 눌렀을 때 인벤토리
            if (Input.GetKeyDown(KeyCode.I))
            {
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
            }

        }
        else
        {
            
        }



    }

    // 게임오버인 경우 UI 활성화
    public void StartGame()
    {
        isGameover = false;
        OverUI.SetActive(false);
    }

    public void EndGame()
    {
        isGameover = true;
        OverUI.SetActive(true);

    }
}
