using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public float speed = 1.0f;
    private float powerUpStrength = 5.0f; 
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public GameObject powerUpIndicator; 
    



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); 

        playerRb.AddForce (focalPoint.transform.forward * speed * forwardInput);

        powerUpIndicator.transform.position = transform.position + new Vector3 (0, -0.5f, 0) ; 
    }

    private void OnTriggerEnter (Collider other)
    { 
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(powerUpCountDown()); 
            powerUpIndicator.SetActive(true);

        }
    }

    private void onCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag ("Enemy") && hasPowerup ) 
            {

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); 
            Debug.Log("Collided with " + collision.gameObject.name + " wih " + hasPowerup );
           


        }
    }

    IEnumerator powerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerUpIndicator.SetActive(false);
    }


}
