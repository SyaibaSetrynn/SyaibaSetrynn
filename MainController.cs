using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool attacking = false;
    public int swordlevel = 0;
    public int armorlevel = 0;
    public int bowlevel = 0;
    public float mainvelocity = 200;
    public float maxhealth = 500f; 
    public float current_health = 500f;
    public int attack = 80;
    public int defence = 50;
    public int magic_defence = 0;
    public int toward = 3; // 0=right 1=up 2=left 3=down
    public int currscene;
    public bool defeatedL4 = false;
    static public int attack_time = 0;
    public float attack_cold_down;
    static float speedDecrease = 0;
    public float Combat_Cold_down = 1.5f;
    public bool Poissioned = false; //L3E3 Poison
    public float Poissioned_time = 0;
    public bool Burned_by_Lava = false; // L4/L5 Lava
    public float Burned_by_Lava_time = 0;
    private float Lastly_LavaAttack = 0;
    private float Lastly_PoisonAttack = 0;
    public float Between_LavaAttack = 0.8f;
    public float Between_PoisonAttack = 0.8f;
    public float Shoot_Cold_down = 0.8f;
    public int bottles = 0;
    public GameObject projectilePrefab;
    public long money = 0;
    public bool Stunned = false;
    public float Stunned_Time;
    public static bool KaguraDead = false;
    Rigidbody2D rigidbody2d;
    public Animator animator;
    private Vector2 fixedposition(Vector2 ToBeFixed)
    {
        if (currscene == 1)
        {
            ToBeFixed.x = 63f;
        } else if (currscene == 2)
        {
            if (ToBeFixed.x < 80)
            {
                ToBeFixed.x = 2f;
                ToBeFixed.y = -148f;
            }
            else
            {
                ToBeFixed.x = 187f;
            }
        } else if (currscene==3)
        {
            if ((ToBeFixed.x<80)||(!defeatedL4))
            {
                ToBeFixed.x = 64.5f;
                ToBeFixed.y = -328.1f;
            }
            else
            {
                ToBeFixed.x = 239f;
                ToBeFixed.y = -368f;
            }
        }else if (currscene==4)
        {
            if (ToBeFixed.x>700)
            {
                ToBeFixed.x = 709.65f;
                ToBeFixed.y = -66.53f;
            }
            else
            {
                ToBeFixed.y = ToBeFixed.y + 10;
            }
        }
        return ToBeFixed;
    }
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (SavesMain.attack!=-1)
        {
            swordlevel = SavesMain.swordlevel;
            armorlevel = SavesMain.armorlevel;
            bowlevel = SavesMain.bowlevel;
            attack = SavesMain.attack;
            defence = SavesMain.defence;
            money = SavesMain.money;
            bottles = SavesMain.bottles;
            magic_defence = SavesMain.magic_defence;
            current_health = SavesMain.current_health;
            toward = SavesMain.toward;
            mainvelocity = SavesMain.mainvelocity;
            maxhealth = SavesMain.maxhealth;
            defeatedL4 = SavesMain.defeatedL4;
            if (SavesMain.latestposition[0, currscene] == 1)
            {
                Vector2 position2;
                position2.x = SavesMain.latestposition[1, currscene];
                position2.y = SavesMain.latestposition[2, currscene];
                position2 = fixedposition(position2);
                transform.position = position2;
            }
        }
        if (current_health >= 0)
        {
            TheHealthMask.instance.SetValue((float)current_health / (float)maxhealth);
            //Debug.Log((float)current_health / (float)maxhealth);
        }
        else
            TheHealthMask.instance.SetValue(0);
    }
    private void Attribute_update()
    {
        SavesMain.bowlevel = bowlevel;
        SavesMain.armorlevel = armorlevel;
        SavesMain.swordlevel = swordlevel;
        SavesMain.attack = attack;
        SavesMain.defence = defence;
        SavesMain.magic_defence = magic_defence;
        SavesMain.current_health = current_health;
        SavesMain.toward = toward;
        SavesMain.bottles = bottles;
        SavesMain.mainvelocity = mainvelocity;
        SavesMain.maxhealth = maxhealth;
        SavesMain.defeatedL4 = defeatedL4;
        SavesMain.money = money;
        Vector2 position;
        position= transform.position;
        SavesMain.latestposition[0, currscene] = 1;
        SavesMain.latestposition[1, currscene] = position.x;
        SavesMain.latestposition[2, currscene] = position.y;
    }
    private Vector2 VectorDirection(int n)
    {
        if (n == 0)
            return new Vector2(1, 0);
        else if (n == 1)
            return new Vector2(0, 1);
        else if (n == 2)
            return new Vector2(-1, 0);
        else return new Vector2(0, -1);
    }
    public void MainSplit()
    {
        attacking = true;
    }
    public void MainLaunch()
    {
        if (attack_cold_down == 0)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.4f, Quaternion.identity);
            Bullet projectile = projectileObject.GetComponent<Bullet>();
            projectile.Launch(VectorDirection(toward), 800);
            attack_cold_down = Shoot_Cold_down;
        }
    }

    public void Calculate_Debuff()          //（王柯）将此函数public
    {
        if (Stunned_Time > 0)
        {
            Stunned_Time -= Time.deltaTime;
        }
        else
        {
            Stunned = false;
            Stunned_Time = 0;
        }
        if (Poissioned_time>Time.deltaTime)
        {
            Poissioned_time -= Time.deltaTime;
        }
        else
        {
            Poissioned = false;
            Poissioned_time = 0;
        }
        if (Burned_by_Lava_time > Time.deltaTime)
        {
            Burned_by_Lava_time -= Time.deltaTime;
        }
        else
        {
            Burned_by_Lava = false;
            Burned_by_Lava_time = 0;
        }
        if (Lastly_LavaAttack > Time.deltaTime)
        {
            Lastly_LavaAttack -= Time.deltaTime;
        }
        else
        {
            if (Burned_by_Lava)
            {
                MainChangeHealth(150,0,defence,60,magic_defence,false,-20);
                Lastly_LavaAttack = Between_LavaAttack;
            }
            else
            {
                Lastly_LavaAttack = 0;
            }
        }
        if (Lastly_PoisonAttack > Time.deltaTime)
        {
            Lastly_PoisonAttack -= Time.deltaTime;
        }
        else
        {
            if (Poissioned)
            {
                MainChangeHealth(20, 0, defence, 80, magic_defence, false, -10);
                Lastly_PoisonAttack = Between_PoisonAttack;
            }
            else
            {
                Lastly_PoisonAttack = 0 ;
            }    
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (current_health >= 0)
        {
            if (SavesMain.SmoveCoolDown<=0)
            {
                if (Input.GetKey(KeyCode.R)) 
                {
                    if (currscene <= 3)
                        transform.position = fixedposition(transform.position);
                    if (currscene==5)
                    {
                        Vector2 neoposition = new Vector2(557, -227);
                        transform.position = neoposition;
                    }
                    SavesMain.SmoveCoolDown = 10.0f;
                }
            }
            Attribute_update();
            animator.SetFloat("DirectionForBullets", toward / 3.0f);
            Calculate_Debuff();
            if (attack_cold_down < Combat_Cold_down - 0.5f)
                attacking = false;
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (attack_cold_down == 0)
                {
                    animator.SetTrigger("Split");
                    Invoke("MainSplit", 0.3f);
                    attack_cold_down = Combat_Cold_down;
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                animator.SetTrigger("Launch");
                Invoke("MainLaunch", 0.2f);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (bottles >= 1)
                {
                    bottles -= 1;
                    Recoverer.HealCount += 6;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if ((current_health >= 0)&&(!Stunned))
        {
            float mainhorizontal = Input.GetAxis("Horizontal");
            float mainvertical = Input.GetAxis("Vertical");
            bool XbigY = (Mathf.Abs(mainhorizontal) > Mathf.Abs(mainvertical));
            bool Xpositive = mainhorizontal > 0;
            bool Ypositive = mainvertical > 0;
            bool Xnegative = mainhorizontal < 0;
            bool Ynegative = mainvertical < 0;
            if (XbigY && Xpositive)
                toward = 0;
            else if (XbigY && Xnegative)
                toward = 2;
            else if (!XbigY && Ypositive)
                toward = 1;
            else if (!XbigY && Ynegative)
                toward = 3;
            rigidbody2d.velocity = new Vector2(0, 0);
            Vector2 position = transform.position;
            position.x = position.x + mainvelocity * (1 - speedDecrease) * mainhorizontal * Time.deltaTime;
            position.y = position.y + mainvelocity * (1 - speedDecrease) * mainvertical * Time.deltaTime;
            transform.position = position;
            if (attack_cold_down < Time.deltaTime)
                attack_cold_down = 0;
            else
                attack_cold_down -= Time.deltaTime;
            animator.SetFloat("Move X", mainhorizontal);
            animator.SetFloat("Move Y", mainvertical);
            animator.SetFloat("Speed", (Mathf.Abs(mainhorizontal) + Mathf.Abs(mainvertical)) / 2);
            //Debug.Log("position="+ SavesMain.latestposition[1, currscene]+" "+ SavesMain.latestposition[2, currscene]);
            //Debug.Log(current_health);
        }
        if (SavesMain.ended)
        {
            Invoke("FindTheEndding",0.1f);
        }
    }

    private int GetArmorDiscount(int n)
    {
        if (n <= 2)
            return 0;
        if (n == 3)
            return 10;
        if (n == 4)
            return 20;
        if (n == 5)
            return 30;
        return 60;
    }
    int GetArmorDef()
    {
        if (armorlevel == 1) return 50;
        if (armorlevel == 2) return 120;
        if (armorlevel == 3) return 200;
        if (armorlevel == 4) return 225;
        if (armorlevel == 5) return 250;
        return 0;
    }
    public void MainChangeHealth(int atk, int swatk, int def, int mgatk, int mgdef, bool is_long_range, int offset)
    {
        float deltaHealth = 0;
        def += GetArmorDef();
        float atkrate = (float)atk / (float)def;
        if (is_long_range)
        {
            atkrate = 0.8f * 1.55f * (float)Mathf.Log(1 + atkrate) / (float)Mathf.Log(2);
            deltaHealth -= (75 + swatk) * atkrate + deltaHealth;
        }
        else
        {
            atkrate = 1.55f * (float)Mathf.Log(1 + atkrate) / (float)Mathf.Log(2);
            deltaHealth -= (75 + swatk) * atkrate + deltaHealth;
        }
        deltaHealth = deltaHealth * (1 - GetArmorDiscount(armorlevel) / 100.0f);
        if (mgatk != 0)
        {
            float mgrate = mgdef / mgatk;
            deltaHealth -= mgatk * (float)Mathf.Pow(0.2f, mgrate);
        }
        deltaHealth = deltaHealth + offset;
        Debug.Log(atk+"+" + def+"+"+deltaHealth);
        current_health += deltaHealth;
        if (current_health > maxhealth)
            current_health = maxhealth;
        if (deltaHealth < 0)
        {
            animator.SetTrigger("Hurt");
        }
        if (current_health >= 0)
        {
            TheHealthMask.instance.SetValue((float)current_health / (float)maxhealth);
            //Debug.Log((float)current_health / (float)maxhealth);
        }
        else
        {
            TheHealthMask.instance.SetValue(0);
            SavesMain.current_health = 0;
        }
        if ((current_health<0))
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 5);
            Invoke("FindTheEndding", 3.0f);
        }
    }
    public void FindTheEndding()
    {
        if ((!SavesMain.KaguraSummoned) && (current_health < 0))
            SceneManager.LoadScene("Caijiuduolian"); //还没回场景1见到神乐瑶就死了
        else if ((!SavesMain.KaguraSummoned) && (current_health >= 0))
            SceneManager.LoadScene("Guyongzhe");//不回一次城直接杀穿
        else if ((GateBreaker.GateBroken) && (transform.position.y < -67))
            SceneManager.LoadScene("Weiyiyigecongmingren");//带神乐瑶离开
        else //这回神乐瑶一定出来了
        {
            if ((SavesMain.KaguraDead != 0) && (current_health < 0))
                SceneManager.LoadScene("Nishenmedoubaohubuliao"); //...但是还是死了
            if ((SavesMain.KaguraDead != 0) && (current_health > 0))
                SceneManager.LoadScene("Beishenyueguangxianwannongyuguzhangzhijian"); //打败BOSS，但是神乐瑶死了
            if ((SavesMain.KaguraDead == 0) && (current_health < 0))
                SceneManager.LoadScene("Nishenmedoubaohubuliao");//你死了，那么然后呢，你猜
        }
    }
}

