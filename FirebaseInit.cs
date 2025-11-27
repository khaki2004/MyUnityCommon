using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour
{
    void Start()
    {
        // Firebase SDK‚Ì‰Šú‰»
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log(" Firebase ‰Šú‰»¬Œ÷I");
            }
            else
            {
                Debug.LogError(" Firebase ‰Šú‰»¸”s: " + task.Result.ToString());
            }
        });
    }
}
