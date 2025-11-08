using System.Collections;
using UnityEngine;

public class SwordBehaviour : MeleeWeaponBehaviour
{
    public float swingDuration = 0.22f;
    public float radius = 0.5f;
    public float sideOffset = 1f;


    public void SetDirection(Vector2 dir)
    {
        if (dir.x >= 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localPosition = new Vector3(radius, sideOffset, 0f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.localPosition = new Vector3(-radius, sideOffset, 0f);
        }

        StartCoroutine(EndSlash());
    }
    IEnumerator EndSlash()
    {
        yield return new WaitForSeconds(swingDuration);
        Destroy(gameObject);
    }
}
