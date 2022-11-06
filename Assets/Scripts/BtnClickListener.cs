using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClickListener : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
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


    }

    public void startButtonOnClick()
    {
        SceneChangeManager.checkFlag(SceneChangeManager.MAIN);

    }
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }

    public void goTitleBtnClick()
    {
        SceneChangeManager.checkFlag(SceneChangeManager.TITLE);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
