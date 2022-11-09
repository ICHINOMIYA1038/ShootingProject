using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

/// <summary>
///????????????????????
///csv??1???????
///auto?true???????????????
/// </summary>
public class MovieTextController : MonoBehaviour
{
    TextMeshProUGUI movieText;
    [SerializeField]
    string[] textArray;
    TextAsset csvFile;
    /// <summary>
    ///??????????
    /// </summary>
    int textMaxIndex = 0;
    /// <summary>
    ///????????index
    /// </summary>
    int textNow = 0;
    [SerializeField]
    bool auto;
    float time;
    /// <summary>
    ///???????????????
    /// </summary>
    [SerializeField]
    float changeTime;

    // Start is called before the first frame update
    void Start()
    {
        movieText = GetComponent<TextMeshProUGUI>();
        csvFile = Resources.Load("dialog") as TextAsset; // Resouces??CSV?????
        StringReader reader = new StringReader(csvFile.text);
        //????10????????????????????
        textArray = new string[10];
        int i = 0;
        while (reader.Peek() != -1) 
        {
            string line = reader.ReadLine(); // 1??????
            textArray[i] = line; // ??????????
            i += 1;
        }
        textMaxIndex = i;
        movieText.text = textArray[textNow];
        
    }

    // Update is called once per frame
    void Update()
    {
        //?????true??????????
        if(auto==true)
        {
            time += Time.deltaTime;
            if (time > changeTime)
            {
                time = 0;
                NextText();
            }
        }
    }

    /// <summary>
    /// ???????????
    /// </summary>
    /// <returns>
    /// ????????????false???
    /// </returns>
    public bool NextText()
    {
        if(textNow<textMaxIndex)
        {
            textNow += 1;
            movieText.text = textArray[textNow];
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
