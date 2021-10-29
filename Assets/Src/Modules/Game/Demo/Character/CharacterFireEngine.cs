using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFireEngine : StateMachine
{
    const float FIRE_RATE = 1;

    Camera _eye;
    bool _canCreateBullet = true;
    float _fireSpeed;

    [SerializeField] GameObject _bullet;

    protected override void Start()
    {
        this._eye = this.GetComponentInChildren<Camera>();
        this._fireSpeed = 1 / FIRE_RATE;
        base.Start();
    }

    protected override void Update()
    {
        this.Look();
        this.Interact();
        this.Shoot();
    }

    void TurnOffFire(float waitTime)
    {
        this._canCreateBullet = false;
        LeanTween.delayedCall(waitTime, () =>
        {
            this._canCreateBullet = true;
        });
    }

    public GameObject GetTarget()
    {
        float screenX = Screen.width / 2;
        float screenY = Screen.height / 2;
        RaycastHit hit;
        Ray ray = this._eye.ScreenPointToRay(new Vector3(screenX, screenY));

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        else return null;
    }

    void Look()
    {
        GameObject target = this.GetTarget();
        if (target != null)
        {
            // Debug.Log("Target:" + target);
        }
    }

    void Shoot()
    {
        // //create bullet
        // if (InputMgr.DoShoot() && this._canCreateBullet)
        // {
        //     this.TurnOffFire(this._fireSpeed);
        //     Vector3 aimSpot = this._eye.transform.position;
        //     GameObject newBullet = Instantiate(this._bullet, this._eye.transform.position, this._eye.transform.rotation);
        //     newBullet.transform.LookAt(aimSpot);
        // }

        //ray-firing
        if (InputMgr.DoShoot() && this._canCreateBullet)
        {
            GameObject target = this.GetTarget();
            if (target != null)
            {
                CharacterHealth health = target.GetComponent<CharacterHealth>();
                Debug.Log(health);
                if (health != null) health.InflictDamage(999);
            }
        }
    }

    void Interact()
    {
        if (InputMgr.DoInteract())
        {
            GameObject target = this.GetTarget();
            Debug.Log(target);
            if (target != null)
            {
                InteractEngine interactEngine = target.GetComponent<InteractEngine>();
                if (interactEngine != null && interactEngine.IsPlayerInRange(this.gameObject))
                {
                    interactEngine.DoInteract(this.gameObject);
                }
            }
        }
    }
}