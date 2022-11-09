using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// ボタンを押した時の処理
/// </summary>
public class BtnClickListener : MonoBehaviour
{
    [SerializeField]
    GameManager manager;
    Button button;

    // ボタンの名前に応じてイベントリスナーを追加
    void Start()
    {
        button = GetComponent<Button>();

        if(button.name.Equals("StartBtn"))
        {
            button.onClick.AddListener(startButtonOnClick);
        }
        if (button.name.Equals("ExitBtn"))
        {
            button.onClick.AddListener(Exit);
        }
        if (button.name.Equals("Btn_GameOver"))
        {
            button.onClick.AddListener(goTitleBtnClick);
        }
        if (button.name.Equals("Btn_GameClear"))
        {
            button.onClick.AddListener(goTitleBtnClick);
        }
        if (button.name.Equals("ExitBtn"))
        {
            button.onClick.AddListener(Exit);
        }
        if (button.name.Equals("returnBtn"))
        {
            button.onClick.AddListener(returnBattleBtn);
        }
    }

    //タイトルからメイン画面に遷移
    public void startButtonOnClick()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);

    }

    //ゲームを終了
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //タイトルへ戻るボタンを押した時の処理
    public void goTitleBtnClick()
    {
        manager.sceneChange(GameManager.TITLE_SCENE);

    }

    //Config画面からMain画面に戻るときの処理
    public void returnBattleBtn()
    {
        manager.ReturnFromConfig();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
