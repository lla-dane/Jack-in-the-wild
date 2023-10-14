using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private GameObject bullet;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        bullet = Object_Pooling.Instance.Spawn_Object("Bullet",this.transform.position);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - bullet.transform.position).normalized;

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
