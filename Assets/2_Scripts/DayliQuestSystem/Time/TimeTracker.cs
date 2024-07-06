using Newtonsoft.Json;
using System;
using UnityEngine;

public class TimeTracker
{
    private const string LastLoginKey = "LastLoginTimeKey";
    private LoginData loginData;

    public int GetDayCount => loginData.DayCount;

    public TimeTracker(bool resetable, int dayCount, bool loop)
    {
        Load();

        Init(resetable);

        if(loop)
        {
            loginData.DayCount %= dayCount;
        }
    }

    private void Init(bool resetable)
    {
        if(resetable)
        {
            if(IsExactlyOneDayPassed())
            {
                loginData.DayCount++;
            }
            else if(IsOneDayOrMorePassed())
            {

                loginData.DayCount = 0;
            }
        }
        else if(IsOneDayOrMorePassed())
        {
            loginData.DayCount++;
        }
    }

    public bool IsExactlyOneDayPassed()
    {
        TimeSpan timeSinceLastLogin = GetTimeSinceLastLogin();
        return timeSinceLastLogin.TotalDays >= 1 && timeSinceLastLogin.TotalDays < 2;
    }

    public bool IsOneDayOrMorePassed()
    {
        TimeSpan timeSinceLastLogin = GetTimeSinceLastLogin();
        return timeSinceLastLogin.TotalDays >= 1;
    }

    private void Load()
    {
        string json = PlayerPrefs.GetString(LastLoginKey, JsonConvert.SerializeObject(new LoginData()));
        loginData = JsonConvert.DeserializeObject<LoginData>(json);
    }

    private void Save()
    {

        loginData.DateTime = DateTime.Now;

        string json = JsonConvert.SerializeObject(loginData);

        PlayerPrefs.SetString(LastLoginKey, json);
    }

    
    private TimeSpan GetTimeSinceLastLogin()
    {
        DateTime lastLogin = loginData.DateTime;
        DateTime currentLogin = DateTime.Now;

        return currentLogin - lastLogin;
    }

    ~TimeTracker()
    {
        Save();
    }
}
