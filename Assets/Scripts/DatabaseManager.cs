using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{

    public static DatabaseManager Instance;

    public PlayerData playerData = new PlayerData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Load()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "\\SaveFolder\\playerData.json");
        playerData = JsonUtility.FromJson<PlayerData>(data);



        GameManager.gm.SetData();

    }

    public void Save()
    {

        playerData.money = GameManager.gm.current_major;
        playerData.touchMoney = GameManager.gm.touch_major;
        playerData.secondMoney = GameManager.gm.second_major;


        playerData.charLevel["Jjaiman"] = GameManager.gm.characterLevel[0];
        playerData.charLevel["Artist"] = GameManager.gm.characterLevel[1];
        playerData.charLevel["Fashionista"] = GameManager.gm.characterLevel[2];
        playerData.charLevel["Bully"] = GameManager.gm.characterLevel[3];
        playerData.charLevel["Planner"] = GameManager.gm.characterLevel[4];
        playerData.charLevel["Bodyguard"] = GameManager.gm.characterLevel[5];
        playerData.charLevel["Consultant"] = GameManager.gm.characterLevel[6];
        playerData.charLevel["Fixer"] = GameManager.gm.characterLevel[7];
        playerData.charLevel["Hacker"] = GameManager.gm.characterLevel[8];
        playerData.charLevel["Maker_Artist"] = GameManager.gm.characterLevel[9];
        playerData.charLevel["Chef"] = GameManager.gm.characterLevel[10];
        playerData.charLevel["Professional_gamer"] = GameManager.gm.characterLevel[11];

        playerData.realEstate["House_Real"] = GameManager.gm.real_estate_havable[0];
        playerData.realEstate["_30pyeong_apartment_Real"] = GameManager.gm.real_estate_havable[1];
        playerData.realEstate["_1st_floor_shopping_mall_Real"] = GameManager.gm.real_estate_havable[2];
        playerData.realEstate["_5_story_building_Real"] = GameManager.gm.real_estate_havable[3];
        playerData.realEstate["_10_story_building_Real"] = GameManager.gm.real_estate_havable[4];
        playerData.realEstate["_20_story_building_Real"] = GameManager.gm.real_estate_havable[5];
        playerData.realEstate["_50_story_building_Real"] = GameManager.gm.real_estate_havable[6];


        playerData.frenchaisee["SejongCultural"] = GameManager.gm.Franchisee_havable[0];
        playerData.frenchaisee["SeoulArts"] = GameManager.gm.Franchisee_havable[1];
        playerData.frenchaisee["Jjaigram"] = GameManager.gm.Franchisee_havable[2];
        playerData.frenchaisee["Jjp"] = GameManager.gm.Franchisee_havable[3];
        playerData.frenchaisee["JjaiGuard"] = GameManager.gm.Franchisee_havable[4];
        playerData.frenchaisee["Jtd"] = GameManager.gm.Franchisee_havable[5];
        playerData.frenchaisee["Jjaigong"] = GameManager.gm.Franchisee_havable[6];
        playerData.frenchaisee["Hacksy"] = GameManager.gm.Franchisee_havable[7];
        playerData.frenchaisee["Touch"] = GameManager.gm.Franchisee_havable[8];
        playerData.frenchaisee["JjaiRestaurant"] = GameManager.gm.Franchisee_havable[9];
        playerData.frenchaisee["Fg"] = GameManager.gm.Franchisee_havable[10];

        string data = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "\\SaveFolder\\PlayerData.json", data);
    }

    public void resetData()
    {

        TextAsset data = Resources.Load<TextAsset>("resetData");
        playerData = JsonUtility.FromJson<PlayerData>(data.text);
        SceneManager.LoadScene("LoadScene");
    }
}
