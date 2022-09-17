using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public  Animator _animator;

    private Rigidbody2D rigidbody2D;

    public float movementSpeed = 1f;

    public LayerMask LayerInteractables;
    public float circleCastRadius = 0.1f;
    bool IsDigging = false;

    private Direction lastDirection;
    void Awake()
    {
        Instance = this;
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.isKinematic = false;
        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;

        //I want to start in front position
        lastDirection = Direction.Front;
    }
    void Update()
    {

        if (IsDigging) return; //I not wanna move if I am digging

        Move();

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithInteractable();
        }

    }

    #region public calls
    //start the animation/action dig
    public void SetAnimationDig(UnityAction _callback)
    {
        StartCoroutine(StartDigAnimation(_callback));
    }
    #endregion
    #region private calls

    void Move()
    {

        float HorizontalValue = Input.GetAxisRaw("Horizontal");
        float VerticalValue = Input.GetAxisRaw("Vertical");

        float absolutex = Mathf.Abs(HorizontalValue);
        float absolutey = Mathf.Abs(VerticalValue);
        //if there is no button pressing, no move
        if ((absolutex != 0) || (absolutey != 0))
        {

            Vector3 newforce;
            //if is vertical or orizontal
            if (absolutex > absolutey)
            {
                newforce = new Vector3(HorizontalValue * movementSpeed,
                      0,
                      0);

                if (HorizontalValue > 0)
                    lastDirection = Direction.Right;
                else
                    lastDirection = Direction.Left;

                _animator.SetFloat("DirY", 0);
                _animator.SetFloat("DirX", HorizontalValue);
                _animator.SetBool("Run", true);

            }
            else
            {
                newforce = new Vector3(0,
                VerticalValue * movementSpeed,
                0); ;
                if (VerticalValue > 0)
                    lastDirection = Direction.Back;
                else
                    lastDirection = Direction.Front;

                _animator.SetFloat("DirX", 0);
                _animator.SetFloat("DirY", VerticalValue);
                _animator.SetBool("Run", true);
            }

            rigidbody2D.velocity = newforce;

        }
        else //if the player remain still
        {
            rigidbody2D.velocity = Vector2.zero;
            _animator.SetBool("Run", false);
            switch (lastDirection)
            {
                case Direction.Front: _animator.SetFloat("DirY", -0.2f); _animator.SetFloat("DirX", 0); break;
                case Direction.Back: _animator.SetFloat("DirY", 0.2f); _animator.SetFloat("DirX", 0); break;
                case Direction.Left: _animator.SetFloat("DirX", -0.2f); _animator.SetFloat("DirY", 0); break;
                case Direction.Right: _animator.SetFloat("DirX", 0.2f); _animator.SetFloat("DirY", 0); break;
            }

        }

    }

    IEnumerator StartDigAnimation(UnityAction _callback)
    {
        IsDigging = true;
        _animator.SetBool("Digging", IsDigging);
        yield return new WaitForSeconds(1);
        IsDigging = false;
        _animator.SetBool("Digging", IsDigging);
        _callback?.Invoke();
    }

    private void InteractWithInteractable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCastRadius, LayerInteractables);
        for (int i = 0; i < colliders.Length; i++)
        {

            Interactables controller = colliders[i].gameObject.GetComponent<Interactables>();
            if (controller != null)
                controller.Interact();
        }
    }
    #endregion

    enum Direction { Front, Back, Left, Right };
}