using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goinCoinPrefab, healthGlobePrefab, staminaGlobePrefab;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        if(randomNum == 1)
        {
            Instantiate(staminaGlobePrefab, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(healthGlobePrefab, transform.position, Quaternion.identity);
        }

        if(randomNum == 3)
        {
            int randomAmountGold = Random.Range(1, 4);

            for(int i = 0; i < randomAmountGold; i++)
            {
                Instantiate(goinCoinPrefab, transform.position, Quaternion.identity);
            } 
        }
    }
}
