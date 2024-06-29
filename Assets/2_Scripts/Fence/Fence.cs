using Base.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fence : MonoBehaviour
{
    [SerializeField] private GameWindowController gameWindowController;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float FenceHealth;

    private void Start()
    {
        FenceHealth = Data.Instance.GetFenceHealth;
        healthSlider.maxValue = FenceHealth;
        healthSlider.value = FenceHealth;
    }

    public void GetDamage(float damage)
    {
        FenceHealth -= damage;

        FenceHealth = Mathf.Clamp(FenceHealth, 0, Data.Instance.GetFenceHealth);
        healthSlider.value = FenceHealth;

        if(FenceHealth <= 0)
        {
            gameWindowController.Lose();
        }
        else
        {
            StartCoroutine(DamageAnimation());
        }
    }

    private IEnumerator DamageAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }
}
