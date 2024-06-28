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

        public int GetOpenedLevel => SaveData.openLevel;

        public void SetOpenLevel()
        {
            SaveData.openLevel++;
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
        public int openLevel = 1;
        public List<int> openWeaponId = new List<int>() { 0 };
        public int money = 0;
    }
}
