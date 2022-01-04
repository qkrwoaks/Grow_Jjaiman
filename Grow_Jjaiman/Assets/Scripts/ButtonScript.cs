using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void save()
    {
        DatabaseManager.Instance.Save();
    }

    public void reset()
    {
        DatabaseManager.Instance.resetData();
    }
}
