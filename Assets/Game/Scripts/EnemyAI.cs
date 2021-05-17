using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{ 

    [SerializeField]
    private GameObject _EnemyExplosionPrefab;


    //variable for speed
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private AudioClip _clip;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
       
    {
        //move down 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
         
        //when off screen at bottom
        //respawn at the top with new x position between bounds of screen
        if (transform.position.y <= -6.34f)
        {
            float RandomX = Random.Range(-6.34f, 6.34f);
            transform.position = new Vector3(RandomX, 6.34f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
           
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
