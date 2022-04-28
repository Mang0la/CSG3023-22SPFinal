/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: 
 * Last Edited:
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    private int numBalls = 3;
    public float score = 0;
    public Text ballTxt;
    public Text scoreTxt;

    [Header("Ball Settings")]
    public Vector3 initialForce = new Vector3(0, 10, 0);
    public float speed = 10f;
    public GameObject paddle;
    private bool isInPlay = false;
    private Rigidbody rb;
    private AudioSource audioSource;



 


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }//end Awake()


    // Start is called before the first frame update
    void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        if (ballTxt)
        {
            ballTxt.text = "Balls: " + numBalls;
        }
        
        if (scoreTxt)
        {
            scoreTxt.text = "Score: " + score;
        }

        if (!isInPlay)
        {
            SetStartingPos();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isInPlay)
        {
            isInPlay = true;
            Move();
        }

    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            rb.velocity = speed * rb.velocity.normalized;
        }

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false; //ball is not in play
        rb.velocity = Vector3.zero; //set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddle
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    private void Move()
    {
        rb.AddForce(initialForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();

        if (collision.gameObject.tag == "Brick")
        {
            score += 100;
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "OutBounds")
        {
            numBalls--;
            if (numBalls > 0)
            {
                Invoke("SetStartingPos", 2f);
            }
        }
    }




}
