using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerContollerScene2 : MonoBehaviour
{
    protected float m_MoveX;
    
    public GameObject mermi;
    protected bool isDead = false;
    protected bool Is_DownJump_GroundCheck = true;
    private Rigidbody2D m_rigidbody;
    
    private Animator animator;
    protected bool atesEdildi;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        animator.SetBool("saldiriyorMu?", true);
    }

    // Update is called once per frame
    
    private void Update() {
        
        

        checkInput();
        if(m_MoveX == 0){
            animator.SetBool("saldiriyorMu?", true);
        } else {
            animator.SetBool("saldiriyorMu?", false);
        }
    }

    protected Vector2 yon = Vector2.right;
    bool c = true;
    private void FixedUpdate() {
        m_MoveX = Input.GetAxis("Horizontal");

        if(m_MoveX>0){
            yon = Vector2.right;
            Debug.Log("yön sağ oldu");
        } else if (m_MoveX<0){
            yon = Vector2.left;
            Debug.Log("yön sol oldu");
        }

        if(atesEdildi){
            mermi.transform.Translate(yon * 6 * Time.deltaTime);
            if(c){
                StartCoroutine(mermiSuresi());
                c = false;
            }
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"))
            {
                Is_DownJump_GroundCheck = true;

            }

        if(other.gameObject.CompareTag("Deadzone")){
            transform.position = new Vector3((float)-8.72,(float)-2.26,0);
        }
        if(other.gameObject.CompareTag("endgame")){
            SceneManager.LoadScene(0);
        }
    }


    void checkInput(){

        
        
        if (Input.GetKey(KeyCode.Mouse0)){
            atesEdildi = true;
        }

        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A))){
            transform.transform.Translate(Vector2.right* m_MoveX * 6 * Time.deltaTime);
            if(transform.localScale.x<0){
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D))){
            transform.transform.Translate(Vector2.right * m_MoveX * 6 * Time.deltaTime);
            if(transform.localScale.x>0){
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            
            Debug.Log("1");
            if(Is_DownJump_GroundCheck){
                Debug.Log("2");
                Is_DownJump_GroundCheck = false;
                Debug.Log(Is_DownJump_GroundCheck);
                m_rigidbody.velocity = new Vector2(0, 0);

                m_rigidbody.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            }
        }
        
    }

    IEnumerator mermiSuresi(){
        yield return new WaitForSeconds(1.5f);
        atesEdildi = false;
        mermi.transform.position = new Vector3(transform.position.x, transform.position.y+0.5f,transform.position.z);
        c = true;
    }
    
}
