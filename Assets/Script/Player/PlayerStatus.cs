using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 재시작 때문에 넣긴 했는데 여기서 처리하는게 맞는가 모르겠음.
/*
 1. 맵 들어올 때 스텟 셋팅
 2. 박스 밀 때 스텟 감소, UI 메소드 실행
 */

static class HF_Constance
{
    public const float BOXMOVE = 1;

}


public class PlayerStatus : MonoBehaviour
{
    #region Singleton
    public static PlayerStatus instance;
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

    // player states
    private MyStates states;

    public float Hunger
    {
        set => states.Hunger = Mathf.Clamp(value, 0, states.HungerMax);
        get => states.Hunger;
    }
    public float Fatigue
    {
        set => states.Fatigue = Mathf.Clamp(value, 0, states.FatigueMax);
        get => states.Fatigue;
    }
    public float HungerMax
    {
        set => states.HungerMax = value;
        get => states.HungerMax;
    }
    public float FatigueMax
    {
        set => states.FatigueMax = value;
        get => states.FatigueMax;
    }

    void Start()
    {
        /*FatigueMax = 15f;
        HungerMax = 2f;
        Hunger = 10f;
        Fatigue = 15f;*/
    }

    public bool InitStatus(float _HungerMax, float _FatigueMax, float _Hunger, float _Fatigue)
    {
        HungerMax = _HungerMax;
        FatigueMax = _FatigueMax;
        Hunger = _Hunger;
        Fatigue = _Fatigue;

        return true;
    }

    public bool InitCurrentStatus(float _Hunger, float _Fatigue)
    {
        Hunger = _Hunger;
        Fatigue = _Fatigue;

        return true;
    }

    public bool InitHunger(float _Hunger)
    {
        Hunger = _Hunger;

        return true;
    }

    public bool InitHungerMax(float _HungerMax)
    {
        HungerMax = _HungerMax;

        return true;
    }

    public bool InitFatigue(float _Fatigue)
    {
        Fatigue = _Fatigue;

        return true;
    }
    public bool InitFatigueMax(float _FatigueMax)
    {
        FatigueMax = _FatigueMax;

        return true;
    }

    public bool OnDamageHunger(float damage)
    {
        Hunger -= damage;

        // 플레이어 배고픔 0에 도달한다면 GameOver 화면으로 가야함
        if(Hunger <= 0)
        {
            GameManager.instance.EndGame();
        }

        return true;
    }

    public bool OnDamageFatigue(float damage)
    {
        Fatigue -= damage;
        StatusManager.instance.FDChange(HF_Constance.BOXMOVE);

        // 플레이어 피로도 0에 도달한다면 현재 화면에서 재시작 하여야 함.
        if(Fatigue <= 0)
        {
            GameManager.instance.EndGame();
            /* 이곳에서 해야하는 일 */
            // 1. 박스 배치 원래대로 돌려놓기 , 이전 맵은 건들여서는 안됨
            // 2. 캐릭터 위치 초기화 하기
            // 3. 피로도 다시 부여 받기
        }

        return true;
    }

    public bool OnHealHunger(float heal)
    {
        Hunger += heal;

        if(Hunger > HungerMax)
        {
            Debug.Log("배고픔을 초과하여 heal 하였음");
            Hunger = HungerMax;
        }

        return true;
    }

    public bool OnHealFatigue(float heal)
    {
        Fatigue += heal;

        if (Fatigue > FatigueMax)
        {
            Debug.Log("피로도를 초과하여 heal 하였음");
            Fatigue = FatigueMax;
        }

        return true;
    }

}

[System.Serializable]
public struct MyStates
{
    [HideInInspector]
    public float Hunger;
    [HideInInspector]
    public float HungerMax;
    [HideInInspector]
    public float Fatigue;
    [HideInInspector]
    public float FatigueMax;
}