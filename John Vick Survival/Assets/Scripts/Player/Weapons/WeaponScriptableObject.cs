using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    // base weapon stats
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip critSound;
    [Range(0f, 1f)] public float critVolume = 1f;
    [Range(0, 8)]
    public int maxSimultaneousHitSounds = 3;

    [Range(0f, 1f)]
    public float hitVolume = 1f;

    




}
