using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int bulletAmount;
    public GameObject bulletPrefab;
    public GameObject shootPoint;
    public AudioSource shootSound;
    public ParticleSystem muzzleEffect;
    Animator animator;
    float lastShootTime;
    public float fireRate;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKey(KeyCode.Mouse0) && bulletAmount >0 && Time.timeScale>0)
        {
            animator.SetBool("Shooting", true);

            var timeSinceLastshoot = Time.time - lastShootTime;
            if(timeSinceLastshoot< fireRate)
            {
                return;
            }
            lastShootTime = Time.time;
            bulletAmount--;
            muzzleEffect.Play();
            shootSound.Play();
            GameObject clone = Instantiate(bulletPrefab);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;

        }
        else
        {
            animator.SetBool("Shooting", false);
        }
    }
}
