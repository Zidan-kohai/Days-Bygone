using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun")]
    public float offSet;
    public GameObject bullet;
    public Transform shootPoint;
    private float timeBtwShot;
    public float startTimeBtwShot;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offSet);

        if(timeBtwShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shootPoint.position, transform.rotation);
                timeBtwShot = startTimeBtwShot;
            }
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
}