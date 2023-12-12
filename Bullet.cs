using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    EnemyVirtue enemyVirtue;
    Animator animator;
    float timer = 0;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        animator = this.GetComponent<Animator>();
        int towards = SavesMain.toward;
        int level = SavesMain.bowlevel;
        animator.SetFloat("toward", towards / 3.0f);
        if (level<=2)
        {
            animator.SetTrigger("level");
        }else if (level<=4)
        {
            animator.SetTrigger("level1");
        }else
        {
            animator.SetTrigger("level2");
        }
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    private int getBowAttack(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 60;
        if (n == 2) return 120;
        if (n == 3) return 250;
        if (n == 4) return 270;
        if (n == 5) return 290;
        return 3000;
    }
    private int getSwordAttack(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 20;
        if (n == 2) return 70;
        if (n == 3) return 170;
        if (n == 4) return 190;
        if (n == 5) return 210;
        return 3000;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        enemyVirtue = other.GetComponent<EnemyVirtue>();
        L5BOSS boss=other.GetComponent<L5BOSS>();
        magma mag = other.GetComponent<magma>();
        marsh mar = other.GetComponent<marsh>();
        if (enemyVirtue!=null)
        {
            EnemyAttack enemyAttack = other.GetComponent<EnemyAttack>();
            EnemyLongRageAttack enemyLongRageAttack = other.GetComponent<EnemyLongRageAttack>();
            if (enemyAttack != null)
            {
                enemyVirtue.EnemyChangeHealth(SavesMain.attack, getBowAttack(SavesMain.bowlevel), enemyAttack.defence, 0, enemyAttack.magdefence, true, 0);
            }
            else if (enemyLongRageAttack != null)
            {
                enemyVirtue.EnemyChangeHealth(SavesMain.attack, getBowAttack(SavesMain.bowlevel), enemyLongRageAttack.defence, 0, enemyLongRageAttack.magdefence, true, 0);
            }
            Destroy(gameObject);
            //
            //other.enemy
        }
        else if (boss!=null)
        {
            boss.EnemyChangeHealth(SavesMain.attack, getBowAttack(SavesMain.bowlevel), boss.defence, 0, boss.magic_defence, true, 0);
            Destroy(gameObject);
        }
        else if ((timer>=0.00f)&&(mag==null)&&(mar==null))
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody2d.totalTorque = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
    }
}
