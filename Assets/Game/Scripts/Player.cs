using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	public bool canTripleShot = false;
	public bool canSpeedBoost = false;
	public bool canActiveShields = false;
	public bool isPlayerOne = false;
	public bool isPlayerTwo = false;

	public int lives = 3;

	//Public or Private Identifier
	//Data Type (int, float, bool, string)
	//Variable Name
	//Optional Value Assinged

	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _TripleShotPrefab;
	[SerializeField]
	private GameObject _ExplosionPrefab;
	[SerializeField]
	private GameObject _ShieldsGameobject;


	[SerializeField]
	private GameObject[] _engines;



	[SerializeField]
	private float _fireRate = 0.65f;

	private float _canFire = 0.0f;

	//firerate is 0.25f
	//canfire -- has The amount of time between firing passed?
	//Time.time

	[SerializeField]
	private float _speed = 5.0f;
	[SerializeField]
	private float _Acceleration = 4.0f;
	private UIManager _uiManager;
	private GameManager _gameManager;
	private AudioSource _audioSource;

	private int HitCount = 0;

	// Start is called before the first frame update
	private void Start()
	{

	    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

		if(_uiManager != null)
		{
			_uiManager.UpdateLives(lives);

		}

		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		_audioSource = GetComponent<AudioSource>();

		HitCount = 0;

		if (_gameManager.IsCoopMode == false)
		{
			//Currenst Position = New Position
			transform.position = new Vector3(0, 0, 0);
		}
	}



	// Update is called once per frame
	private void Update()
	{
		if(isPlayerOne == true)
		{
			Movements();


		#if UNITY_ANDROID
			if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && isPlayerOne == true)
			{
				Shoot();
			}
#elif UNITY_IOS

			if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) && isPlayerOne == true))
			{
				Shoot();
			}

#else

		  if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) && isPlayerOne == true))
			{
				Shoot();
			}  
		

#endif

		}

		//if player two
		
		if(isPlayerTwo == true)
			{
				PlayerTwoMovements();
				if (Input.GetKeyDown(KeyCode.KeypadEnter))
				{
					Shoot();
				}

		}

	}

	private void Shoot()
	{
		//if TripleShot 
		//Shot 3 laser
		//else
		//Shoot 1


		if (Time.time > _canFire)
		{
			_audioSource.Play();
			if (canTripleShot == true)
			{
				Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
			}
			else
			{
				Instantiate(_laserPrefab, transform.position + new Vector3(0.003f, 1.050f, 0), Quaternion.identity);

			}

			_canFire = Time.time + _fireRate;
		}


	}


	private void Movements()
	{
		float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");    //Input.GetAxis("Horizontal");
		float VerticalInput = CrossPlatformInputManager.GetAxis("Verticle");  //Input.GetAxis ("Vertical");

		//if speed boost enable
		//move 1.5x normal speed
		//else
		//move normal speed

		if(canSpeedBoost == true)
		{
			transform.Translate(Vector3.right * _speed * 2.0f * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up * _Acceleration * 2.0f * VerticalInput * Time.deltaTime);

		}
		else if(canSpeedBoost == false)
		{
			transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up * _Acceleration * VerticalInput * Time.deltaTime);

		}


		//if Player on the Y is greater Than 1.50
		//set player position to 1.50
		if (transform.position.y > 1.50f) {
			transform.position = new Vector3 (transform.position.x, 1.50f, 0);
		} else if (transform.position.y < -4) {
			transform.position = new Vector3 (transform.position.x, -4, 0);
		}



		//if Player on the x is greater Than 9.58
		//set player position to -9.58
		if (transform.position.x > 9.58f) {
			transform.position = new Vector3 (-9.5f, transform.position.y, 0);
		} else if (transform.position.x < -9.58f) {
			transform.position = new Vector3 (9.5f, transform.position.y, 0);
		}
	}

	private void PlayerTwoMovements()
	{

		//if speed boost enable
		//move 1.5x normal speed
		//else
		//move normal speed

		if (canSpeedBoost == true)
		{
			//if hit 8, move up
			if (Input.GetKey(KeyCode.Keypad8))
			{
				transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime);
			}
			//if hit 6 key, move right
			if (Input.GetKey(KeyCode.Keypad6))
			{
				transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime);
			}
			//if hit 2, move up
			if (Input.GetKey(KeyCode.Keypad2))
			{
				transform.Translate(Vector3.down * _speed * 1.5f * Time.deltaTime);
			}
			//if hit 4 key, move left
			if (Input.GetKey(KeyCode.Keypad4))
			{
				transform.Translate(Vector3.left * _speed * 1.5f * Time.deltaTime);
			}

		}
		else if (canSpeedBoost == false)
		{

			//if hit 8, move up
			if (Input.GetKey(KeyCode.Keypad8))
			{
				transform.Translate(Vector3.up * _speed * Time.deltaTime);
			}
			//if hit 6 key, move right
			if (Input.GetKey(KeyCode.Keypad6))
			{
				transform.Translate(Vector3.right * _speed * Time.deltaTime);
			}
			//if hit 2, move up
			if (Input.GetKey(KeyCode.Keypad2))
			{
				transform.Translate(Vector3.down * _speed * Time.deltaTime);
			}
			//if hit 4 key, move left
			if (Input.GetKey(KeyCode.Keypad4))
			{
				transform.Translate(Vector3.left * _speed * Time.deltaTime);
			}

		}


		//if Player on the Y is greater Than 1.50
		//set player position to 1.50
		if (transform.position.y > 1.50f)
		{
			transform.position = new Vector3(transform.position.x, 1.50f, 0);
		}
		else if (transform.position.y < -4)
		{
			transform.position = new Vector3(transform.position.x, -4, 0);
		}



		//if Player on the x is greater Than 9.58
		//set player position to -9.58
		if (transform.position.x > 9.58f)
		{
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.58f)
		{
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}
	}

	public void Damage()
	{
		//if shield are on 
		//do nothing


		if (canActiveShields == true)
		{
			canActiveShields = false;
			_ShieldsGameobject.SetActive(false);
			return;
				
		}


		HitCount++;

		if (HitCount == 1)
		{
			//Turn left Engine Failure on
			_engines[0].SetActive(true);
		}

		else if (HitCount == 2)
		{
			//Turn Right Engine Failure on
			_engines[1].SetActive(true);
		}



		//Subtract 1 life from the player 
		lives--;
		_uiManager.UpdateLives(lives);
		//if life less than < 1(meaning zero)
		//destroy player
		if (lives < 1)
		{
			Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen();
			_uiManager.CheckForBestScore();
			Destroy(this.gameObject);
		}

	}


	public void TripleShotPowerupOn()
	{
		canTripleShot = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	//method to enable the powerup

		public void SpeedBoostPowerupOn()
	{
		canSpeedBoost = true;
		StartCoroutine(SpeedBoostPowerDownroutine());
	}

	public void  shieldsPowerupOn()
	{
		canActiveShields = true;
		_ShieldsGameobject.SetActive(true);
	}

	public IEnumerator SpeedBoostPowerDownroutine()
	{
		yield return new WaitForSeconds(5.0f);
		canSpeedBoost = false;
	}

	public IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		canTripleShot = false;

	}
}
