using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 for Triple shot, 1 for Speed, 2 For Shields
    [SerializeField]
    private AudioClip _clip;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

       if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with:" + other.name);

       if(other.tag == "Player")
        {
            //access the player 
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                //Enable Triple shots
                // if Powerup ID == 0
                if(powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
               else if(powerupID == 1)  // if Powerup ID == 1
                {
                    // Enable Speed Boost
                    player.SpeedBoostPowerupOn();
                }
                else if (powerupID == 2) // if Powerup ID == 2
                {
                    // Enable Shields
                    player.shieldsPowerupOn();

                }

            }
            //destroy our self
            Destroy(this.gameObject);
        }
    }
}
