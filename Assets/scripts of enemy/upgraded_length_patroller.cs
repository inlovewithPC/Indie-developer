using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgraded_length_patroller : MonoBehaviour
{
    public float mMovementSpeed = 1.5f;
    public bool bIsGoingRight = true;
    public float mRaycastingDistance = 1f;
    private SpriteRenderer _mSpriteRenderer;
    public float length_timer = 6;
    // Start is called before the first frame update
    public string id = "ABC";
    public bool flipstate = false;
    private float k;
    private enum State
    {
        patrol,
        shooting,
    }
    private State state;
    private void Awake()
    {
        state = State.patrol;
    }
    void Start()
    {
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = !bIsGoingRight;
            k = mMovementSpeed;


    }


    void Update()
    {

        
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;

        Vector3 raycastDirection = (bIsGoingRight) ? Vector3.right : Vector3.left;
        if (flipstate)
        {
            state = State.shooting;
        }
        else
        {
            state = State.patrol;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f);
        switch (state)
        {
            default:
            case State.patrol:
                {
                    // if the ennemy is going right, get the vector pointing to its right

                    mMovementSpeed = k;

                    directionTranslation *= Time.deltaTime * mMovementSpeed;
                    transform.Translate(directionTranslation);

                    if (hit.collider != null)
                    {
                        if (hit.transform.tag == "Terrain")
                        {
                            bIsGoingRight = !bIsGoingRight;
                            _mSpriteRenderer.flipX = !bIsGoingRight;

                        }
                    }
                    if (length_timer > 0)
                        StartCoroutine("delay_patroller");

                }; break;

            case State.shooting:
                {
                    mMovementSpeed = 0;
                    directionTranslation *= Time.deltaTime * mMovementSpeed;
                    transform.Translate(directionTranslation);



                }; break;


        }





        Enemy_zone z = GameObject.Find(id).GetComponent<Enemy_zone>();     
            flipstate = z.enabled;
 
        
        // for (int i=0;i<5;i++)





    }

    IEnumerator delay_patroller()
    {
        yield return new WaitForSeconds(length_timer);
        transform.Translate(Vector3.zero);
        bIsGoingRight = !bIsGoingRight;
        _mSpriteRenderer.flipX = !bIsGoingRight;
        StopCoroutine("delay_patroller");
        //StopAllCoroutines();
    }



}

