using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private PlayerStats player;
    CircleCollider2D playerCollector;

    void Start()
    {
        player = GetComponentInParent<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!player || !player.alive) return; 

        ICollectable item = col.GetComponent<ICollectable>();
        if (item != null)
        {
            item.Collect();
        }
    }


}
