using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //Esto es pa poder mover el player
    private Rigidbody rb;
    public float movSpeed;
    private float movLateral;
    private float movFrontal;

    //Esto es pa gestionar el salto
    public float jumpForce;
    public bool isGrounded = false;
    public float jumpSpeed;
    public float fallSpeed;

    //Esto es pa mover la camara con el ratón
    public float mouseSensitivity;
    private float mouseRotation = 0f;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        // cogemos el componente pa mover el player
        rb = GetComponent<Rigidbody>();
        // centramos el cursos en pantalla y lo ocultamos
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // le damos el valor X a la rotacion de la camara con el cursor
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);
        // le damos el valor Y a la camara y lo bloqueamos en los polos
        mouseRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseRotation = Mathf.Clamp(mouseRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(mouseRotation, 0, 0);

        // con esto cogemos los controles del player
        movLateral = Input.GetAxisRaw("Horizontal");
        movFrontal = Input.GetAxisRaw("Vertical");

        // si pulsamos espacio y estamos tocando suelo, aplicamos el salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // aquí le damos los valores al transform cuando nos movemos
        Vector3 playerMovement = (transform.right * movLateral + transform.forward * movFrontal);
        Vector3 playerSpeed = new Vector3 (playerMovement.x * movSpeed, rb.velocity.y, playerMovement.z * movSpeed);
        rb.velocity = playerSpeed;

        // aumentamos la velocidad del salto al subir
        if (rb.velocity.y > 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * jumpSpeed * Time.fixedDeltaTime;
        }
        // aumentamos la gravedad al caer del salto
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallSpeed * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // compruebo haber colisionado con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("toco");
            isGrounded = true;
        }
    }

    void Jump()
    {
        //actualizamos el estado, la altura y damos la fuerza
        isGrounded = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
