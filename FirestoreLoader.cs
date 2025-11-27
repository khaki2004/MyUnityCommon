// Scripts/Firebase/FirestoreLoader.cs Ç…ï€ë∂êÑèß

using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public class FirestoreLoader : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI ownerNameText;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                LoadProductData();  // FirestoreÇ©ÇÁì«Ç›çûÇﬁ
            }
            else
            {
                Debug.LogError("FirebaseÇÃàÀë∂ä÷åWÇ™ñûÇΩÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ: " + task.Result);
            }
        });
    }

    void LoadProductData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        // "products" ÉRÉåÉNÉVÉáÉìÇÃç≈èâÇÃ1åèÇéÊìæ
        db.Collection("products").Limit(1).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                foreach (DocumentSnapshot doc in task.Result.Documents)
                {
                    string title = doc.GetValue<string>("title");
                    string description = doc.GetValue<string>("description");
                    string price = doc.GetValue<string>("price");
                    string ownerName = doc.GetValue<string>("ownerName");

                    titleText.text = title;
                    descriptionText.text = description;
                    priceText.text = price;
                    ownerNameText.text = ownerName;
                }
            }
            else
            {
                Debug.LogError("Firestore ì«Ç›çûÇ›é∏îs: " + task.Exception);
            }
        });
    }
}
