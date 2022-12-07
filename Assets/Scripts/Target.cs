using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    
    private GameManager gameManage;
    private Rigidbody targetRb;
    private float minSpeed = 15;
    private float maxSpeed = 22;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -2f;
    private AudioSource failed;
    

    private AudioSource crunch;


    public ParticleSystem explosionParticle;

    public int pointValue;
    
    // Start is called before the first frame update
    void Start()
    {


        failed = GameObject.Find("Game Manager").GetComponent<AudioSource>();
        crunch = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        targetRb = GetComponent<Rigidbody>();
        gameManage = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        

        float randomForce = Random.Range(minSpeed, maxSpeed);
        float randomTorque = Random.Range(-maxTorque, maxTorque);
        float xPos = Random.Range(-xRange, xRange);
        
        transform.position = new Vector3(xPos, ySpawnPos, 0);
        targetRb.AddForce(Vector3.up * randomForce, ForceMode.Impulse);
        targetRb.AddTorque(randomTorque, randomTorque, randomTorque, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {

        
        
        if (!gameManage.isGameOver)
        {

            crunch.Play();
           
            gameObject.SetActive(false);
            Destroy(gameObject, 0.7f);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            gameManage.UpdateScore(pointValue);
            
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.7f);
        
        if (!gameObject.CompareTag("Bad") && !gameManage.isGameOver) 
        {
            failed.Play();
            gameManage.UpdateLives(-1);

        }
       
    }




}
