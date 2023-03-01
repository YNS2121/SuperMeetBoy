using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
   // public GameOverScreen GameOverScreen;
    Rigidbody2D rgb; 
    Vector3 velocity;
    public Animator animator;
    public TextMeshProUGUI playerScoreText;
    public int score;
    public Joystick joystick;
    public LayerMask whatisGround;

    private float gravityStore;
    float horizontalMoveSpeed;
    float speedAmount = 5f;
    float jumpAmount = 5f;
    float gravityScale = 1f;

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");//gameover ekran� y�klenecek
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Movement");//ilk sahne y�klenecek
    }
    void Start() //oyun ba�lad���ndaki tan�mlamalar�m� yapt�m.
    {
        rgb = this.GetComponent<Rigidbody2D>();
        gravityStore = rgb.gravityScale;
        rgb = GetComponent<Rigidbody2D>();
        score = 0;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = score.ToString();
        if(joystick.Horizontal > 0.2f) //joystick'im yatay y�nde 0.2f den fazla olursa
        {
            horizontalMoveSpeed = 1;
        }
        else if(joystick.Horizontal < -0.2f) //joystick'im yatay y�nde -0.2f den fazla olursa
        {
            horizontalMoveSpeed = -1;
        }
        else
        {
            horizontalMoveSpeed = 0;
        }
        velocity = new Vector3(horizontalMoveSpeed, 0f);
        transform.position += velocity * speedAmount * Time.deltaTime;
        animator.SetFloat("speed", Mathf.Abs(joystick.Horizontal));
        
        if (joystick.Vertical > 0.6f && !animator.GetBool("IsJumping")) //joystick'im dikey y�nde 0.5f den fazla olursa z�plama animasyonuna ge�
        {
            rgb.AddForce(Vector3.up * jumpAmount, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
        if (animator.GetBool("IsJumping") && Mathf.Approximately(rgb.velocity.y, 0))
        {
            animator.SetBool("IsJumping", false);
        }
        if (horizontalMoveSpeed == -1) 
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(horizontalMoveSpeed == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground")) //yer olarak tan�mlad���m b�lgeye de�di�inde yani a�a�� d��t���nde oyun bitecek.
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            animator.SetBool("IsJumping", false);
        }else if (collision.gameObject.tag == "gear" || collision.gameObject.CompareTag("thorn"))//gear ya da thorn engellerine de�di�inde yanacak.
        {
            GameOver();
        }
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            animator.SetBool("IsJumping", true);
        }
    }
}
