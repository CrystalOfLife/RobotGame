    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerController : MonoBehaviour
    {
        public Text countText;
        public float speed;
        private float secondsCount;
        private int minuteCount;
        public Text timerText;
    public Text highScore;
    // Use this for initialization
    private Rigidbody rb;
    
        public Text WinText;
        public int count = 17;
        void Start ()
        {
            rb = GetComponent<Rigidbody>();
    SetCountText();
            WinText.text = "";
            countText.text = "" ;    
        UpdateTimerUI();
    
    }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
            if (Input.GetKeyDown("space"))
            {
                 transform.Translate(Vector3.up * 52 * Time.deltaTime, Space.World);
                            }
           
            if (Input.GetKeyDown("r"))
            {
                transform.position = new Vector3(0, 0, 0);
            }UpdateTimerUI();
            

    }

        public void UpdateTimerUI()
        {
        //set timer UI

        secondsCount += Time.deltaTime;
            if (count > 0)
            {
                timerText.text = "Your time " + minuteCount + "m:" + (int) secondsCount + "s";
            }
            if (secondsCount >= 60)
            {
                minuteCount++;
                secondsCount = 0;
            }
           
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                count = count - 1;
                speed = speed + 4;
    SetCountText();
            }else if (other.gameObject.CompareTag("Bad Pick Up"))
            {
                other.gameObject.SetActive(false);
                speed = speed + 15;
            }
            
        }

        void SetCountText()
        {
            countText.text = "Red cubes left: " + count.ToString();
            if (count == 0)
            {
             
                WinText.text = " You Win";
                
            }
        }
    }
