using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject Game_Explanation_Panel; // 게임 설명 판넬


    #region 버튼
    /// <summary>
    /// 게임 시작 버튼을 눌렀을 때
    /// </summary>
    public void OnStartButton()
    {
        GameSceneManager.OnGame(); // GameScene을 로드함
    }

    /// <summary>
    /// 게임 설명 판넬 나가기 버튼
    /// </summary>
    public void OnExitButton()
    {
        Game_Explanation_Panel.SetActive(false); // 게임 설명 판넬 비활성화
    }

    /// <summary>
    /// 게임 설명 버튼
    /// </summary>
    public void OnGameExplanation_button()
    {
        Game_Explanation_Panel.SetActive(true); // 게임 설명 판넬 활성화
    }

    #endregion
}
