using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using UnityEngine.Rendering;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region Major
    [Header("Major")]
    public ulong current_major = 0; // ���� ���� ������ �ִ� ������
    public ulong second_major = 1;  // �ʸ��� ���� ������
    public ulong touch_major = 1;   // ��ġ�ø��� ���� ������
    #endregion

    #region Text
    [Header("Text")]
    public TextMeshProUGUI current_major_text;        // ���� ������ UI �ؽ�Ʈ
    public TextMeshProUGUI second_major_text;         // �� ������ UI �ؽ�Ʈ
    public TextMeshProUGUI touch_major_text;          // ��ġ ������ UI �ؽ�Ʈ
    public TextMeshProUGUI[] characterNameText;       // ĳ���� �̸� UI �ؽ�Ʈ
    public TextMeshProUGUI[] characterUpgradeText;    // ĳ���� ���׷��̵� UI �ؽ�Ʈ
    public TextMeshProUGUI[] real_estate_UpgradeText; // �ε��� ���׷��̵� UI �ؽ�Ʈ
    public TextMeshProUGUI[] FranchiseeNameText;      // ���������� �̸� �ؽ�Ʈ
    public TextMeshProUGUI[] cardText;                // ī�� �ؽ�Ʈ(0, 1)   
    public TextMeshProUGUI endcardText;               // ������ ī�� �ؽ�Ʈ
    public TextMeshProUGUI PowerSavingText;           // ���� ��� ���� �ؽ�Ʈ
    public TextMeshProUGUI StatusText;                // ���� ���� �ؽ�Ʈ

    #endregion

    #region Panel
    [Header("Panel")]
    public GameObject[] MainPanel;       // ���� �г�(0, 1)   
    public GameObject SettingPanel;      // ���� �г�
    public GameObject CharacterPanel;    // ĳ���� �г�
    public GameObject Real_estate_Panel; // �ε��� �г�
    public GameObject Franchisee_Panel;  // ������ �г�
    public GameObject CardPanel;         // ī�� �г�
    public GameObject PowerSavingPanel;  // ���� ��� �г�
    #endregion

    #region Animator
    [Header("Animator")]
    public Animator[] Character_Animator;       // ¥�̸� ĳ���� �ִϸ����� 
    public Animator Major_Size_Up_Animator;     // ������ �ִϸ�����
    #endregion

    #region GameObject
    [Header("GameObject")]
    public GameObject Jjaiman_character;                // ĳ����
    public GameObject programming_language_prefab;      // ���α׷��� ���
    public GameObject Major;                            // ������ 
    public GameObject[] character;                      // ĳ����
    public GameObject card_gameObject;                  // ī��
    public GameObject EndCard_gameObject;               // ������ ī��
    public GameObject[] Real_estate_Image_collections;  // �ε��� ����
    public GameObject[] Franchisee_Image_collections;   // ������ ����
    public GameObject[] NotSettingCollections;          // ���� not ����
    public GameObject gameStatusTextgameobject;         // ���� ����â�� ������ �ִ� ���� ������Ʈ
    #endregion

    #region Transform
    [Header("Transform")]
    public Transform parent;                            // programming prefabs�� �θ�
    #endregion

    #region Sprite
    [Header("Sprite")]
    public Sprite[] programming_language_sprite; // ���α׷��� ��� ��������Ʈ
    public Sprite[] card_sprite;                 // ī�� ��������Ʈ
    #endregion

    #region Image
    [Header("Image")]
    public Image card_image;                     // ī�� �̹���
    public Image Major_image_icon;               // ������ ������
    #endregion

    #region AudioSource
    [Header("AudioSource")]
    public AudioSource background_Audio;         // ��� �Ҹ�
    public AudioSource sound_effect_Audio;       // ȿ���� �Ҹ�
    #endregion

    #region AudioClip
    [Header("AudioClip")]
    public AudioClip background_Music_clip;      // ��� �Ҹ� Ŭ��
    public AudioClip[] sound_effect_clip;        // ȿ���� �Ҹ� Ŭ��
    #endregion

    public static GameManager gm; // �ٸ� Ŭ�������� ���ӸŴ����� ���پ��ϰ� ����� �� �ְ�

    #region story
    public int[] characterLevel = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] character_Name = new string[] { "¥�̸�", "�������", "�мŴϽ�Ÿ", "���ġ", "��ȹ��", "��ȣ��", "����", "�����", "��Ŀ", "����ũ�� ��Ƽ��Ʈ", "�丮��", "���ΰ��̸�" };
    public string[] real_estate = new string[] { "�ܵ�����", "30�� ����Ʈ", "1�� ��", "5�� ����", "10�� ����", "20�� ����", "50�� ����" };
    public bool[] real_estate_havable = new bool[] { false, false, false, false, false, false, false };
    public bool[] Franchisee_havable = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
    public string[,,] cardLine = new string[12, 9, 2]
     {
        { {"�� ���� �����...\n�� ��� �� ���Ѱ���\n�ٸ� ȸ�翡���� �̹� �ҹ��� ������\n�ٸ� ȸ�翡�� ����� �� �����...", ""}, {"", "������...������...�׸��� �̾��ϴ�...\n�̷��� �����ٰ� �� �� �� �ۿ� ���\n�� �װ� �׷� ����� �ƴ϶�� �� �˰� �־�\n�̾��ϴ�...\n�� �ʿ��� ���� �� �ִ� �� ���ιۿ� ���"}, {"�ƴϿ��� �̷��� �� ���� ��� �ֽð� �ִ°�\n�����ε� ������ ���� �� ���� ���ΰ� �ſ�\n������ ���� �ٽ� �Ͼ��\n�� ȸ�翡 ������ �ſ���\n�׷����� ���� ������ ���̴� �� �� �� ���ƿ�\n���� �Բ� ���Ͻ��� �����Ƿ���?","" }, {"","�� ������ �� �ִٸ�...."}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""} },
        { {"��� �Խ��ϴ�!!", ""},{"","�׷� �̰͸� �԰�..."}, {"����...\n�츮 ȸ���� ȫ�� ���� ���ֽǷ���?", ""},{"", "��...?\n���� ä���Ͻð� �ʹٰ��?"}, {" ��...\n���� ������ ���� ���� �� ���� ������\n���� �� �츮 ȸ���� ȫ�� ����\n�Ǿ� �ּ����� �մϴ�", ""}, {"","�� ���� ���� �ֽ��� �ʾƵ� �˴ϴ�\n�� ���� �̷�� ���ؼ����..."}, {"", ""}, {"", ""} , {"", ""}},
        { {"��!! ������ ����� ��������\n���� �Բ� ���� ������?","" },{"", "���� �Ҹ� �ϴ� �ž�\n�� �౸�� ���� ����!!"}, {"�� ���� ����� ���⼭ ��� �� �� ����\n�� �������� �پ ����� ������ �־�\n���� �Բ� ������","" },{"", "����..�� ���� �� �� ������ ��...?"}, {"������ ���� ���� ������ �Ǿ� �ٰ�!!\n���� �Բ� ������!!", ""}, {"", ""}, {"", ""} , {"", ""}, {"", ""}},
        { {"", "��~~~ �̹� ȸ�絵 Ż���ΰ� ~\n �ƹ��� ��ȹ���� ���Ƶ� �ڽ��� ����\n�����״� ��ȹ�ڴ� �����ΰ�"}, {"�� �� ��ȹ����..!\n�ű� �տ� ���!!","" },{"", "����?"},{"�� ����̿�!!\n��� �� ȸ�翡 ����ϽǷ���?", ""}, {"","��¥��?"},{"��","" },  {"","�� ���̳� ������ �ҽ��ص���?"}, {"�� ���� �Ƿ¸� ���ϴ�", ""} , {"", ""}},
        { {"", "���� �̹��� ����Ϸ��� �ϴ� �����ε�\n���� ������ �� ��� ��� ���� ��!!\n�׷� �� ����̾�"}, {"�� ���� ���̽���", ""}, {"", "�� ����� ��ȣ�� �ϰ� �ͽ��ϴ�"}, {"", ""}, {"", ""}, {"", ""} , {"", ""}, {"", ""}, {"", ""}},
        { {"", "��������\n������ �����ϰ� �ִٰ� ����µ�"}, { "��", ""}, {"", "�� �����Ϸ��� �ϴµ�\n�� �� �������?"}, {"�翬����", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} , {"", ""} },
        { {"", "ŭŭ\n���� ������� �����Ѵٰ� �ߴµ� �³�?"}, {"��", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""} },
        { {"�츮 ȸ�翡 ����ϸ� �� ������� ����.", ""},{"", "��...."}, {"���� ���׷��̵� ���ٰ�.", ""},{"", "��!!"}, {"", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} },
        { {"���� ȭ�� ��� �� �����ص帱�Կ�!\n�츮 ȸ�� ������.", ""}, {"", "��¥��?" }, {"��.", ""}, {"", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} , {"", ""} },
        { {"�� �� �丮�� ���ְڳ׿�...\n�츮 ȸ�翡 ����ϽǷ���?", ""},{"", "��... ��?"}, {"�ñ��� ���⺸�� �� �帱�Կ�.", ""}, {"", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} , {"", ""} },
        { {"�츮 ȸ�翡 ����ؼ�\n�츮 ȸ�縦 ȫ�������� ������?",""}, {"", "��..." }, {"���� ����� ���� �ɷ� ���ٲ�.", ""}, {"", ""}, {"", ""}, {"", ""} , {"", ""}, {"", ""}, {"", ""}},
        { {"��...�����غ��� ©���� ���� ���� ���� �־���...?", ""}, {"������...�ε���...������...", ""}, {"���� �� �� ����...", ""}, {"�����غ���...�� ���� ������...?", ""}, {"�ǹ���...?\n���� ü���� ����..?\n�ƴ�...������ ���� �����ڿ���...", ""}, {"�����غ���...\n�� ȸ��...���� ���Ի���� �����ϴ���...\n�����̾��� �׶��� �׸���...", ""}, {"�ƴ�!!!\n�ٽ�...�� ȸ�翡 ����ؼ�...\n�����̾���...�׶��� ���ư��� �ž�..!", ""}, {"�׶��� �޶��� �� ����� �����ְھ�!!!", ""}, {"", ""} }
     };
    public string[] endcardLine = new string[11] { "", "", "", "", "", "", "", "", "", "", "" };
    int[] current_card_turn = new int[4];

    #endregion

    private void Awake()
    {

        DontDestroyOnLoad(this);
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        DatabaseManager.Instance.Load();
        SetData();
        Init();
        Character_Init();
        Character_Panel_Init();
        Real_estate_Init();
        Franchisee_init();
        StartCoroutine("SecondAddMajor");
        background_Audio.clip = background_Music_clip;
        background_Audio.Play();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        OnDemandRendering.renderFrameInterval = 3;
    }

    public void SetData()
    {
        current_major = DatabaseManager.Instance.playerData.money;
        second_major = DatabaseManager.Instance.playerData.secondMoney;
        touch_major = DatabaseManager.Instance.playerData.touchMoney;

        int i = 0;

        foreach (var item in DatabaseManager.Instance.playerData.charLevel.Values)
        {
            characterLevel[i] = item;
            if (item > 0)
            {
                character[i].SetActive(true);
            }
            i++;
        }

        i = 0;

        foreach (var item in DatabaseManager.Instance.playerData.realEstate.Values)
        {
            real_estate_havable[i] = item;
            i++;
        }

        i = 0;
        foreach (var item in DatabaseManager.Instance.playerData.frenchaisee.Values)
        {
            Franchisee_havable[i] = item;
            i++;
        }
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
    /// �ʸ��� ���ϴ� �Լ�
    /// </summary>
    /// <returns>1�� ���</returns>
    IEnumerator SecondAddMajor()
    {
        while (true)
        {
            if (second_major != 0)
            {
                current_major += second_major;
                Init();
                yield return new WaitForSeconds(1f);
            }
        }
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
        AddMajor(current_major, current_major_text);
        AddMajor(second_major, second_major_text);
        second_major_text.text += "/��";
        AddMajor(touch_major, touch_major_text);
        touch_major_text.text += "/Ŭ��";
    }

    /// <summary>
    /// ����� �Ҹ� �÷��� �Լ�
    /// </summary>
    /// <param name="source">����� ����� �ҽ�</param>
    /// <param name="clip">����� ����� Ŭ��</param>
    public void AudioPlay(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
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
            if (!NotSettingCollections[1].activeSelf)
            {
                GameObject programming_language = Instantiate(programming_language_prefab, parent); // ������ ����
                if (touch_major >= 75000)
                {
                    programming_language.GetComponent<Image>().sprite = programming_language_sprite[4];
                    if (NotSettingCollections[1].activeSelf)
                        sound_effect_Audio.clip = sound_effect_clip[4];
                }
                else if (touch_major >= 62500)
                {
                    programming_language.GetComponent<Image>().sprite = programming_language_sprite[3];
                    if (NotSettingCollections[1].activeSelf)
                        sound_effect_Audio.clip = sound_effect_clip[3];
                }
                else if (touch_major >= 50000)
                {
                    programming_language.GetComponent<Image>().sprite = programming_language_sprite[2];
                    if (NotSettingCollections[1].activeSelf)
                        sound_effect_Audio.clip = sound_effect_clip[2];
                }
                else if (touch_major >= 37500)
                {
                    programming_language.GetComponent<Image>().sprite = programming_language_sprite[1];
                    if (!NotSettingCollections[1].activeSelf)
                        sound_effect_Audio.clip = sound_effect_clip[1];
                }
                else
                {
                    programming_language.GetComponent<Image>().sprite = programming_language_sprite[0];
                    sound_effect_Audio.clip = sound_effect_clip[0];
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
    }

    /// <summary>
    /// ĳ���� �ʱ�ȭ �Լ�
    /// </summary>
    void Character_Init()
    {
        for (int i = 0; i < 12; i++)
        {
            if (characterLevel[i] == 0)
            {
                character[i].SetActive(false);
            }
            else
            {
                character[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// ĳ���� �ǳ� �ʱ�ȭ �Լ�
    /// </summary>
    void Character_Panel_Init()
    {
        for (int i = 0; i < 12; i++)
        {
            characterNameText[i].text = character_Name[i] + " Lv" + characterLevel[i].ToString();
            characterUpgradeText[i].text = "���: " + string.Format("{0:#,0}", (i + 1) * 2500 * characterLevel[i] + "\n +" + string.Format("{0:#,0}", ((i + 1) * 2500 * characterLevel[i]) / 2)) + " Ŭ��";
            if (characterLevel[i] == 0)
            {
                if (characterLevel[i - 1] < 10)
                {
                    characterNameText[i].text = character_Name[i] + " ��� " + "(���� : " + character_Name[i - 1] + " 10����)";
                }
            }
            if (characterLevel[i] >= 30)
            {
                characterUpgradeText[i].text = "MAX LEVEL";
            }
        }
    }

    /// <summary>
    /// �ε��� �ʱ�ȭ �Լ�
    /// </summary>
    void Real_estate_Init()
    {
        for (int i = 0; i < 7; i++)
        {
            if (!real_estate_havable[i])
            {
                real_estate_UpgradeText[i].text = "���: " + string.Format("{0:#,0}", (i + 1) * 20000000) + "\n +" + string.Format("{0:#,0}", ((i + 1) * 200000)) + " �ʴ�";
            }
            else
            {
                real_estate_UpgradeText[i].text = "���� �Ϸ�!";
            }
        }
    }

    /// <summary>
    /// ������ �ʱ�ȭ �Լ�
    /// </summary>
    void Franchisee_init()
    {
        for (int i = 0; i < 11; i++)
        {
            if (!Franchisee_havable[i])
            {
                FranchiseeNameText[i].text = "���: " + string.Format("{0:#,0}", (i + 1) * 30000000) + "\n +" + string.Format("{0:#,0}", (i + 1) * 300000) + " �ʴ�";
            }
            else
            {
                FranchiseeNameText[i].text = "���� �Ϸ�!";
            }
        }
    }

    /// <summary>
    /// ī�� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="num">������ ĳ���� ����</param>
    /// <param name="card_turn">ī���� ����</param>
    void Card_Init(int num, int card_turn)
    {
        current_card_turn[0] = num;
        current_card_turn[1] = card_turn;
        card_image.sprite = card_sprite[num];
        if (current_card_turn[0] == 0 && current_card_turn[1] > 1)
        {
            card_image.sprite = card_sprite[11];
        }
        else if (current_card_turn[0] == 2)
        {
            if (current_card_turn[1] == 1)
            {
                card_image.sprite = card_sprite[12];
            }
            else if (current_card_turn[1] >= 3)
            {
                card_image.sprite = card_sprite[13];
            }
        }
        else if (current_card_turn[0] == 3)
        {
            if (current_card_turn[1] > 1)
            {
                card_image.sprite = card_sprite[14];
            }
            if (current_card_turn[1] > 3)
            {
                card_image.sprite = card_sprite[15];
            }
        }
        else if (current_card_turn[0] == 4 && current_card_turn[1] >= 1)
        {
            card_image.sprite = card_sprite[16];
        }
        else if (current_card_turn[0] == 10 && current_card_turn[1] == 2)
        {
            card_image.sprite = card_sprite[17];
        }
        if (current_card_turn[1] != -1)
        {
            cardText[0].text = cardLine[num, card_turn, 0];
            cardText[1].text = cardLine[num, card_turn, 1];
        }
    }

    /// <summary>
    /// �� ī�� �ʱ�ȭ
    /// </summary>
    /// <param name="num">������ ĳ���� ����</param>
    public void EndCard_Init(int num)
    {
        endcardText.text = character_Name[num + 1] + "��(��)\n¥�̸��� ȸ�翡\n��� �Ǿ���.";
    }

    /// <summary>
    /// ������ ����Ʈ �ʱ�ȭ
    /// </summary>
    public void MajorEffectInit()
    {
        if (touch_major >= 75000)
        {
            Major_image_icon.sprite = programming_language_sprite[4];
        }
        else if (touch_major >= 62500)
        {
            Major_image_icon.sprite = programming_language_sprite[3];
        }
        else if (touch_major >= 50000)
        {
            Major_image_icon.sprite = programming_language_sprite[2];
        }
        else if (touch_major >= 37500)
        {
            Major_image_icon.sprite = programming_language_sprite[1];
        }
        else
        {
            Major_image_icon.sprite = programming_language_sprite[0];
        }
    }

    public void SettingInit(GameObject GO)
    {
        if (GO.activeSelf)
        {
            GO.SetActive(false);
        }
        else
        {
            GO.SetActive(true);
        }
    }

    public void EnableGameStautsText()
    {
        gameStatusTextgameobject.SetActive(false);
        StatusText.text = "";
    }

    public void PowerSavingInit()
    {
        if (PowerSavingPanel.activeSelf)
        {
            OnDemandRendering.renderFrameInterval = 3;
            PowerSavingPanel.SetActive(false);
            PowerSavingText.text = "";
        }
        else
        {
            OnDemandRendering.renderFrameInterval = 1;
            PowerSavingPanel.SetActive(true);
            PowerSavingText.text = "���� ���� ���Դϴ�.\n�ٽ� ��ġ�� ������尡 �����˴ϴ�.";
            SettingInit(NotSettingCollections[7]);
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
        {
            if (NotSettingCollections[0].activeSelf)
                sound_effect_Audio.clip = sound_effect_clip[5];
            sound_effect_Audio.Play();
            SettingPanel.SetActive(false);
        }

        else if (SettingPanel.activeSelf == false)
        {
            if (NotSettingCollections[0].activeSelf)
                sound_effect_Audio.clip = sound_effect_clip[5];
            sound_effect_Audio.Play();
            SettingPanel.SetActive(true);
        }
    }

    /// <summary>
    /// ȿ���� ��ư
    /// </summary>
    public void SoundEffectButton()
    {
        SettingInit(NotSettingCollections[0]);
    }

    /// <summary>
    /// ������ ȿ�� ��ư
    /// </summary>
    public void MajorEffectButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        SettingInit(NotSettingCollections[1]);
    }

    /// <summary>
    /// �������� ��ư
    /// </summary>
    public void WebPageButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "9�� �ν��� �ִ� ����Ϳ� �ֽ��ϴ�.";
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// ��� ��ư
    /// </summary>
    public void NotionButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Application.OpenURL("https://zest-sandpaper-2a9.notion.site/c718986933634f999783d34c7934cb62");
    }

    /// <summary>
    /// GSM ��ư
    /// </summary>
    public void GSMButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "���� ����Ʈ���� ���̽��� ����б� ����Ʈ�� �̵��մϴ�.";
        Application.OpenURL("http://gsm.gen.hs.kr/main/main.php");
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// ���ڵ� ��ư
    /// </summary>
    public void DiscordButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "���ڵ� ��ũ�� ���ϴ�.";
        Application.OpenURL("https://discord.gg/rF2EJJKz");
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// ����� ��ư
    /// </summary>
    public void GithubButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "����� ��ũ�� �����մϴ�.";
        Application.OpenURL("https://github.com/qkrwoaks/Grow_Jjaiman");
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// ������� ��ư
    /// </summary>
    public void OnPowerSavingModeButton()
    {
        if (!NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        SettingInit(NotSettingCollections[7]);
        PowerSavingInit();
    }


    /// <summary>
    /// �ʱ�ȭ ��ư
    /// </summary>
    public void InitButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "���� �ʱ�ȭ �����\nPc�� ��� �� ��ġ\n�޴����� ��� ������ ������ ���ֽñ� �ٶ��ϴ�.";
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// Setting Panel ���� ������ ��ư�� ������ ��
    /// </summary>
    public void LeaveButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "������ �����մϴ�.";
        Application.Quit(); // �������� ���ø����̼�, ������ ���� �Լ�
        Invoke("EnableGameStautsText", 3f);
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

    /// <summary>
    /// ĳ���� ��ư�� ������ ��
    /// </summary>
    public void Character_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        CharacterPanel.SetActive(true);
        Character_Panel_Init();
    }

    /// <summary>
    /// ĳ���� �г� ������ ��ư
    /// </summary>
    public void Character_Exit_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        CharacterPanel.SetActive(false);
    }

    /// <summary>
    /// ĳ���� ���׷��̵� ��ư
    /// </summary>
    /// <param name="num">��ȣ</param>
    public void Character_Upgrade_Button(int num)
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        int cost = (num + 1) * 2500 * characterLevel[num];
        if (current_major >= (ulong)cost && characterLevel[num] < 30)
        {
            if (num != 0)
            {
                if (characterLevel[num - 1] >= 10)
                {
                    if (characterLevel[num] == 0)
                    {
                        CardPanel.SetActive(true);
                        Card_Init(num - 1, 0);
                    }
                    current_major -= (ulong)cost;
                    characterLevel[num]++;
                    touch_major += (ulong)cost / 20;
                    second_major += (ulong)cost / 1000;
                    Character_Panel_Init();
                    Character_Init();
                    Init();
                }
            }
            else
            {
                if (characterLevel[num] == 0)
                {
                    CardPanel.SetActive(true);
                    Card_Init(num - 1, 0);
                }
                current_major -= (ulong)cost;
                characterLevel[num]++;
                touch_major += (ulong)cost / 20;
                second_major += (ulong)cost / 1000;
                Character_Panel_Init();
                Character_Init();
                Init();
            }
        }
    }

    /// <summary>
    /// �ε��� �г� ��ư
    /// </summary>
    public void Real_estate_Panel_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Real_estate_Init();
        Real_estate_Panel.SetActive(true);
    }

    /// <summary>
    /// �ε��� �г� ������ ��ư
    /// </summary>
    public void Real_estate_Panel_Exit_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Real_estate_Panel.SetActive(false);
    }

    /// <summary>
    /// �ε��� ���׷��̵� ��ư
    /// </summary>
    /// <param name="num">Ŭ���� �ε��� ����</param>
    public void Real_estate_Upgrade_Button(int num)
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[7];
        sound_effect_Audio.Play();
        int cost = (num + 1) * 20000000;
        if (current_major >= (ulong)cost && !real_estate_havable[num])
        {
            real_estate_havable[num] = true;
            current_major -= (ulong)cost;
            second_major += (ulong)cost / 100;
            Real_estate_Image_collections[num].SetActive(true);
            Real_estate_Init();
            Init();
        }
    }

    /// <summary>
    /// ������ �г� ��ư
    /// </summary>
    public void Franchisee_Panel_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Franchisee_init();
        Franchisee_Panel.SetActive(true);
    }

    /// <summary>
    /// ������ ������ ��ư
    /// </summary>
    public void Franchisee_Panel_Exit_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Franchisee_Panel.SetActive(false);
    }

    /// <summary>
    /// ������ ���׷��̵� ��ư
    /// </summary>
    /// <param name="num">Ŭ���� ������ ����</param>
    public void Franchisee_Panel_Upgrade_Button(int num)
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[7];
        sound_effect_Audio.Play();
        int cost = (num + 1) * 30000000;
        if (current_major >= (ulong)cost && !Franchisee_havable[num])
        {
            Franchisee_havable[num] = true;
            current_major -= (ulong)cost;
            second_major += (ulong)cost / 100;
            Franchisee_Image_collections[num].SetActive(true);
            Franchisee_init();
            Init();
        }
    }

    /// <summary>
    /// ī�� ��ư(��, ��)
    /// </summary>
    /// <param name="isNext">���� ��ư ���� Ȯ���ϴ� ����</param>
    public void CardButton(bool isNext)
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[6];
        sound_effect_Audio.Play();
        if (isNext)
        {
            if (current_card_turn[0] <= 10)
            {
                if (!cardLine[current_card_turn[0], current_card_turn[1] + 1, 0].Equals("") || !cardLine[current_card_turn[0], current_card_turn[1] + 1, 1].Equals(""))
                {
                    Card_Init(current_card_turn[0], current_card_turn[1] + 1);
                }
                else if (cardLine[current_card_turn[0], current_card_turn[1] + 1, 0].Equals("") && cardLine[current_card_turn[0], current_card_turn[1] + 1, 1].Equals(""))
                {
                    card_gameObject.SetActive(false);
                    EndCard_gameObject.SetActive(true);
                    EndCard_Init(current_card_turn[0]);
                }
            }
        }
        else
        {
            if (current_card_turn[1] != 0)
            {
                Card_Init(current_card_turn[0], current_card_turn[1] - 1);
            }
        }
    }

    /// <summary>
    /// ī�� ��ŵ ��ư
    /// </summary>
    public void CardSkipButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[6];
        sound_effect_Audio.Play();
        card_gameObject.SetActive(false);
        EndCard_gameObject.SetActive(true);
        EndCard_Init(current_card_turn[0]);
    }

    /// <summary>
    /// �� ī�� ������ ��ư
    /// </summary>
    public void EndCardExitButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[7];
        sound_effect_Audio.Play();
        CardPanel.SetActive(false);
        EndCard_gameObject.SetActive(false);
        card_gameObject.SetActive(true);
    }

    #endregion

}
