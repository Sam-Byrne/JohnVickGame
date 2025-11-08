using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Pickup Audio")]
    public AudioClip pickupSound;
    [Range(0f, 1f)] public float pickupVolume = 1f;

    [HideInInspector] public bool isCollected = false;
}
