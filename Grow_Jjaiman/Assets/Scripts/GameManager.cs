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
    public ulong current_major = 0; // ���� ���� ������ �ִ� ������
    public ulong second_major = 1;  // �ʸ��� ���� ������
    public ulong touch_major = 1;   // ��ġ�ø��� ���� ������
    public ulong total_major = 0;   // ����� �� ������

    [Header("Text")]
    public TextMeshProUGUI current_major_text; // ���� ������ UI �ؽ�Ʈ
    public TextMeshProUGUI second_major_text;  // �� ������ UI �ؽ�Ʈ
    public TextMeshProUGUI touch_major_text;   // ��ġ ������ UI �ؽ�Ʈ

    [Header("Panel")]
    public GameObject[] MainPanel;  // ���� �г�(0, 1)   
    public GameObject SettingPanel; // ���� �г�

    [Header("Animator")]
    public Animator[] Character_Animator; // ¥�̸� ĳ���� �ִϸ����� 
    public Animator Major_Size_Up_Animator;     // ������ �ִϸ�����

    [Header("GameObject")]
    public GameObject Jjaiman_character;                        // ĳ����
    public GameObject programming_language_prefab;      // ���α׷��� ���
    public GameObject Major;                            // ������ 

    [Header("Transform")]
    public Transform parent;

    [Header("Sprite")]
    public Sprite[] programming_language_sprite; // ���α׷��� ��� ��������Ʈ

    public static GameManager gm; // �ٸ� Ŭ�������� ���ӸŴ����� ���پ��ϰ� ����� �� �ְ�

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    #region �Լ�

    /// <summary>
    /// �������� ���ϴ� �Լ�
    /// </summary>
    /// <param name="major">������</param>
    /// <param name="text">UI �ؽ�Ʈ</param>
    /// <returns>���� ������ ��</returns>
    ulong AddMajor(ulong major, TextMeshProUGUI text)
    {
        string[] coinUnitArr = new string[] { "", "�� ", "�� ", "�� ", "�� ", "�� ", "�� ", "�� ", "�� ", "�� ", "�� " };
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
    /// major �ȿ� ���� �� comma�� ������ ��ȯ �ϴ� �Լ�
    /// </summary>
    /// <param name="major">���� major</param>
    /// <returns>�޸��� ����</returns>
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
    /// �ʱ�ȭ �Լ�
    /// </summary>
    void Init()
    {
        AddMajor(current_major, current_major_text); // 
        AddMajor(second_major, second_major_text);
        second_major_text.text += "/��";
        AddMajor(touch_major, touch_major_text);
        touch_major_text.text += "/Ŭ��";
    }

    /// <summary>
    /// ���α׷��� ���� ������Ʈ �̵� �Լ�
    /// </summary>
    /// <param name="num">ĳ����, ���α׷��� ���� ������Ʈ num��°�� ��</param>
    /// <returns>���� ����</returns>
    public IEnumerator arrive_programming_language()
    {
        if (Jjaiman_character.activeSelf) // num �� ��°�� character�� Ȱ��ȭ �Ǿ��ִٸ�
        {
            GameObject programming_language = Instantiate(programming_language_prefab, parent); // ������ ����
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
            programming_language.transform.position = Jjaiman_character.transform.position;     // ��ġ �ʱ�ȭ
            while (programming_language.transform.position != Major.transform.position)
            {
                programming_language.transform.position = Vector2.MoveTowards(programming_language.transform.position, Major.transform.position, 10f); // ��ġ ����
             
                yield return new WaitForSeconds(0.01f);
            }
            Major_Size_Up_Animator.SetTrigger("touch"); // �ִϸ��̼� ����
            Destroy(programming_language); // ������ ����
        }
    }

    #endregion

    #region ��ư

    /// <summary>
    /// Setting Panel�� ����ؾ� �� ��
    /// </summary>
    public void SettingButton()
    {
        if (SettingPanel.activeSelf == true)
            SettingPanel.SetActive(false);

        else if (SettingPanel.activeSelf == false)
            SettingPanel.SetActive(true);
    }

    /// <summary>
    /// Setting Panel ���� ������ ��ư�� ������ ��
    /// </summary>
    public void LeaveButton()
    {
        Application.Quit(); // �������� ���ø����̼�, ������ ���� �Լ�
    }

    /// <summary>
    /// ���� ���� ������ ��ġ ���� ��
    /// </summary>
    public void TouchArea()
    {
        for (int i = 0; i < Character_Animator.Length; i++)
        {
            Character_Animator[i].SetTrigger("touch");   // �ִϸ��̼� ����
        }
        current_major = AddMajor(current_major + touch_major, current_major_text); // �� �߰�
        StartCoroutine("arrive_programming_language"); // �̵� �ڷ�ƾ �Լ� ���� 
    }

    #endregion


}
