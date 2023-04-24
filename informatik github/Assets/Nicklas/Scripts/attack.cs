using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject hitBox;
    public GameObject weaponThrown;
    public float throwVelocity = 5;
    public float cooldown = 0.25F;
    public float attackTime = 0.1f;

    

    private float currentCooldown = 0;
    private Transform player;
    private string thrownWeaponTag = "ThrownWeapon";
    private string weaponTag = "Weapon";
    private Points points;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown >= 0.1)
        {
            currentCooldown -= Time.deltaTime;
        }

        
        if (currentCooldown <= 0.1 && Input.GetButtonDown("Attack") && GameObject.FindGameObjectsWithTag(thrownWeaponTag).Length == 0)
        {
            Destroy(GameObject.FindWithTag(weaponTag), 0F);
            Instantiate(hitBox, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).transform.parent = transform;
            Destroy(GameObject.FindWithTag(weaponTag), attackTime);
            currentCooldown += cooldown;
        }

        if (Input.GetButtonDown("Throw"))
        {
            if (currentCooldown <= 0.5 && GameObject.FindGameObjectsWithTag(thrownWeaponTag).Length == 0)
            {
                Instantiate(weaponThrown, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x - player.position.x, 0) * throwVelocity;
                currentCooldown += cooldown;
            }
            else
            {
                if (currentCooldown <= 0.5 && !GameObject.FindGameObjectWithTag(thrownWeaponTag).GetComponent<Animator>().GetBool("returning"))
                {
                    GameObject.FindGameObjectWithTag(thrownWeaponTag).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    GameObject.FindGameObjectWithTag(thrownWeaponTag).GetComponent<Animator>().SetBool("returning", true);
                }
            }
        }

       
    }
}
