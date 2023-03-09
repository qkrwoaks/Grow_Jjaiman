using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public ulong money;
    public ulong touchMoney;
    public ulong secondMoney;

    public Dictionary<string, int> charLevel = new Dictionary<string, int>();
    public Dictionary<string, bool> realEstate = new Dictionary<string, bool>();
    public Dictionary<string, bool> frenchaisee = new Dictionary<string, bool>();
    public Dictionary<string, bool> settingValue = new Dictionary<string, bool>();

}
