using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 10.0F;
    public Transform destroyPos;
    public AudioClip clipExplode;
    public AudioClip clipUpgrade;
    public AudioClip clipDowngrade;
    public AudioClip clipVictory;
    public AudioClip clipPickup;
    public int scene;
    private int life = 5;
    private AudioSource audio;
    private bool wallActive = false;
    private bool movingWallActive = false;
    private float jumpTimer = 0;
    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;
    public Text livesText;
    private float secondsCount;
    private int minuteCount;
    public Text timerText;
    private float previousSpeed = 1;
    private float newSpeed = 1;
    private bool stoptime = false;
    private bool upgradeCollected = false;
    private int currentScene;
    public Text highScore;
    private float hiscoreTime;

   

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        audio = this.gameObject.GetComponent<AudioSource>();
        UpdateTimerUI();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 1)
        {
            highScore.text = "Fastest time: " + PlayerPrefs.GetInt("HighScore_lvl1", 999).ToString() + " seconds";
            noHighscore();
        
        }

        if (currentScene == 2)
        {
            highScore.text = "Fastest time: " + PlayerPrefs.GetInt("HighScore_lvl2", 999).ToString() + " seconds";
            noHighscore();
        }
        if (currentScene == 3)
        {
            highScore.text = "Fastest time: " + PlayerPrefs.GetInt("HighScore_lvl3", 999).ToString() + " seconds";
            noHighscore();
        }
    }
   public void noHighscore()
    {
        if (highScore.text == "Fastest time: " + 999 + " seconds")
        {
            highScore.text = "";
        }
    }
    IEnumerator nextLevel()
    {
        
        audio.clip = clipVictory;
        audio.Play();
        yield return new WaitForSeconds(clipVictory.length);
        SceneManager.LoadScene(scene);

        if (scene == 0)
        {
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                jumpTimer = 1;
                anim.SetBool("Jumping", true);
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                previousSpeed = speed;
                speed = 1;
                UpDownGradeSound();
                newSpeed = speed;
                jumpSpeed = 8;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                previousSpeed = speed;
                speed = 8;
                UpDownGradeSound();
                newSpeed = speed;
                jumpSpeed = 12;

            }
            if (Input.GetKey(KeyCode.Alpha3) && upgradeCollected == true && currentScene == 1 || Input.GetKey(KeyCode.Alpha3) && currentScene >= 2)
            {
                previousSpeed = speed;
                speed = 12;
                UpDownGradeSound();
                newSpeed = speed;
                jumpSpeed = 16;
            }
            if (Input.GetKey(KeyCode.Alpha4) && upgradeCollected == true && currentScene == 2 || Input.GetKey(KeyCode.Alpha4) && currentScene >= 3)
            {
                previousSpeed = speed;
                speed = 15;
                newSpeed = speed;
                jumpSpeed = 20;
                UpDownGradeSound();

            }
            if (Input.GetKey(KeyCode.Alpha5) && upgradeCollected == true && currentScene == 3)
            {
                previousSpeed = speed;
                speed = 20;
                UpDownGradeSound();
                newSpeed = speed;
                jumpSpeed = 30;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                setInfoText();
            }
            else
            {
                livesText.text = "Lives remaining " + life;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (jumpTimer > 0.5) jumpTimer -= Time.deltaTime;
        else if (anim.GetBool("Jumping") == true) anim.SetBool("Jumping", false);

        //Rotate Player
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        anim.SetFloat("Speed", speed);

        if (this.transform.position.y <= destroyPos.position.y)
        {
            Death();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||  
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            speed = newSpeed;
        }
        else
        {
            if (speed != 0)
            {
                newSpeed = speed;
            }
            speed = 0;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            Cursor.visible = true;
        }
    }
    void setInfoText()
    {
        livesText.text = "Press 1,2,3,4 or 5 to increase your speed and jump";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            wallActive = true;
            if (movingWallActive == true)
            {
                Death();
            }
        }

        if (other.gameObject.tag == "Oil")
        {
            other.gameObject.SetActive(false);
            stoptime = true;
            if (hiscoreTime < PlayerPrefs.GetInt("HighScore_lvl1", 999) && currentScene == 1)
            {
                int Score = Mathf.CeilToInt(hiscoreTime);
                PlayerPrefs.SetInt("HighScore_lvl1", Score);
                highScore.text = "Fastest time: " + hiscoreTime.ToString() + " seconds";
            }
                if (hiscoreTime < PlayerPrefs.GetInt("HighScore_lvl2", 999) && currentScene == 2)
                {
                    int Score = Mathf.CeilToInt(hiscoreTime);
                    PlayerPrefs.SetInt("HighScore_lvl2", Score);
                    highScore.text = "Fastest time: " + hiscoreTime.ToString() + " seconds";
                }
            if (hiscoreTime < PlayerPrefs.GetInt("HighScore_lvl3", 999) && currentScene == 3)
            {
                int Score = Mathf.CeilToInt(hiscoreTime);
                PlayerPrefs.SetInt("HighScore_lvl3", Score);
                highScore.text = "Fastest time: " + hiscoreTime.ToString() + " seconds";
            }
            speed = 0;
            previousSpeed = 0;
            newSpeed = 0;
            StartCoroutine(nextLevel());
        }

        if (other.gameObject.tag == "Pick Up")
        {
            other.gameObject.SetActive(false);
            upgradeCollected = true;
            audio.clip = clipPickup;
            audio.Play();
        }


        if (other.gameObject.tag == "Moving Wall")
        {
            movingWallActive = true;
            if (wallActive == true)
            {
                Death();
            }
        }

        if (other.gameObject.tag == "Deadly")
        {
            Death();
        }
    }

    private void UpDownGradeSound()
    {
        if (newSpeed > speed)
        {
            audio.clip = clipDowngrade;
            audio.Play();
        }
        else if (newSpeed < speed)
        {
            audio.clip = clipUpgrade;
            audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Moving Wall")
        {
            movingWallActive = false;
        }

        if (other.gameObject.tag == "Wall")
        {
            wallActive = false;
        }
    }

    void Death()
    {
        audio.clip = clipExplode;
        audio.Play();
        life--;
        transform.position = new Vector3(0, 0, 0);
        //Destroy(this.gameObject);
        if (life == 0)
        {
            SceneManager.LoadScene(0);
            Cursor.visible = true;
        }
    }
        public void UpdateTimerUI()
        {
            //set timer UI

            secondsCount += Time.deltaTime;
        hiscoreTime += Time.deltaTime;
        if (stoptime == false)
            {
                timerText.text = "Your time " + minuteCount + "m:" + (int)secondsCount + "s";
            }
            if (secondsCount >= 60)
            {
                minuteCount++;
                secondsCount = 0;
            }

        }
    }
