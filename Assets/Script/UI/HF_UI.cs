using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro

/* 수정사항 */
// UI Update문에서 하지 말고 메소드로 실행시킬 것.
// 맵마다 피로도 부여 할 것
// 스테이지 1에서 배고픔 부여 할 것
// 피로도 다 쓰면 재시작 기능 넣을 것
// 배고픔 다 쓰면 게임 오버 시킬 것

public class HF_UI : MonoBehaviour
{
    [SerializeField]
    private Slider sliderHunger;
    [SerializeField]
    private Text textHunger;
    [SerializeField]
    private Slider sliderFatigue;
    [SerializeField]
    private Text textFatigue;

    void Update()
    {
        if (sliderHunger != null)
        {
            sliderHunger.value = Utils.Percent(PlayerStatus.instance.Hunger, PlayerStatus.instance.HungerMax);
        }
        if (textHunger != null)
        {
            textHunger.text = PlayerStatus.instance.Hunger + "/" + PlayerStatus.instance.HungerMax;
        }

        if(sliderFatigue != null)
        {
            sliderFatigue.value = Utils.Percent(PlayerStatus.instance.Fatigue, PlayerStatus.instance.FatigueMax);
        }

        if(textFatigue != null)
        {
            textFatigue.text = PlayerStatus.instance.Fatigue + "/" + PlayerStatus.instance.FatigueMax;
        }

    }
}
