using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BeeRun
{
    public class UserManager
    {
        static private UserManager instance;
        public static UserManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserManager();
                }
                return instance;
            }
        }

        private const string userDataKey = "_user_data_";
        // Can replace PlayerPrefsDataSource with GooglePlayDataSource or anything else
        private IDataSource<UserData> dataSource = new PlayerPrefsDataSource<UserData>(userDataKey);
        private UserData userData;
        private bool isInitialized = false;

        private UserManager() { }

        public async Task Initialize()
        {
            if (isInitialized) return;
            await LoadData();
            isInitialized = true;
        }

        private async Task LoadData() => userData = await dataSource.LoadData();

        private void SaveData() => dataSource.SaveData(userData);

        public void OnAppLaunched()
        {
            userData.AppLaunchCount++;

            SaveData();
        }

        public void OnGameStarted()
        {
            userData.GameStartedCount++;
            SaveData();
        }

        public void OnGameComplete(bool won, int level, int coinsEarned)
        {
            if (won)
            {
                userData.GameWonCount++;
            }
            else
            {
                userData.GameLoseCount++;
            }
            userData.Coins += coinsEarned;
            userData.MaxLevel = Math.Max(level, userData.MaxLevel);

            SaveData();
        }
    }
}
