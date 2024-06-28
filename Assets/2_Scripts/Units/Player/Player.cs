using UnityEngine;

namespace Unit.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Gun")]
        [SerializeField] private float offSet;
        [SerializeField] private GameObject gunOnHand;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float timeBtwShot;
        [SerializeField] private float startTimeBtwShot;

        private void Update()
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            gunOnHand.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offSet);

            if (timeBtwShot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
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
}