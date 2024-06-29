using Base.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDawnWeapon : MonoBehaviour
{
    [SerializeField] private BulletDrop gun_00;
    [SerializeField] private BulletDrop gun_01;
    [SerializeField] private BulletDrop gun_02;
    [SerializeField] private BulletDrop gun_03;

    [SerializeField] private int Ypos = 10, maxXpos = -5, minXpos = 5;


    public int gun_00Chance = 100;
    public int gun_01Chance = 200;
    public int gun_02Chance = 300;
    public int gun_03Chance = 400;

    private void Start()
    {
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            if (!Data.Instance.SaveData.OpenWeaponId.Contains(0) && CheckDropDawn(gun_00Chance))
            {
                SpawnDropDawnGun(gun_00);
            }

            if (!Data.Instance.SaveData.OpenWeaponId.Contains(1) && CheckDropDawn(gun_01Chance))
            {
                SpawnDropDawnGun(gun_01);
            }

            if (!Data.Instance.SaveData.OpenWeaponId.Contains(2) && CheckDropDawn(gun_02Chance))
            {
                SpawnDropDawnGun(gun_02);
            }

            if (!Data.Instance.SaveData.OpenWeaponId.Contains(3) && CheckDropDawn(gun_03Chance))
            {
                SpawnDropDawnGun(gun_03);
            }
        }
    }

    private bool CheckDropDawn(int chance)
    {
        return UnityEngine.Random.Range(0, chance) < 1;
    }

    private void SpawnDropDawnGun(BulletDrop gun_00)
    {
        BulletDrop drop = Instantiate(gun_00);

        drop.transform.position = new Vector3(UnityEngine.Random.Range(minXpos, maxXpos), Ypos, 0);
    }
}
