using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavesMain : MonoBehaviour
{
    static public float mainvelocity = -1;
    static public float maxhealth=-1;
    static public int EffectiveChatCounter = 0;
    static public float current_health=-1;
    static public int attack =-1;
    static public int defence =-1;
    static public int magic_defence =-1;
    static public int toward =-1;
    static public float[,] latestposition =new float [3, 6];
    static public bool defeatedL4 = false;
    static public int swordlevel = 0;
    static public int armorlevel = 0;
    static public int bowlevel = 0;
    static public long money = 0;
    static public int bottles = 0;
    private static SavesMain instance;
    static public float KaguraHealth=-10000;
    static public int KaguraDead = 0;
    static public bool KaguraSummoned = false;
    static public bool ended = false;
    static public bool L2KaguraSay = false;
    static public int MissionCompleted = 0;
    static public int HintNumber = 0;
    private bool GettoL3 = false;
    private bool GettoL4 = false;
    static public bool GettoL1 = false;
    static public bool Kua10000 = false;
    static public bool Kua20000 = false;
    static public int[] ScriptRoller = new int[6] { 0, 5, 19, 29, 34, 36 };
    private bool GetBottle = false;
    public static bool KaguraLeaveHome = false;
    static public bool notFindBoss = true;
    static public float SmoveCoolDown;
    //static public float[,] KaguraLatestposition = new float[3, 6];
    //private float testtime = 0;
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    static public int getBowAttack(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 60;
        if (n == 2) return 120;
        if (n == 3) return 250;
        if (n == 4) return 270;
        if (n == 5) return 290;
        return 3000;
    }
    static public int getSwordAttack(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 20;
        if (n == 2) return 70;
        if (n == 3) return 170;
        if (n == 4) return 190;
        if (n == 5) return 210;
        return 3000;
    }
    // Update is called once per frame
    private void Update()
    {
        if (SmoveCoolDown>0)
            SmoveCoolDown -= Time.deltaTime;
        HintsBox.MissionNumber = MissionCompleted;
       // Debug.Log(MissionCompleted);
       if ((bottles!=0) && (!GetBottle))
        {
            GetBottle = true;
            HintNumber = 37;
            HintsBox.HintNumber = HintNumber;
            //Debug.Log("here");
        }
        if (SceneManager.GetActiveScene().name != "1-Mica_City")
        {
            HintsBox.HintNumber = HintNumber;
        }
        //Debug.Log(latestposition);
        if ((!GettoL3) && (latestposition[0,3]!=0))
        {
            GettoL3 = true;
            MissionCompleted++;
        }
        if ((!GettoL4) && (latestposition[0,4] != 0))
        {
            GettoL4 = true;
            MissionCompleted++;
        }
    }
}
