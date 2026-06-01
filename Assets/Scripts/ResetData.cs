using UnityEngine;

public class ResetData : MonoBehaviour
{
    void Start()
    {
        GameData.ResetData();

        Cursor.visible = true;
    }

}
