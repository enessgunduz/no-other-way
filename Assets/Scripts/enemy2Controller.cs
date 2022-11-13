using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Controller : MonoBehaviour
{
    private bool saldiriyorMu = false;
    public Vector3 current = Vector3.left;
    public GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(!saldiriyorMu){
            transform.Translate(current * Time.deltaTime ,Space.World);
            this.transform.Find("model").GetComponent<Animator>().Play("Run");
        } 

        int layerMask = 1<<8;
        if(Physics2D.Raycast(transform.position,current,1f,layerMask)){
            //ateş işlemleri    
            Debug.Log("piyuv pituv");
            saldiriyorMu = true;
            
            this.transform.Find("model").GetComponent<Animator>().Play("Attack");
            mainCharacter.transform.position = new Vector3((float)-8.72,(float)-2.26,0);
            //Debug.Log("çarptı");
        } else {
            saldiriyorMu = false;
            
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

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
        
        }

        if(other.CompareTag("mermi")){
            Destroy(gameObject);
        }

        
    }
}
