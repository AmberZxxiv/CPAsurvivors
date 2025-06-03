using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    //Esto es pa poder mover el player
    #region /////////PLAYER MOVEMENT/////////
    private Rigidbody rb;
    public float movSpeed;
    private float movLateral;
    private float movFrontal;
    #endregion

    //Esto es pa gestionar el salto
    public float jumpForce;
    public bool isGrounded = false;
    public float jumpSpeed;
    public float fallSpeed;

    //Esto es pa mover la camara con el ratón
    public float mouseSensitivity;
    private float mouseRotation = 0f;
    public Transform cameraTransform;
    public Transform lanternTransform;

    //Esto es pa que aparezcan las vidas y monedas en el HUD
    public int lifes;
    public int money;
    public GameObject health;
    public GameObject earned;
    public GameObject heartSprite;
    public GameObject coinSprite;

    //Esto pa los menus varios y victoria
    public GameObject pauseMenu;
    public GameObject deadMenu;
    public GameObject winMenu;
    public GameObject exitZone;

    // llamo al script del spawner de monedas
    public Spawner spawner;
    public Timer timer;
    public AI_Demon demon;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        // cogemos el componente pa mover el player
        rb = GetComponent<Rigidbody>();
        // centramos el cursos en pantalla y lo ocultamos
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // colocamos las vidas y monedas en pantalla
        for (int x = 0; x < lifes; x++) 
        {
            Instantiate(heartSprite, health.transform);
        }
        for (int x = 0; x < money; x++)
        {
            Instantiate(coinSprite, earned.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // cogemos el valor del cursor para poder darlo de vuelta
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);
        mouseRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseRotation = Mathf.Clamp(mouseRotation, -90f, 90f);
        // lo copiamos en la camara y la linterna, y lo bloqueamos en los polos
        cameraTransform.localRotation = Quaternion.Euler(mouseRotation, 0, 0);
        lanternTransform.localRotation = Quaternion.Euler(mouseRotation, 0, 0);


        // con esto cogemos los controles del player
        movLateral = Input.GetAxisRaw("Horizontal");
        movFrontal = Input.GetAxisRaw("Vertical");

        // si pulsamos espacio y estamos tocando suelo, aplicamos el salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // si pulsamos el ESC, panel de pausa y activamos cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // si llegas a 0 vidas o te caes, panel de muerte y activamos cursor
        if (lifes <= 0 || transform.position.y < -50)
        {
            demon.scareDemon.SetActive(false);
            Time.timeScale = 0;
            deadMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
    void Jump()
    {
        //actualizamos el estado del salto, la altura y damos la fuerza
        isGrounded = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // compruebo haber colisionado con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("toco");
            isGrounded = true;
        }
        // te suma una moneda e instancia en la UI
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            money += 1;
            Instantiate(coinSprite, earned.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // si llegas a la salida con 5 monedas, puntos, paneles y cursor
        if (other.CompareTag("EXIT") && money >= 5) 
        {
            timer.SetTimeToBeat();
            Time.timeScale = 0;
            winMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void QuitPause() // quita el panel de pausa, reanuda el tiempo y quita el cursor
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame() // recarga la escena de juego
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() // carga la escena de menu inicial
    {
        SceneManager.LoadScene(0);
    }
}
