using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float jumpPower;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private LayerMask wallLayer;
   public float baseSpeed;
   public float dashPower;
   public float dashTime;
   private Rigidbody2D body;
   private Animator anim;
   private BoxCollider2D boxCollider;
   private float wallJumpCooldown;
   private float HorizontalInput;
  
   bool isDashing = false;

   private void Awake(){

      speed = baseSpeed;

       body = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       boxCollider = GetComponent<BoxCollider2D>();

   }

   private void Update(){



       HorizontalInput = Input.GetAxis("Horizontal");
       body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);


//obrne karakterja levo-desno
      if(HorizontalInput > 0.01f)
         transform.localScale = Vector3.one;
      else if (HorizontalInput < -0.01f)
      transform.localScale = new Vector3(-1, 1, 1);


      

   anim.SetBool("run", HorizontalInput != 0);
   anim.SetBool("grounded", isGrounded());



   if(wallJumpCooldown > 0.1f)
   {
      

      body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

      if(onWall() && !isGrounded())
      {
         body.gravityScale = 0;
         body.velocity = Vector2.zero;
      }
      else
         body.gravityScale = 5;

      if (Input.GetKey(KeyCode.Space))
      Jump();
   }
   else wallJumpCooldown += Time.deltaTime;



      if (Input.GetKeyDown(KeyCode.LeftShift))
      {
         if(!isDashing)
         {
            StartCoroutine(Dash());
         }
      }

      print(onWall());

   }

   private void Jump()
    {
       if(isGrounded())
       {
          body.velocity = new Vector2(body.velocity.x, jumpPower);
       anim.SetTrigger("jump");
       }
       else if(onWall() && !isGrounded())
       {
          if(HorizontalInput == 0)
          {
             body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
             transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
          }
          else
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);


          wallJumpCooldown = 0;
          
       }
       
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

   IEnumerator Dash()
   {
      isDashing = true;
      speed *= dashPower;

      yield return new WaitForSeconds(dashTime);

      speed = baseSpeed;
      isDashing = false;

   }

   private bool isGrounded()
   {
      RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
      return raycastHit.collider != null;
   }

   private bool onWall()
   {
      RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
      return raycastHit.collider != null;
   }

}
