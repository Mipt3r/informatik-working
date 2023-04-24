using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    //enables the player to move and defines public uses like lives and gameobjects that need to be connected
    Rigidbody2D rb;
    public int lives = 3;
    public GameObject Player;
    public Transform RespawnPoint;
    //why is nothing working
    public int DeathTime;

    public Collider2D objectCollider;
    public Collider2D anotherCollider;
    private Points points;
    private Text itemText;
    private GameObject itemOutline;
    private List<GameObject> itemList = new List<GameObject>();
    public GameObject explosionAttack;
    public GameObject healEffect;
    public float itemCooldown = 2f;
    private float currentItemCooldown = 0;

    //edited values for making the respawn mechanic work properly
    public float movementSpeed;
        public float movementJump;

    //Movement
        float speed;
        float jump;
        float moveVelocity;
    void Start()
    {
        itemText = GameObject.FindWithTag("ItemText").GetComponent<Text>();
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
        itemOutline = GameObject.FindWithTag("ItemOutline");
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        speed = movementSpeed;
        jump = movementJump;
    }
   

    //Grounded Vars
    bool isGrounded = true;
        
    void Update()
    {

        if (currentItemCooldown >= 0.1)
        {
            currentItemCooldown -= Time.deltaTime;
        }



        //checks if the player is touching the ground
        if (objectCollider.IsTouching(anotherCollider))
        {
           isGrounded = true;
        }
        else
        {
         isGrounded = false;
        }
           
        //Jumping
        if (Input.GetButton("Jump"))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }

        }

        if (lives <= 0)
        {
            StartCoroutine(Dead());
        }

        IEnumerator Dead()
            {
            Debug.Log("dead");
            speed = 0;
            jump = 0;
            GetComponent<Renderer>().enabled = false;
            Player.transform.position = RespawnPoint.position;
            lives = 3;
            yield return new WaitForSeconds(DeathTime);
            speed = movementSpeed;
            jump = movementJump;
            Debug.Log("respawn");
            GetComponent<Renderer>().enabled = true;
            
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetButton("Left"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
            moveVelocity = -speed;
        }
        if (Input.GetButton("Right"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            moveVelocity = speed;
        }

        //nick: cycles forward in itemList
        if (Input.GetButtonDown("Item forw") && itemList.Count > 0)
        {
            itemList.Add(itemList[0]);
            itemList.RemoveAt(0);
            UpdateItem();
        }

        //nick: cycles backward in itemList
        if (Input.GetButtonDown("Item backw") && itemList.Count > 0)
        {
            itemList.Insert(0, itemList[itemList.Count - 1]);
            itemList.RemoveAt(itemList.Count - 1);
            UpdateItem();
        }

        if (Input.GetButtonDown("Item use")  && itemList.Count > 0 && currentItemCooldown <= 0.1f)
        {
            if (itemList[0].name == "EXPLOSION")
            {
                Instantiate(itemList[0].GetComponent<ItemScript>().effect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("Weapon"), 0.25f);
            }

            if (itemList[0].name == "HEAL" && points.currentPoints >= 100)
            {
                Instantiate(itemList[0].GetComponent<ItemScript>().effect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                lives += 1;
                Destroy(GameObject.FindGameObjectWithTag("Heal"), 0.5f);
                points.GetPoints(-100);
            } 
            currentItemCooldown += itemCooldown;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //nick: adds item to itemList when touching an item
        if (collision.gameObject.tag == "Item")
        {
            itemList.Insert(0, collision.gameObject);
            collision.gameObject.transform.position = new Vector2(-300, -300);
            UpdateItem();
        }
    }

    void UpdateItem()
    {
        Destroy(GameObject.FindWithTag("ItemUI"));
        var equippedItem = Instantiate(itemList[0].GetComponent<ItemScript>().UI, new Vector2(itemOutline.transform.position.x, itemOutline.transform.position.y), Quaternion.identity);
        equippedItem.transform.SetParent(itemOutline.transform);
        itemText.text = itemList[0].name;
    }
}