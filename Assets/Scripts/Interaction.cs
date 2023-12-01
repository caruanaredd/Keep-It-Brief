using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Interaction : MonoBehaviour
{
    public GameObject interaction;

    private Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    // Start is called before the first frame update
    void OnInteract(InputValue value)
    {
        bool isPressed = value.Get<float>() != 0;

        if (isPressed)
        {
            StartCoroutine(DisableInteraction());
        }
        else
        {
            interaction.transform.localPosition = Vector2.zero;
        }
    }

    IEnumerator DisableInteraction()
    {
        interaction.transform.localPosition = movement.direction.ToVector2();
        yield return new WaitForSeconds(0.25f);
        interaction.transform.localPosition = Vector2.zero;
    }
}
