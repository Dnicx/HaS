using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionCollider : MonoBehaviour {
    private PickableObj itemHold;
    private PickableObj threwItem;
    private bool isHold;
    private GameObject interactObj;
    private bool isAction;
    [SerializeField]
    private Text text;
    [SerializeField] private GameObject handPos;

	// Use this for initialization
	void Start () {
        isAction = false;
        interactObj = null;
        isHold = false;
        text = GameObject.Find("ActionText").GetComponent<Text>();

        Physics.IgnoreLayerCollision(9, 10);
        Debug.Log("spawn action col");
	}
	
	// Update is called once per frame
	void Update () {
		if(interactObj != null && !isHold)
        {
            InteractabltObj obj = interactObj.GetComponent<InteractabltObj>();
            PickableObj tmp = obj as PickableObj;
            if (tmp != null && itemHold != null)
            {
                text.text = "";
            }
            else
            {
                text.text = "Left Click to action";
            }
            if (Input.GetButtonDown("Pickup"))
            {

                if (tmp != null && threwItem == null && itemHold == null && gameObject.tag == "Prey")
                {
                    itemHold = interactObj.GetComponent<PickableObj>();
                    itemHold.Pick();
                    isHold = true;
                    interactObj = null;
                }
                isAction = true;
                if (itemHold != null && itemHold.isHoly()) {
                    if (handPos != null) {
                        obj.transform.position = handPos.transform.position;
                        obj.transform.rotation = handPos.transform.rotation;
                    } else {
                        obj.actionDirection = this.transform.forward.normalized;    
                    }
                } else {
                    obj.actionDirection = this.transform.forward.normalized;
                }
                obj.action();
            }
            else if (Input.GetButtonDown("Pickup"))
            {
                isAction = false;
                obj.noAction();
            }
            //print(obj + " " + itemHold);

        }
        else
        {
            text.text = "";
            isAction = false;
        }

        if (isHold)
        {
            itemHold.Pick();
            itemHold.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            Vector3 direction = this.transform.forward.normalized * 0.5f;
            itemHold.transform.position = new Vector3(this.transform.position.x + direction.x, (this.transform.position.y - 0.1f + direction.y), this.transform.position.z + direction.z) + transform.right.normalized*0.3f;
        }

        if (threwItem != null)
        {
            itemHold = null;
            threwItem = null;

        }
    }

    public bool isHoldingHoly() {
        return (itemHold == null?false:itemHold.isHoly());
    }

    public bool IsAction()
    {
        return isAction;
    }

    public bool isItemHold()
    {
        return itemHold != null;
    }

    public void ThrowItemHold()
    {
        if(itemHold != null)
        {
            isHold = false;
            threwItem = itemHold;
            itemHold.ThrowObj();
        }
    }

    public void HolyHit() {
        if(itemHold != null)
        {
            isHold = false;
            Destroy(itemHold);
            itemHold = null;
            threwItem = null;
        }
    }

    public void ToggleHold()
    {
        isHold = !isHold;
        if (isHold)
        {
            itemHold.Pick();
            return;
        }
        itemHold.Keep();
    }

    public bool IsHold()
    {
        return isHold;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interact Obj"))
        {
            if(other.gameObject != itemHold)
                interactObj = other.gameObject;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interact Obj"))
        {
            InteractabltObj obj = interactObj!=null?interactObj.GetComponent<InteractabltObj>():null;
            obj.noAction();
            interactObj = null;
        }
    }
}
