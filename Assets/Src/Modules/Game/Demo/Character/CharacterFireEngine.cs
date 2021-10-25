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

    protected override void FixedUpdate()
    {
        if (InputMgr.DoShoot() && this._canCreateBullet)
        {
            this.TurnOffFire(this._fireSpeed);
            Vector3 aimSpot = this._eye.transform.position;
            GameObject newBullet = Instantiate(this._bullet, this._eye.transform.position, this._eye.transform.rotation);
            newBullet.transform.LookAt(aimSpot);
        }

        //ray-firing
        if (InputMgr.DoShoot() && this._canCreateBullet)
        {
            float screenX = Screen.width / 2;
            float screenY = Screen.height / 2;
            RaycastHit hit;
            Ray ray = this._eye.ScreenPointToRay(new Vector3(screenX, screenY));

            if (Physics.Raycast(ray, out hit))
            {
                CharacterHealth health = hit.collider.gameObject.GetComponent<CharacterHealth>();
                Debug.Log(health);
                if (health != null) health.InflictDamage(999);
            }
        }

        base.FixedUpdate();
    }

    void TurnOffFire(float waitTime)
    {
        this._canCreateBullet = false;
        LeanTween.delayedCall(waitTime, () =>
        {
            this._canCreateBullet = true;
        });
    }
}