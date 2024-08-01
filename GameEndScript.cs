using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Eðer karakter veya belirli bir GameObject trigger alanýna girdiyse kapýyý aç
        if (other.CompareTag("Player"))
        {
            MarketList.Instance.YouEscapedYouWin();
        }
    }
}
