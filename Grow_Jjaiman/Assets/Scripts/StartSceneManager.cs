using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject Game_Explanation_Panel; // ���� ���� �ǳ�


    #region ��ư
    /// <summary>
    /// ���� ���� ��ư�� ������ ��
    /// </summary>
    public void OnStartButton()
    {
        GameSceneManager.OnGame(); // GameScene�� �ε���
    }

    /// <summary>
    /// ���� ���� �ǳ� ������ ��ư
    /// </summary>
    public void OnExitButton()
    {
        Game_Explanation_Panel.SetActive(false); // ���� ���� �ǳ� ��Ȱ��ȭ
    }

    /// <summary>
    /// ���� ���� ��ư
    /// </summary>
    public void OnGameExplanation_button()
    {
        Game_Explanation_Panel.SetActive(true); // ���� ���� �ǳ� Ȱ��ȭ
    }

    #endregion
}
