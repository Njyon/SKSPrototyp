using Ultra;
using UnityEngine;

public class SKSUtilities : Singelton<SKSUtilities>
{
    static UIManager uiManger;
    public static UIManager UIManager
    {
        get
        {
            if (uiManger == null)
            {
                uiManger = FindAnyObjectByType<UIManager>(); 
            }
            return uiManger;
        }
    }
}
