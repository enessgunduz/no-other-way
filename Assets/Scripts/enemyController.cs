using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject mermi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public Vector3 current = Vector3.right;
    private bool saldiriyorMu = false;
    private bool mermiYolda = false;
    void FixedUpdate()
    {
        
        if(!saldiriyorMu){
            transform.Translate(current * Time.deltaTime ,Space.World);
        } 
        

        int layerMask = 1<<8;
        if(Physics2D.Raycast(transform.position,current,6f,layerMask)){
            //ateş işlemleri    
            if(mermiYolda == false){
            StartCoroutine(mermiFunc());
            }
            saldiriyorMu = true;
            GetComponent<Animator>().SetBool("saldiriyorMu?", true);
            //Debug.Log("çarptı");
        } else {
            saldiriyorMu = false;
            GetComponent<Animator>().SetBool("saldiriyorMu?", false);
        }

        if(mermiYolda){
            mermi.transform.Translate(current *Time.deltaTime,Space.World);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("enemyborder")){
            
           
            if(current == Vector3.right){
                current = Vector3.left;
            }
                
            else {
                current = Vector3.right;
            }

            gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y + 180,
                gameObject.transform.eulerAngles.z
            );
        
        
        }

        
    }

    IEnumerator mermiFunc(){
        Debug.Log("bura çalştı");
        mermiYolda = true;
        yield return new WaitForSeconds(6);
        mermiYolda = false;
        mermi.transform.position = new Vector3(transform.position.x, transform.position.y+0.5f,transform.position.z);
        
    }
    
}
