using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;



public class FirebaseLoginManager : MonoBehaviour
{
    private FirebaseAuth auth;

    // メール/パスワード入力用UI（Unityからアサイン）
    [Header("メールログイン用UI")]
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public TMP_Text statusText;
    public TMP_InputField usernameInputField;
    public GameObject accountItemPrefab;

    // 追加部分

    [Header("アカウント一覧の親（Content）")]
    public Transform accountListParent;

    // これはあとで使います：
    // public GameObject accountItemPrefab;

    // ...既存のコードはそのまま下に続けてOK




    void Start()
    {
        // Firebaseの初期化とAuthの取得
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase Authentication 初期化完了");
            }
            else
            {
                Debug.LogError("Firebase 初期化失敗: " + task.Result.ToString());
            }
        });
    }

    //  匿名ログイン（ボタンに接続）
    public void SignInAsGuest()
    {
        statusText.text = "ゲストログイン中...";
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "ゲストログイン失敗";
                Debug.LogError("匿名ログイン失敗: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;
            statusText.text = "ゲストログイン成功 UID: " + user.UserId;
            Debug.Log("匿名ログイン成功: " + user.UserId);
        });
    }

    //  メールアドレスでアカウント作成
    public void RegisterWithEmail()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        string username = usernameInputField.text;

        statusText.text = "アカウント作成中...";
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "登録失敗: " + task.Exception.InnerExceptions[0].Message;
                Debug.LogError("登録エラー: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;

            //  ユーザーの表示名（ユーザーネーム）を更新する
            UserProfile profile = new UserProfile { DisplayName = username };

            user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(updateTask =>
            {
                if (updateTask.IsCanceled || updateTask.IsFaulted)
                {
                    statusText.text = "ユーザー名の更新失敗";
                    Debug.LogError("表示名更新失敗: " + updateTask.Exception);
                    return;
                }

                statusText.text = $"登録成功\n名前: {user.DisplayName}\nメール: {user.Email}";
                Debug.Log($"登録成功: {user.Email}, 名前: {user.DisplayName}");
            });
        });
    }

    //  メールアドレスでログイン
    public void LoginWithEmail()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        statusText.text = "ログイン中...";
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "ログイン失敗: " + task.Exception.InnerExceptions[0].Message;
                Debug.LogError("ログインエラー: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;
            statusText.text = "ログイン成功 UID: " + user.UserId;
            Debug.Log("ログイン成功: " + user.Email);
        });
    }

    //  ログアウト（任意）
    public void Logout()
    {
        auth.SignOut();
        statusText.text = "ログアウトしました";
        Debug.Log("ログアウト完了");
    }
}
