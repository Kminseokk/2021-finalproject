using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObj : MonoBehaviour
{
    public int maxHealth;
    public int nowHealth;

    Rigidbody rigid;
    BoxCollider boxcollider;
    Material mat;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;  

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            nowHealth -= weapon.damage;
            
            StartCoroutine(OnDamage());
            //Debug.Log("Melee : " + nowHealth);
        }
        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            nowHealth -= bullet.damage;

            Destroy(other.gameObject);

            StartCoroutine(OnDamage());
            //Debug.Log("Range : " + nowHealth);
        }

    }


    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (nowHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 15; //layer 15번 으로 설정

            Destroy(gameObject, 1);
        }
    }

}
