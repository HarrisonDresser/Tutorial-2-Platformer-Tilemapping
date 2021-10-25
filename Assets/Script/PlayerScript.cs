using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;

    public AudioSource playerAudioSource;

    public Text score;
    public int scoreValue = 0;

    public Text lives;
    private int livesValue = 3;

    public int R2 = 0;

    public GameObject winTextObject;
    public GameObject loseTextObject;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        lives.text = "lives: " + livesValue.ToString();

        score.text = scoreValue.ToString();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2( hozMovement * speed, verMovement * speed));
        

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject ThePlayer = GameObject.Find("Player 2"); 

        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (livesValue <= 0)
        {
            Destroy(ThePlayer);
            loseTextObject.SetActive(true);
        }

        if (scoreValue >= 4 && R2 == 0)
        {
            transform.position = new Vector3(85.0f, 1.0f, 0.0f); 
            R2 = 1;
            livesValue = 3;
            lives.text = "lives: " + livesValue.ToString();
        }
        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            
        }
    }
}
