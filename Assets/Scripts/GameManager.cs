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
    public ulong current_major = 0; // 지금 현재 가지고 있는 전공력
    public ulong second_major = 1;  // 초마다 쌓일 전공력
    public ulong touch_major = 1;   // 터치시마다 쌓일 전공력
    #endregion

    #region Text
    [Header("Text")]
    public TextMeshProUGUI current_major_text;        // 현재 전공력 UI 텍스트
    public TextMeshProUGUI second_major_text;         // 초 전공력 UI 텍스트
    public TextMeshProUGUI touch_major_text;          // 터치 전공력 UI 텍스트
    public TextMeshProUGUI[] characterNameText;       // 캐릭터 이름 UI 텍스트
    public TextMeshProUGUI[] characterUpgradeText;    // 캐릭터 업그레이드 UI 텍스트
    public TextMeshProUGUI[] real_estate_UpgradeText; // 부동산 업그레이드 UI 텍스트
    public TextMeshProUGUI[] FranchiseeNameText;      // 프랜차이즈 이름 텍스트
    public TextMeshProUGUI[] cardText;                // 카드 텍스트(0, 1)   
    public TextMeshProUGUI endcardText;               // 마지막 카드 텍스트
    public TextMeshProUGUI PowerSavingText;           // 절전 모드 상태 텍스트
    public TextMeshProUGUI StatusText;                // 게임 상태 텍스트

    #endregion

    #region Panel
    [Header("Panel")]
    public GameObject[] MainPanel;       // 메인 패널(0, 1)   
    public GameObject SettingPanel;      // 설정 패널
    public GameObject CharacterPanel;    // 캐릭터 패널
    public GameObject Real_estate_Panel; // 부동산 패널
    public GameObject Franchisee_Panel;  // 가맹점 패널
    public GameObject CardPanel;         // 카드 패널
    public GameObject PowerSavingPanel;  // 절전 모드 패널
    #endregion

    #region Animator
    [Header("Animator")]
    public Animator[] Character_Animator;       // 짜이만 캐릭터 애니메이터 
    public Animator Major_Size_Up_Animator;     // 전공력 애니메이터
    #endregion

    #region GameObject
    [Header("GameObject")]
    public GameObject Jjaiman_character;                // 캐릭터
    public GameObject programming_language_prefab;      // 프로그래밍 언어
    public GameObject Major;                            // 전공력 
    public GameObject[] character;                      // 캐릭터
    public GameObject card_gameObject;                  // 카드
    public GameObject EndCard_gameObject;               // 마지막 카드
    public GameObject[] Real_estate_Image_collections;  // 부동산 모음
    public GameObject[] Franchisee_Image_collections;   // 가맹점 모음
    public GameObject[] NotSettingCollections;          // 설정 not 모음
    public GameObject gameStatusTextgameobject;         // 게임 상태창을 가지고 있는 게임 오브젝트
    #endregion

    #region Transform
    [Header("Transform")]
    public Transform parent;                            // programming prefabs의 부모
    #endregion

    #region Sprite
    [Header("Sprite")]
    public Sprite[] programming_language_sprite; // 프로그래밍 언어 스프라이트
    public Sprite[] card_sprite;                 // 카드 스프라이트
    #endregion

    #region Image
    [Header("Image")]
    public Image card_image;                     // 카드 이미지
    public Image Major_image_icon;               // 전공력 아이콘
    #endregion

    #region AudioSource
    [Header("AudioSource")]
    public AudioSource background_Audio;         // 배경 소리
    public AudioSource sound_effect_Audio;       // 효과음 소리
    #endregion

    #region AudioClip
    [Header("AudioClip")]
    public AudioClip background_Music_clip;      // 배경 소리 클립
    public AudioClip[] sound_effect_clip;        // 효과음 소리 클립
    #endregion

    public static GameManager gm; // 다른 클래스에서 게임매니저에 접근안하고 사용할 수 있게

    #region story
    public int[] characterLevel = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] character_Name = new string[] { "짜이만", "밥아저씨", "패셔니스타", "양아치", "기획자", "경호원", "상담원", "정비공", "해커", "메이크업 아티스트", "요리사", "프로게이머" };
    public string[] real_estate = new string[] { "단독주택", "30평 아파트", "1층 상가", "5층 빌딩", "10층 빌딩", "20층 빌딩", "50층 빌딩" };
    public bool[] real_estate_havable = new bool[] { false, false, false, false, false, false, false };
    public bool[] Franchisee_havable = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
    public string[,,] cardLine = new string[12, 9, 2]
     {
        { {"나 이제 어떡하죠...\n나 모든 걸 빼앗겼어요\n다른 회사에서도 이미 소문이 퍼져서\n다른 회사에도 취업할 수 없어요...", ""}, {"", "괜찮다...괜찮아...그리고 미안하다...\n이렇게 괜찮다고 만 할 수 밖에 없어서\n난 네가 그럴 사람이 아니라고 잘 알고 있어\n미안하다...\n난 너에게 해줄 수 있는 게 위로밖에 없어서"}, {"아니에요 이렇게 제 말을 들어 주시고 있는것\n만으로도 재편이 생긴 것 같아 위로가 돼요\n아저씨 저는 다시 일어나서\n그 회사에 복수할 거예요\n그렇지만 저는 아저씨 없이는 못 할 것 같아요\n저와 함께 일하시지 않으실래요?","" }, {"","널 도와줄 수 있다면...."}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""} },
        { {"배달 왔습니다!!", ""},{"","그래 이것만 먹고..."}, {"저기...\n우리 회사의 홍보 모델이 돼주실래요?", ""},{"", "네...?\n저를 채용하시고 싶다고요?"}, {" 네...\n제가 당장은 돈을 많이 줄 수는 없지만\n저는 꼭 우리 회사의 홍보 모델이\n되어 주셨으면 합니다", ""}, {"","네 당장 돈을 주시지 않아도 됩니다\n제 꿈을 이루기 위해서라면..."}, {"", ""}, {"", ""} , {"", ""}},
        { {"너!! 굉장한 재능을 가졌구나\n나와 함께 하지 않을래?","" },{"", "무슨 소리 하는 거야\n난 축구할 꺼야 꺼져!!"}, {"난 너의 재능을 여기서 썩게 할 수 없어\n넌 누구보다 뛰어난 재능을 가지고 있어\n나와 함께 일하자","" },{"", "저정..말 나도 될 수 이있을 까...?"}, {"걱정마 내가 너의 날개가 되어 줄게!!\n나와 함께 일하자!!", ""}, {"", ""}, {"", ""} , {"", ""}, {"", ""}},
        { {"", "휴~~~ 이번 회사도 탈락인가 ~\n 아무리 기획서가 좋아도 자신이 없는\n나한테는 기획자는 무리인가"}, {"어 이 기획서는..!\n거기 앞에 사람!!","" },{"", "저요?"},{"네 당신이요!!\n당신 제 회사에 취업하실래요?", ""}, {"","진짜요?"},{"네","" },  {"","제 얼굴이나 성격이 소심해도요?"}, {"네 저는 실력만 봅니다", ""} , {"", ""}},
        { {"", "왠지 이번에 계약하려고 하는 연예인도\n뭔가 느낌이 안 들어 어떻게 하지 어!!\n그래 저 사람이야"}, {"저 무슨 일이시죠", ""}, {"", "저 사람의 경호를 하고 싶습니다"}, {"", ""}, {"", ""}, {"", ""} , {"", ""}, {"", ""}, {"", ""}},
        { {"", "여보세요\n상담원을 모집하고 있다고 들었는데"}, { "네", ""}, {"", "저 지원하려고 하는데\n할 수 있을까요?"}, {"당연하죠", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} , {"", ""} },
        { {"", "큼큼\n여기 정비공을 모집한다고 했는데 맞나?"}, {"네", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""}, {"", ""} },
        { {"우리 회사에 취업하면 더 재미있을 꺼야.", ""},{"", "음...."}, {"장비는 업그레이드 해줄게.", ""},{"", "오!!"}, {"", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} },
        { {"제가 화장 장비 다 구매해드릴게요!\n우리 회사 오세요.", ""}, {"", "진짜요?" }, {"네.", ""}, {"", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} , {"", ""} },
        { {"오 이 요리들 맛있겠네요...\n우리 회사에 취업하실래요?", ""},{"", "어... 어?"}, {"시급은 여기보다 더 드릴게요.", ""}, {"", ""}, {"", ""}, {"", ""} , {"", ""} , {"", ""} , {"", ""} },
        { {"우리 회사에 취업해서\n우리 회사를 홍보해주지 않을래?",""}, {"", "음..." }, {"장비는 더욱더 좋은 걸로 해줄께.", ""}, {"", ""}, {"", ""}, {"", ""} , {"", ""}, {"", ""}, {"", ""}},
        { {"음...생각해보니 짤리고 나서 많은 일이 있었지...?", ""}, {"직원들...부동산...가맹점...", ""}, {"이젠 할 게 없네...", ""}, {"생각해보니...내 꿈이 뭐였지...?", ""}, {"건물주...?\n대형 체인점 사장..?\n아니...내꿈은 게임 개발자였어...", ""}, {"생각해보니...\n그 회사...요즘 신입사원을 모집하던데...\n신입이었던 그때가 그립네...", ""}, {"아니!!!\n다시...그 회사에 취업해서...\n신입이었던...그때로 돌아가는 거야..!", ""}, {"그때와 달라진 내 모습을 보여주겠어!!!", ""}, {"", ""} }
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
    /// 초마다 더하는 함수
    /// </summary>
    /// <returns>1초 대기</returns>
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
        AddMajor(current_major, current_major_text);
        AddMajor(second_major, second_major_text);
        second_major_text.text += "/초";
        AddMajor(touch_major, touch_major_text);
        touch_major_text.text += "/클릭";
    }

    /// <summary>
    /// 오디오 소리 플레이 함수
    /// </summary>
    /// <param name="source">사용할 오디오 소스</param>
    /// <param name="clip">사용할 오디오 클립</param>
    public void AudioPlay(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
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
            if (!NotSettingCollections[1].activeSelf)
            {
                GameObject programming_language = Instantiate(programming_language_prefab, parent); // 프리팹 생성
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
    }

    /// <summary>
    /// 캐릭터 초기화 함수
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
    /// 캐릭터 판넬 초기화 함수
    /// </summary>
    void Character_Panel_Init()
    {
        for (int i = 0; i < 12; i++)
        {
            characterNameText[i].text = character_Name[i] + " Lv" + characterLevel[i].ToString();
            characterUpgradeText[i].text = "비용: " + string.Format("{0:#,0}", (i + 1) * 2500 * characterLevel[i] + "\n +" + string.Format("{0:#,0}", ((i + 1) * 2500 * characterLevel[i]) / 2)) + " 클릭";
            if (characterLevel[i] == 0)
            {
                if (characterLevel[i - 1] < 10)
                {
                    characterNameText[i].text = character_Name[i] + " 잠김 " + "(조건 : " + character_Name[i - 1] + " 10레벨)";
                }
            }
            if (characterLevel[i] >= 30)
            {
                characterUpgradeText[i].text = "MAX LEVEL";
            }
        }
    }

    /// <summary>
    /// 부동산 초기화 함수
    /// </summary>
    void Real_estate_Init()
    {
        for (int i = 0; i < 7; i++)
        {
            if (!real_estate_havable[i])
            {
                real_estate_UpgradeText[i].text = "비용: " + string.Format("{0:#,0}", (i + 1) * 20000000) + "\n +" + string.Format("{0:#,0}", ((i + 1) * 200000)) + " 초당";
            }
            else
            {
                real_estate_UpgradeText[i].text = "구매 완료!";
            }
        }
    }

    /// <summary>
    /// 가맹점 초기화 함수
    /// </summary>
    void Franchisee_init()
    {
        for (int i = 0; i < 11; i++)
        {
            if (!Franchisee_havable[i])
            {
                FranchiseeNameText[i].text = "비용: " + string.Format("{0:#,0}", (i + 1) * 30000000) + "\n +" + string.Format("{0:#,0}", (i + 1) * 300000) + " 초당";
            }
            else
            {
                FranchiseeNameText[i].text = "구매 완료!";
            }
        }
    }

    /// <summary>
    /// 카드 초기화 함수
    /// </summary>
    /// <param name="num">현재의 캐릭터 순서</param>
    /// <param name="card_turn">카드의 순서</param>
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
    /// 끝 카드 초기화
    /// </summary>
    /// <param name="num">현재의 캐릭터 순서</param>
    public void EndCard_Init(int num)
    {
        endcardText.text = character_Name[num + 1] + "은(는)\n짜이만의 회사에\n취업 되었다.";
    }

    /// <summary>
    /// 전공력 이펙트 초기화
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
            PowerSavingText.text = "게임 절전 중입니다.\n다시 터치시 절전모드가 해제됩니다.";
            SettingInit(NotSettingCollections[7]);
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
    /// 효과음 버튼
    /// </summary>
    public void SoundEffectButton()
    {
        SettingInit(NotSettingCollections[0]);
    }

    /// <summary>
    /// 전공력 효과 버튼
    /// </summary>
    public void MajorEffectButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        SettingInit(NotSettingCollections[1]);
    }

    /// <summary>
    /// 웹페이지 버튼
    /// </summary>
    public void WebPageButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "9번 부스에 있는 모니터에 있습니다.";
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// 노션 버튼
    /// </summary>
    public void NotionButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Application.OpenURL("https://zest-sandpaper-2a9.notion.site/c718986933634f999783d34c7934cb62");
    }

    /// <summary>
    /// GSM 버튼
    /// </summary>
    public void GSMButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "광주 소프트웨어 마이스터 고등학교 사이트로 이동합니다.";
        Application.OpenURL("http://gsm.gen.hs.kr/main/main.php");
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// 디스코드 버튼
    /// </summary>
    public void DiscordButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "디스코드 링크로 들어갑니다.";
        Application.OpenURL("https://discord.gg/rF2EJJKz");
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// 깃허브 버튼
    /// </summary>
    public void GithubButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "깃허브 링크로 접속합니다.";
        Application.OpenURL("https://github.com/qkrwoaks/Grow_Jjaiman");
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// 절전모드 버튼
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
    /// 초기화 버튼
    /// </summary>
    public void InitButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "게임 초기화 방법은\nPc의 경우 재 설치\n휴대폰의 경우 데이터 삭제를 해주시길 바랍니다.";
        Invoke("EnableGameStautsText", 3f);
    }

    /// <summary>
    /// Setting Panel 안의 나가기 버튼을 눌렀을 때
    /// </summary>
    public void LeaveButton()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        gameStatusTextgameobject.SetActive(true);
        StatusText.text = "게임을 종료합니다.";
        Application.Quit(); // 실행중인 어플리케이션, 에디터 종료 함수
        Invoke("EnableGameStautsText", 3f);
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

    /// <summary>
    /// 캐릭터 버튼을 눌렀을 때
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
    /// 캐릭터 패널 나가기 버튼
    /// </summary>
    public void Character_Exit_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        CharacterPanel.SetActive(false);
    }

    /// <summary>
    /// 캐릭터 업그레이드 버튼
    /// </summary>
    /// <param name="num">번호</param>
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
    /// 부동산 패널 버튼
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
    /// 부동산 패널 나가기 버튼
    /// </summary>
    public void Real_estate_Panel_Exit_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Real_estate_Panel.SetActive(false);
    }

    /// <summary>
    /// 부동산 업그레이드 버튼
    /// </summary>
    /// <param name="num">클릭한 부동산 순서</param>
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
    /// 가맹점 패널 버튼
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
    /// 가맹점 나가기 버튼
    /// </summary>
    public void Franchisee_Panel_Exit_Button()
    {
        if (NotSettingCollections[0].activeSelf)
            sound_effect_Audio.clip = sound_effect_clip[5];
        sound_effect_Audio.Play();
        Franchisee_Panel.SetActive(false);
    }

    /// <summary>
    /// 가맹점 업그레이드 버튼
    /// </summary>
    /// <param name="num">클릭한 가맹점 순서</param>
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
    /// 카드 버튼(앞, 뒤)
    /// </summary>
    /// <param name="isNext">다음 버튼 인지 확인하는 변수</param>
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
    /// 카드 스킵 버튼
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
    /// 끝 카드 나가기 버튼
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
