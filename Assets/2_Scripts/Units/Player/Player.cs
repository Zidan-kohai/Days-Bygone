using Base.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Gun")]
        [SerializeField] private float offSet;
        [SerializeField] private GameObject gunOnHand;
        [SerializeField] private List<Bullet> bullets;
        [SerializeField] private Bullet currentBullet;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float timeBtwShot;
        [SerializeField] private float startTimeBtwShot;


        [SerializeField] private Animator animator;

        private void Start()
        {
            int weaponID = Data.Instance.CurrentWeaponId;

            currentBullet = bullets[weaponID];

            startTimeBtwShot = Data.Instance.GetWeaponData(weaponID).SpawnSpeed;
        }

        private void Update()
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            gunOnHand.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offSet);

            if (timeBtwShot <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    animator.SetTrigger("shoot");
                    timeBtwShot = startTimeBtwShot;
                }
            }
            else
            {
                timeBtwShot -= Time.deltaTime;
            }
        }
        private void Shoot()
        {
            Bullet bulletInstance = Instantiate(currentBullet, shootPoint.position, transform.rotation);
            bulletInstance.transform.rotation = gunOnHand.transform.rotation;

            int damage = Data.Instance.GetWeaponData(Data.Instance.CurrentWeaponId).Damage;
            float bulletSpeed = Data.Instance.GetWeaponData(Data.Instance.CurrentWeaponId).BulletSpeed;
            bulletInstance.Init(damage, bulletSpeed);
        }
    }

}