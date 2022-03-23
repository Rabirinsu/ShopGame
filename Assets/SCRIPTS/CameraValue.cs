using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vcam/CameraValue")]
public class CameraValue : ScriptableObject
{
    public static int talkFOV = 35;
    public static float maxFOV = 100;
    public static float SmoothDelay = .1f;
}
