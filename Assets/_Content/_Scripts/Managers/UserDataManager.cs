using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetroGXR.WhisperingDoubles.Managers
{
    public class UserDataManager : MonoBehaviour
    {
        [System.Serializable]
        public class GameData
        {
            public int score;
            public int playedLevels;
        }

        [System.Serializable]
        public class SettingsData
        {
            public int levelCards;
        }

        private readonly string PrefsKeyGameData = "PREFSGAMEDATA";
        private readonly string PrefsKeySettingsData = "PREFSSETTINGSDATA";

        private GameData gameData;
        private SettingsData settingsData;

        public static UserDataManager Instance;

        public int Score => gameData.score;

        public int PlayedLevels => gameData.playedLevels;

        public int LevelCards => settingsData.levelCards;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(PrefsKeyGameData));
            gameData ??= new GameData();


            settingsData = JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString(PrefsKeySettingsData));
            settingsData ??= new SettingsData() { levelCards = 4 };
        }

        public void AddScore(int score)
        {
            gameData.score += score;

            PlayerPrefs.SetString(PrefsKeyGameData, JsonUtility.ToJson(gameData));
            PlayerPrefs.Save();
        }

        public void CountPlayedLevel()
        {
            gameData.playedLevels++;

            PlayerPrefs.SetString(PrefsKeyGameData, JsonUtility.ToJson(gameData));
            PlayerPrefs.Save();
        }

        public void SetLevelCards(int cards)
        {
            settingsData.levelCards = cards;

            PlayerPrefs.SetString(PrefsKeySettingsData, JsonUtility.ToJson(settingsData));
            PlayerPrefs.Save();
        }
    }
}