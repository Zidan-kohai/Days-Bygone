using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = coin.ToString();
    }
}