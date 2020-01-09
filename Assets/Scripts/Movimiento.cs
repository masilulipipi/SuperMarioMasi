using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour {
    // VARIABLES MOVIMIENTOS
    public float velx = 0.1f;
    public float movX;
    public float posicionactual;
    public float imputX;

    // VARIABLES SALTO
    public float fuerzaSalto = 350f; // agregar fuerza para saltar
    public Transform pie;
    public float radioPie;
    public LayerMask suelo;
    public bool enSuelo;

    //ANIMACIONES
    Animator animator;

    public bool agachado;
    public bool mirarArriba;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        imputX = Input.GetAxis("Horizontal"); // Almacena el mov en el eje x
        if (!agachado && !mirarArriba)
        {
            if (imputX > 0)
            {
                movX = transform.position.x + (imputX * velx);
                transform.position = new Vector3(movX, transform.position.y, 0);
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); // para que se de vuelta 
                movX = posicionactual;
            }

            if (imputX < 0)
            {
                movX = transform.position.x + (imputX * velx);
                transform.position = new Vector3(movX, transform.position.y, 0);
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); // para que se de vuelta 
                movX = posicionactual;
            }
        }
        
        
        // para el animator

        if (imputX != 0 && enSuelo)
        {
            animator.SetFloat("velX", 1);
        }
        else
        {
            animator.SetFloat("velX", 0);
        }
                
        
        // SALTO 
        enSuelo = Physics2D.OverlapCircle(pie.position, radioPie, suelo); // comprobar si esta en el suelo...
        if (enSuelo)
        {
            animator.SetBool("enSuelo", true);

            if (Input.GetButtonDown("Jump") && !agachado)  // (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto)); // agregar fuerza para saltar
                animator.SetBool("enSuelo", false);
            }
        }
        else
        {
            animator.SetBool("enSuelo", false);
        }


        
        //AGACHARSE
        
        if (enSuelo && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Joystick1Button2))
        { 
            animator.SetBool("agachado", true);
            agachado = true;
        }
        else
        {
            animator.SetBool("agachado", false);
            agachado = false;
        }
        //MIRAR ARRIBA
        if (imputX == 0)
        {
            if (enSuelo && Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("mirarArriba", true);
                mirarArriba = true;
            }
            else
            {
                animator.SetBool("mirarArriba", false);
                mirarArriba = false;
            } 
        }

        

    }

}
