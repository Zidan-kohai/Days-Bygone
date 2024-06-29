using System;
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

        public void OpenWeapon(int WeaponID)
        {
            SaveData.openWeaponId.Add(WeaponID);
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
        public List<int> openWeaponId = new List<int>() { 0 };
        public int money = 0;
        public float FenceHealth = 100;
    }
}
