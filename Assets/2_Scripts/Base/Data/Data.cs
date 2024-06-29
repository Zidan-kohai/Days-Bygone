using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Base.Data
{
    public class Data : MonoBehaviour
    {
        public static Data Instance;

        private const string dataPath = "data";

        public SaveData SaveData;

        public int CurrentLevel = 0;

        public int CurrentWeaponId = 0;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }

        public int GetOpenedLevel => SaveData.OpenLevel;

        public float GetFenceHealth => SaveData.FenceHealth;

        public void SetOpenLevel()
        {
            SaveData.OpenLevel++;
            Save();
        }

        public WeaponData GetWeaponData(int weaponID)
        {
            foreach (var weapon in SaveData.Weapons)
            {
                if (weapon.key == weaponID)
                {
                    return weapon.Value;
                }
            }

            throw new ArgumentException($"Doesn`t have weapon with id {weaponID}");
        }

        public void UpgrateWeapon(int weaponID)
        {
            foreach(var weapon in SaveData.Weapons)
            {
                if(weapon.key == weaponID && weapon.Value.UpdateTimes < weapon.Value.MaxUpdateTimes)
                {
                    weapon.Value.Damage += 5; 
                    weapon.Value.Speed -= 0.1f; 
                    weapon.Value.Speed = MathF.Round(weapon.Value.Speed, 1);
                    weapon.Value.UpdateTimes++; 
                    weapon.Value.UpgrateCost += 10;

                    bool isMax = weapon.Value.UpdateTimes == weapon.Value.MaxUpdateTimes;
                    Save();
                }
            }
        }

        public void OpenWeapon(int WeaponID)
        {
            if (SaveData.OpenWeaponId.Contains(WeaponID)) return;

            SaveData.OpenWeaponId.Add(WeaponID);
            Save();
        }

        public void ChangeMoney(int money)
        {
            SaveData.Money += money;
            Save();
        }

        private void Save()
        {
            string data = JsonUtility.ToJson(SaveData);
            PlayerPrefs.SetString(dataPath, data);
        }

        private void Load()
        {
            string data = PlayerPrefs.GetString(dataPath, JsonUtility.ToJson(new SaveData()));

            SaveData = JsonUtility.FromJson<SaveData>(data);
        }
    }

    [Serializable]
    public class SaveData
    {
        public int OpenLevel = 1;
        public List<int> OpenWeaponId = new List<int>() { 0 };
        public int Money = 0;
        public float FenceHealth = 100;

        public MyDictionary<int, WeaponData>[] Weapons = new MyDictionary<int, WeaponData>[]
        {
            new MyDictionary<int, WeaponData> (0, new WeaponData(){ Damage = 50, Speed = 2, UpgrateCost = 10, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (1, new WeaponData(){ Damage = 70, Speed = 1.5f, UpgrateCost = 10, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (2, new WeaponData(){ Damage = 100, Speed = 1, UpgrateCost = 10, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (3, new WeaponData(){ Damage = 150, Speed = 0.5f, UpgrateCost = 10, MaxUpdateTimes = 5 } ),
        };
    }

    [Serializable]
    public class WeaponData
    {
        public int Damage;
        public float Speed;
        public int UpgrateCost;
        public int UpdateTimes;
        public int MaxUpdateTimes;
    }

    [Serializable]
    public class MyDictionary<TKey, TValue>
    {
        public TKey key;
        public TValue Value;

        public MyDictionary(TKey key, TValue value)
        {
            this.key = key;
            this.Value = value;
        }
    }
}
