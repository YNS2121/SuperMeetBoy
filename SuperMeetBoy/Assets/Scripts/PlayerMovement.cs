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
        SceneManager.LoadScene("GameOver");//gameover ekraný yüklenecek
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("Movement");//ilk sahne yüklenecek
    }
    void Start() //oyun baþladýðýndaki tanýmlamalarýmý yaptým.
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
        if(joystick.Horizontal > 0.2f) //joystick'im yatay yönde 0.2f den fazla olursa
        {
            horizontalMoveSpeed = 1;
        }
        else if(joystick.Horizontal < -0.2f) //joystick'im yatay yönde -0.2f den fazla olursa
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
        
        if (joystick.Vertical > 0.6f && !animator.GetBool("IsJumping")) //joystick'im dikey yönde 0.5f den fazla olursa zýplama animasyonuna geç
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
        if (collision.CompareTag("ground")) //yer olarak tanýmladýðým bölgeye deðdiðinde yani aþaðý düþtüðünde oyun bitecek.
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            animator.SetBool("IsJumping", false);
        }else if (collision.gameObject.tag == "gear" || collision.gameObject.CompareTag("thorn"))//gear ya da thorn engellerine deðdiðinde yanacak.
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
