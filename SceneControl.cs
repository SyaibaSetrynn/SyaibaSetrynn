using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Scene AtScene;
    //public float InSceneTime;
    private void Start()
    {
        AtScene = SceneManager.GetActiveScene();
        
    }
    public bool InRange(float upperx, float lowerx, float uppery, float lowery, float currx, float curry)
    {
        if ((currx<=upperx) && (currx >=lowerx) && (curry>=lowery) && (curry<=uppery))
        {
            return true;
        }
        return false;
    }
    private void ChooseScript(int seat,int low, int high)
    {
        SavesMain.ScriptRoller[seat]++;
        if (SavesMain.ScriptRoller[seat] > high) SavesMain.ScriptRoller[seat] = low;
        while (CharacterScripts.Script[SavesMain.ScriptRoller[seat]] == null)
        {
            SavesMain.ScriptRoller[seat]++;
            if (SavesMain.ScriptRoller[seat] > high) SavesMain.ScriptRoller[seat] = low;
        }
        SavesMain.HintNumber = SavesMain.ScriptRoller[seat];
    }
    void Update()
    {
        //InSceneTime = InSceneTime + Time.deltaTime;
        Vector2 currposition = transform.position;
    //    if (InSceneTime >= 1)
        {
            if (AtScene.name == "1-Mica_City")
            {
                if (currposition.x >= 64)
                {
                    ChooseScript(2, 10, 19);
                    SceneManager.LoadScene("2-Infinite_Grassland");
                    AtScene = SceneManager.GetActiveScene();
                   // InSceneTime = 0;
                }
            }
            else if (AtScene.name == "2-Infinite_Grassland")
            {
                if (currposition.x <= 0)
                {
                    SceneManager.LoadScene("1-Mica_City");
                    AtScene = SceneManager.GetActiveScene();
                   // InSceneTime = 0;
                }
                else if (currposition.x >= 189)
                {
                    ChooseScript(3, 20, 29);
                    SceneManager.LoadScene("3-Warped_Mountain");
                    AtScene = SceneManager.GetActiveScene();
                    //   InSceneTime = 0;
                }
                //transform.position = currposition;
            }
            else if (AtScene.name == "3-Warped_Mountain")
            {
                if (InRange(69f, 67f, -332f, -334f, currposition.x, currposition.y))
                {
                    ChooseScript(2, 10, 19);
                    SceneManager.LoadScene("2-Infinite_Grassland");
                    AtScene = SceneManager.GetActiveScene();
                    // InSceneTime = 0;
                }
                else if (InRange(237f, 231f, -367f, -371f, currposition.x, currposition.y))
                {
                    ChooseScript(4, 30, 34);
                    SceneManager.LoadScene("4-Volcano_Entrance");
                    AtScene = SceneManager.GetActiveScene();
                    //   InSceneTime = 0;
                }
            }
            else if (AtScene.name == "4-Volcano_Entrance")
            {
                if (SavesMain.defeatedL4)
                {
                    if (InRange(715f, 713f, -66f, -68f, currposition.x, currposition.y))
                    {
                        ChooseScript(3, 20, 29);
                        SceneManager.LoadScene("3-Warped_Mountain");
                        AtScene = SceneManager.GetActiveScene();
                        //  InSceneTime = 0;
                    }
                    else if (InRange(680f, 678f, -67f, -69f, currposition.x, currposition.y))
                    {
                        ChooseScript(5, 35, 35);
                        SceneManager.LoadScene("5-Lava_Palace");
                        AtScene = SceneManager.GetActiveScene();
                     //   InSceneTime = 0;
                    }
                }
            }
        }
    }
}
