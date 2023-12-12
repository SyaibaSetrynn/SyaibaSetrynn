using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackRangeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
 //       EnemyController controller = other.GetComponent<EnemyController>();
 //       if (MainController.attack_time!=0)
  //      {
 //           EnemyChangeHealth(Saves.attack, Saves.swordattack, controller.defence, 0, controller.magic_defence, false, 0);
  //      }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 90*(SavesMain.toward-1));
    }
}
