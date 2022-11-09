using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BlockController : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D rigidbody2d;
    [SerializeField] private GameObject block;  //This is to store the block object as a parent later on
    [SerializeField] private GameObject item;   //This is a game object that can spawn from this block
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite newSprite;  //This is the new sprite that this block will turn into after getting hit
    
    [SerializeField] private int blockHits = 1; //How many times the block can be hit before graying out
    private int playerHits = 0;    //How many times the player has hit the block
    private bool grayOut = false;   //Whether the block is grayed out or not


    // Start is called before the first frame update
    void Start()
    {
        //The conventional use 'my' refers to this object's values
        anim = GetComponent<Animator>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();

        //Transform myTransform = transform;
        //Vector3 myPos = transform.position;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        
    }


    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" && playerHits < blockHits){
            Debug.Log("enter block");
            transform.position += new Vector3(0, 0.75f, 0);
            playerHits++;
            
        }
    }

    void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" && grayOut == false){
            Debug.Log("exit block");
            transform.position -= new Vector3(0, 0.75f, 0);
            ChangeSprite();
        }
    }

    void ChangeSprite(){

        if (playerHits == blockHits && grayOut == false){
        spriteRenderer.sprite = newSprite;
        SpitItem();
        grayOut = true;
        }
    }


    void SpitItem(){
        if (playerHits == blockHits && grayOut == false){
        
        GameObject newObject = Instantiate(item);
        newObject.transform.SetParent(block.transform);
        newObject.transform.position = (block.transform.position);
        // newObject.GetComponent<Rigidbody2D>().velocity = block.transform.localScale.y * newObject.transform.right;
        newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rigidbody2d.velocity.x * 2, rigidbody2d.velocity.y * 2);

        

        }
    }

    // private IEnumerator WaitForAnim(){
    //     yield return new WaitForSeconds(1.0f);
    // }

}
