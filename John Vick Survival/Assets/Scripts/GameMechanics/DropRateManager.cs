using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropRateManager : MonoBehaviour
{
    bool quitting = false;
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    void OnDestroy()
    {
        if (quitting || !gameObject.scene.isLoaded)
            return;

        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                Instantiate(rate.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }


    void OnApplicationQuit()
    {
        quitting = true;
    }


}


