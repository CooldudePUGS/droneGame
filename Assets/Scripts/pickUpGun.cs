using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pickUpGun : MonoBehaviour
{
    public gunSystem gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;
    public GameObject audioToPlay;
    public TextMeshProUGUI textToChange;

    public float pickUprange;

    public bool equipped;

    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUprange && Input.GetKeyDown(KeyCode.E)) PickUp();

        if (equipped)
        {
            textToChange.SetText("BECOME THE WEAPON");
        }
    }

    private void PickUp()
    {
        equipped = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

        gunScript.enabled = true;

        transform.SetParent(gunContainer);
        //transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.Euler(Vector3.zero);

        audioToPlay.SetActive(true);
    }
}
