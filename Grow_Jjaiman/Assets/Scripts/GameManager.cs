using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

public class GameManager : MonoBehaviour
{
    [Header("Major")]
    public ulong current_major = 0; // 지금 현재 가지고 있는 전공력
    public ulong second_major = 1;  // 초마다 쌓일 전공력
    public ulong touch_major = 1;   // 터치시마다 쌓일 전공력
    public ulong total_major = 0;   // 저장시 총 전공력

    [Header("Text")]
    public TextMeshProUGUI current_major_text; // 현재 전공력 UI 텍스트
    public TextMeshProUGUI second_major_text;  // 초 전공력 UI 텍스트
    public TextMeshProUGUI touch_major_text;   // 터치 전공력 UI 텍스트

    [Header("Panel")]
    public GameObject[] MainPanel;  // 메인 패널(0, 1)   
    public GameObject SettingPanel; // 설정 패널

    [Header("Animator")]
    public Animator[] Character_Animator; // 짜이만 캐릭터 애니메이터 
    public Animator Major_Size_Up_Animator;     // 전공력 애니메이터

    [Header("GameObject")]
    public GameObject Jjaiman_character;                        // 캐릭터
    public GameObject programming_language_prefab;      // 프로그래밍 언어
    public GameObject Major;                            // 전공력 

    [Header("Transform")]
    public Transform parent;

    [Header("Sprite")]
    public Sprite[] programming_language_sprite; // 프로그래밍 언어 스프라이트

    public static GameManager gm; // 다른 클래스에서 게임매니저에 접근안하고 사용할 수 있게

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    #region 함수

    /// <summary>
    /// 전공력을 더하는 함수
    /// </summary>
    /// <param name="major">전공력</param>
    /// <param name="text">UI 텍스트</param>
    /// <returns>더한 전공력 값</returns>
    ulong AddMajor(ulong major, TextMeshProUGUI text)
    {
        string[] coinUnitArr = new string[] { "", "만 ", "억 ", "조 ", "경 ", "해 ", "자 ", "양 ", "가 ", "구 ", "간 " };
        int placeN = 4;
        BigInteger value = major;
        List<int> numList = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numList.Add((int)(value % p));
            value /= p;
        } while (value >= 1);
        string retStr = "";

        for (int i = 0; i < numList.Count; i++)
        {
            if (i > numList.Count - 4)
            {
                if (numList[i] >= 1000)
                {
                    retStr = string.Format("{0:#,0}", numList[i]) + coinUnitArr[i] + retStr;
                }
                else
                {
                    retStr = numList[i] + coinUnitArr[i] + retStr;
                }
            }
        }
        text.text = retStr + " Major";
        return major;
    }

    /// <summary>
    /// major 안에 들어가야 할 comma의 갯수를 반환 하는 함수
    /// </summary>
    /// <param name="major">비교할 major</param>
    /// <returns>콤마의 갯수</returns>
    int CountComma(ulong major)
    {
        int count = 0;
        while ((major /= 1000) > 0)
        {
            count++;
        }
        return count;
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    void Init()
    {
        AddMajor(current_major, current_major_text); // 
        AddMajor(second_major, second_major_text);
        second_major_text.text += "/초";
        AddMajor(touch_major, touch_major_text);
        touch_major_text.text += "/클릭";
    }

    /// <summary>
    /// 프로그래밍 게임 오브젝트 이동 함수
    /// </summary>
    /// <param name="num">캐릭터, 프로그래밍 게임 오브젝트 num번째의 값</param>
    /// <returns>성공 여부</returns>
    public IEnumerator arrive_programming_language()
    {
        if (Jjaiman_character.activeSelf) // num 값 번째의 character가 활성화 되어있다면
        {
            GameObject programming_language = Instantiate(programming_language_prefab, parent); // 프리팹 생성
            if (touch_major >= 10000000000000000)
            {
                programming_language.GetComponent<Image>().sprite = programming_language_sprite[4];
            }
            else if (touch_major >= 1000000000000)
            {
                programming_language.GetComponent<Image>().sprite = programming_language_sprite[3];
            }
            else if (touch_major >= 100000000)
            {
                programming_language.GetComponent<Image>().sprite = programming_language_sprite[2];
            }
            else if(touch_major >= 10000)
            {
                programming_language.GetComponent<Image>().sprite = programming_language_sprite[1];
            }
            else
            {
                programming_language.GetComponent<Image>().sprite = programming_language_sprite[0];
            }
            programming_language.transform.position = Jjaiman_character.transform.position;     // 위치 초기화
            while (programming_language.transform.position != Major.transform.position)
            {
                programming_language.transform.position = Vector2.MoveTowards(programming_language.transform.position, Major.transform.position, 10f); // 위치 변경
             
                yield return new WaitForSeconds(0.01f);
            }
            Major_Size_Up_Animator.SetTrigger("touch"); // 애니메이션 실행
            Destroy(programming_language); // 프리팹 삭제
        }
    }

    #endregion

    #region 버튼

    /// <summary>
    /// Setting Panel을 사용해야 할 때
    /// </summary>
    public void SettingButton()
    {
        if (SettingPanel.activeSelf == true)
            SettingPanel.SetActive(false);

        else if (SettingPanel.activeSelf == false)
            SettingPanel.SetActive(true);
    }

    /// <summary>
    /// Setting Panel 안의 나가기 버튼을 눌렀을 때
    /// </summary>
    public void LeaveButton()
    {
        Application.Quit(); // 실행중인 어플리케이션, 에디터 종료 함수
    }

    /// <summary>
    /// 돈을 버는 구간을 터치 했을 때
    /// </summary>
    public void TouchArea()
    {
        for (int i = 0; i < Character_Animator.Length; i++)
        {
            Character_Animator[i].SetTrigger("touch");   // 애니메이션 실행
        }
        current_major = AddMajor(current_major + touch_major, current_major_text); // 돈 추가
        StartCoroutine("arrive_programming_language"); // 이동 코루틴 함수 실행 
    }

    #endregion


}
