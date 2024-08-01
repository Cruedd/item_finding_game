using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // E�er karakter veya belirli bir GameObject trigger alan�na girdiyse kap�y� a�
        if (other.CompareTag("Player"))
        {
            MarketList.Instance.YouEscapedYouWin();
        }
    }
}
