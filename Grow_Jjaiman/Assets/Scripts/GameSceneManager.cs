using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ��ư�� ������ ��
    /// </summary>
    public static void OnGame()
    {
        SceneManager.LoadScene("LoadScene");
    }

}
