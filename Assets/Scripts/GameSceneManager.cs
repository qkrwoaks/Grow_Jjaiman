using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    /// <summary>
    /// 게임 시작 버튼을 눌렀을 때
    /// </summary>
    public static void OnGame()
    {
        SceneManager.LoadScene("LoadScene");
    }

}
