using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gunSystem : MonoBehaviour
{
    // Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int overheatMeter, bulletsShot;
    bool heating;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public Image overheatMeterBar;

    private void Start()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        zzzzzzzzzzzzz();

        overheatMeter = Mathf.Clamp(overheatMeter, 0, 100);
        overheatMeterBar.fillAmount = overheatMeter / 100f;
//        if (heating)
  //      {
    //        StartCoroutine("cooldown");
      //  }
    }

    private void zzzzzzzzzzzzz()
    {
        if (allowButtonHold) shooting = Input.GetButton("Fire1");

        else shooting = Input.GetButtonDown("Fire1");

        if (Input.GetKeyDown(KeyCode.R) && !reloading) Reload();

        if (readyToShoot && shooting && !reloading) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // Raycast
        Debug.DrawRay(fpsCam.transform.position, direction, Color.green);
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
               // rayHit.collider.gameObject.name;

                Destroy(rayHit.collider.gameObject);
        }

        overheatMeter += 20;

        Invoke("ResetShot", timeBetweenShooting);

        
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    { 
        overheatMeter = 0;
        reloading = false;
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(3);
        overheatMeter -= 1; 
    }
}