using System;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static Action<int> OnSphereFallForUI;
    public static Action OnSphereFall;

    public static void SendOnSphereFallForUI(int points)
    {
        if(OnSphereFallForUI != null) OnSphereFallForUI.Invoke(points);
    }

    public static void SendOnSphereFall()
    {
        if(OnSphereFall != null) OnSphereFall.Invoke();
    }
}
