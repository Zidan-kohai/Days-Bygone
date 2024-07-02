using Base.Data;
using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;

    public event Action onMoneyChange;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int money)
    {
        Data.Instance.ChangeMoney(money);
        onMoneyChange?.Invoke();
    }

    public void SubstructMoney(int money)
    {
        Data.Instance.ChangeMoney(-money);
        onMoneyChange?.Invoke();
    }
}
