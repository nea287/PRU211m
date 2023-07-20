using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using TMPro;
using System;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using Firebase.Extensions;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference DbReference;

    public TMP_Text EmailText;
    public TMP_Text KillText;
    public TMP_Text DeathText;
    public TMP_Text TimeText;
    public FirebaseAuth Auth;
    public FirebaseUser User;

    public String userID;

    void Start()
    {
        Auth = FirebaseAuth.DefaultInstance;
        User = Auth.CurrentUser;
        DbReference = FirebaseDatabase.DefaultInstance.RootReference;
        EmailText.text = "Email: " + User.DisplayName;
        GetUserInfo();
    }

    public IEnumerator GetKills(Action<int> onCallback)
    {
        var userKillData = DbReference.Child("users").Child(userID).Child("Kills").GetValueAsync();
        yield return new WaitUntil(predicate: () => userKillData.IsCompleted);
        if (userKillData != null)
        {
            DataSnapshot snapshot = userKillData.Result;
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    public IEnumerator GetDeaths(Action<int> onCallback)
    {
        var userDeathData = DbReference.Child("users").Child(userID).Child("Deaths").GetValueAsync();
        yield return new WaitUntil(predicate: () => userDeathData.IsCompleted);
        if (userDeathData != null)
        {
            DataSnapshot snapshot = userDeathData.Result;
            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    public IEnumerator GetTime(Action<float> onCallback)
    {
        var userTimeData = DbReference.Child("users").Child(userID).Child("Time").GetValueAsync();
        yield return new WaitUntil(predicate: () => userTimeData.IsCompleted);
        if (userTimeData != null)
        {
            DataSnapshot snapshot = userTimeData.Result;
            onCallback.Invoke(float.Parse(snapshot.Value.ToString()));
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetKills((int kills) =>
        {
            KillText.text = "Kills: " + kills;
        }));
        StartCoroutine(GetDeaths((int deaths) =>
        {
            DeathText.text = "Gold: " + deaths;
        }));
        StartCoroutine(GetTime((float time) =>
        {
            TimeText.text = "Time: " + time;
        }));
    }
}
