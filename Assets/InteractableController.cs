using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textSelectionInfor;
    [SerializeField] private int distanceToRay = 5;

    Vector3 Size = new Vector3(60, -30, 0);

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, distanceToRay))
        {
            ISelectable objectSelect = hit.transform.gameObject.GetComponent<ISelectable>();

            if (objectSelect != null)
            {
                _textSelectionInfor.text = objectSelect.GetNameItemSelect();
                _textSelectionInfor.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    IInteractable objectInteract = hit.transform.gameObject.GetComponent<IInteractable>();
                    if (objectInteract != null)
                    {
                        objectInteract.Interact();
                    }
                }

            }
            else
            {
                _textSelectionInfor.gameObject.SetActive(false);
            }
        }
        else
        {
            _textSelectionInfor.gameObject.SetActive(false);
        }

        _textSelectionInfor.transform.position = Input.mousePosition + Size;
    }
}
