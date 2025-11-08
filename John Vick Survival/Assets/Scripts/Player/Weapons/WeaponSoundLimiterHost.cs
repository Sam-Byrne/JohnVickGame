using UnityEngine;

public class WeaponSoundLimiterHost : MonoBehaviour
{
    private static WeaponSoundLimiterHost _instance;
    public static WeaponSoundLimiterHost Instance
    {
        get
        {
            if (_instance == null)
            {
                var hostObj = new GameObject("WeaponSoundLimiterHost");
                _instance = hostObj.AddComponent<WeaponSoundLimiterHost>();
                GameObject.DontDestroyOnLoad(hostObj);
            }
            return _instance;
        }
    }
}
