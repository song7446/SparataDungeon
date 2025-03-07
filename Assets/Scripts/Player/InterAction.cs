using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterAction : MonoBehaviour
{
    Camera camera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject promtGO;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    GameObject curLookObject;

    private void Awake()
    {
        camera = Camera.main;
        promtGO.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindInterAction();
    }

    void FindInterAction()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f, layerMask))
        {
            promtGO.SetActive(true);
            curLookObject = hit.transform.gameObject;
            UpdatePrompt(hit.collider.GetComponent<InteractableObject>());
        }
        else
        {
            ClearPrompt();
            curLookObject = null;
            promtGO.SetActive(false);
        }
    }

    void UpdatePrompt(InteractableObject mapObject)
    {
        nameText.text = mapObject.GetObjectName();
        descriptionText.text = mapObject.GetObjectDescription();
    }

    void ClearPrompt()
    {
        nameText.text = "";
        descriptionText.text = "";
    }

    void OnInterAction(InputAction.CallbackContext context)
    {
        if(curLookObject != null)
        {

        }
    }
}
