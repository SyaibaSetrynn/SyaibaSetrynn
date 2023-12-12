using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Recoverer : MonoBehaviour
{
    // Start is called before the first frame update
    static public float HealCount = 0;
    private float HealthCoolDown = 0;
    public float Between_Heal = 2;
    private float HealCoolDown2 = 0;
    [SerializeField] MainController main;
    void Start()
    {
        HealCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealCount>0)
        {
            if (HealthCoolDown >= Time.deltaTime)
                HealthCoolDown -= Time.deltaTime;
            else
            {
                HealthCoolDown = Between_Heal;
                HealCount -= 1;
                main.MainChangeHealth(0, 0, 1, 0, 1, false, (int)(0.1f*main.maxhealth));
            }
        }
        else
        {
            if (HealthCoolDown >= Time.deltaTime)
                HealthCoolDown -= Time.deltaTime;
            else
            {
                HealthCoolDown = 0;
            }
        }
        if (HealCoolDown2>=Time.deltaTime)
            HealCoolDown2 -= Time.deltaTime;
        else if (SceneManager.GetActiveScene().name=="1-Mica_City")
        {
            main.MainChangeHealth(0, 0, 1, 0, 1, false, ((1+(int)(0.001f * main.maxhealth))));
            HealCoolDown2 = 0.02f;
        }
        else
        {
            HealCoolDown2 = 0;
        }
    }
}
