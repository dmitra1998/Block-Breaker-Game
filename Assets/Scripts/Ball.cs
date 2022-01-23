using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f, yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;


    bool hasStarted = false;

    //state
    Vector2 paddleToBallVector;

    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    GameSession ballSpeed;
    //float speed;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        //speed = ballSpeed.GameSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
      //  myRigidBody2D.velocity = speed * (myRigidBody2D.velocity.normalized);
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void LaunchOnMouseClick()
    { 
        if(Input.GetMouseButtonDown(0))//0 for left mouse button, 1 for right mouse button and 2 for middle mouse button
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);//x and y values respectively
            hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if(hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0,ballSounds.Length)];//randomly selects songs to be played
            myAudioSource.PlayOneShot(clip);//PlayOneShot can play multiple sounds without cutting each other off.
                                            //On the flipside however that means* you can't stop the audio clip either; 
                                            //it'll just play all the way through, with no way to stop it early.
            myRigidBody2D.velocity += velocityTweak;
            //myRigidBody2D.velocity = new Vector2(xPush, yPush);//x and y values respectively
        }
        
    }

}
