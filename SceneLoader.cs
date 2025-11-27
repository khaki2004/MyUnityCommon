using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //戻り先のシーン名を保持するための変数
    private static string previousScene = null;


    // この関数をボタンのOnClick()などから呼び出せばシーン遷移できる
    public void LoadSceneByName(string sceneName)
    {
        // メモ：Build Settings に追加されているシーン名を正しく指定
        SceneManager.LoadScene(sceneName);
    }

    // 現在のシーンをリロード（例：リトライボタン用）
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // アプリケーションを終了（PC用）
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("アプリ終了（エディタでは無効）");
    }

    public void Start()
    {
        //最初に表示させたい通知を記述
    }

    //↓↓ここから追加

    // 新しいシーンをロードし、現在のシーン名を保存しておく
    public void LoadSceneWithHistory(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    // ひとつ前のシーンに戻る
    public void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("前のシーンが記録されていません。");
        }
    }
}
