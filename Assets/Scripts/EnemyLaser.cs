using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    float _speed = 10f;
    [SerializeField]
    float _laserLifeTime = 2f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        DestroyLaser();
    }

    void DestroyLaser()
    {
        Destroy(gameObject, _laserLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage();

            Destroy(this.gameObject);
        }
    }
}
