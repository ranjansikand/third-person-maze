using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
	private Vector2 inputDirection = Vector2.zero;
	private Vector3 moveAngle, playerVelocity = Vector3.zero;
	private CharacterController controller;
	private Animator anim;

	[SerializeField] 
	private float playerSpeed = 4f;

	[SerializeField] 
	private float rotationSpeed = 8f;

	private float gravityValue = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveAngle = new Vector3(inputDirection.x, 0, inputDirection.y);
        moveAngle = Camera.main.transform.forward * moveAngle.z + Camera.main.transform.right * moveAngle.x;
        moveAngle.y = 0;

        playerVelocity.y += gravityValue;
        controller.Move(playerVelocity*Time.deltaTime);

        controller.Move(moveAngle * Time.deltaTime * playerSpeed);

        if (inputDirection != Vector2.zero) {
        	float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnEnable() {
    	InputManager.OnPlayerMovementPerformed += OnPlayerMovementPerformed;
    	InputManager.OnPlayerMovementCanceled += OnPlayerMovementCanceled;
    }

    private void OnDisable() {
    	InputManager.OnPlayerMovementPerformed -= OnPlayerMovementPerformed;
    	InputManager.OnPlayerMovementCanceled -= OnPlayerMovementCanceled;
    }

    private void OnPlayerMovementPerformed(Vector2 direction) {
    	inputDirection = direction;
    	anim.SetBool("Running", true);
    }

    private void OnPlayerMovementCanceled() {
    	inputDirection = Vector2.zero;
    	anim.SetBool("Running", false);
    }
}
