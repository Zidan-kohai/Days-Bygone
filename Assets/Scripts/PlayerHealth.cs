using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public Image healthBar;
    public float healthAmount;
    public float damage;

    private void Start()
    {
        healthAmount = 1f;
    }

    private void Update()
    {
        healthBar.fillAmount = healthAmount;
    }
}