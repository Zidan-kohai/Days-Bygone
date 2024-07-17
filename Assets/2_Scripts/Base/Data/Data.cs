using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();

            StartCoroutine(TimeTreacker());
        }

        private IEnumerator TimeTreacker()
        {
            while (true)
            {
                yield return new WaitForSeconds(60);

                if (QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest)
                && dailyQuest.TryGetQuestWithType(QuestType.PlayTheGameFor10Minutes, out Quest quest))
                {
                    quest.Increament(1);
                }
            }
        }

        public int GetOpenedLevel => SaveData.OpenLevel;

        public float GetFenceHealth => SaveData.Fence.FenceHealth;

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
                    weapon.Value.SpawnSpeed -= 0.1f; 
                    weapon.Value.SpawnSpeed = MathF.Round(weapon.Value.SpawnSpeed, 1);
                    weapon.Value.SpawnSpeed = Mathf.Clamp(weapon.Value.SpawnSpeed, 0.1f, 1000);
                    weapon.Value.BulletSpeed += 0.3f;
                    weapon.Value.BulletSpeed = MathF.Round(weapon.Value.BulletSpeed, 1);
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

        public void UpgrateFence()
        {
            SaveData.Fence.FenceHealth += 5;
            SaveData.Fence.FenceUprgadeCost += 10;

            Save();
        }

        public void ChangeMusicVolume(bool isOn)
        {
            SaveData.IsMusicOn = isOn;
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
        public FenceData Fence;
        public bool IsMusicOn = true;


        public MyDictionary<int, WeaponData>[] Weapons = new MyDictionary<int, WeaponData>[]
        {
            new MyDictionary<int, WeaponData> (0, new WeaponData(){ Damage = 50, SpawnSpeed = 1.7f, BulletSpeed = 5, UpgrateCost = 10, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (1, new WeaponData(){ Damage = 70, SpawnSpeed = 1.4f, BulletSpeed = 6, UpgrateCost = 20, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (2, new WeaponData(){ Damage = 90, SpawnSpeed = 1.2f, BulletSpeed = 7, UpgrateCost = 40, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (3, new WeaponData(){ Damage = 110, SpawnSpeed = 0.9f, BulletSpeed = 8, UpgrateCost = 50, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (4, new WeaponData(){ Damage = 130, SpawnSpeed = 0.8f, BulletSpeed = 9, UpgrateCost = 60, MaxUpdateTimes = 5 } ),
            new MyDictionary<int, WeaponData> (5, new WeaponData(){ Damage = 150, SpawnSpeed = 0.7f, BulletSpeed = 10, UpgrateCost = 70, MaxUpdateTimes = 5 } ),
        };

        [Serializable]
        public class FenceData
        {
            public float FenceHealth = 100;
            public int FenceUprgadeCost = 10;
        }
    }

    [Serializable]
    public class WeaponData
    {
        public int Damage;
        public float SpawnSpeed;
        public float BulletSpeed;
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
