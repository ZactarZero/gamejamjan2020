using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float timeBetweenAttacks;
    public int damage;

    public LineRenderer lineRenderer;
    public float lineStayTime = 0.2f;
    private bool lineExists = false;
    private float lineTimer = 0f;

    private bool attackDebounce = false;
    private float debounceTimer = 0f;

    private GameObject target = null;
    private bool hasTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target && !attackDebounce)
        {

            Vector3 startPoint = transform.position;
            startPoint.z = -1;

            Vector3 endPoint = target.transform.position;
            endPoint.z = -1;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
            lineRenderer.gameObject.SetActive(true);

            target.GetComponent<EnemyController>().Damage(damage);

            lineExists = true;
            attackDebounce = true;
        }

        if (attackDebounce)
        {
            debounceTimer += Time.deltaTime;

            if (debounceTimer > timeBetweenAttacks)
            {
                attackDebounce = false;
                debounceTimer = 0f;
            }
        }

        if (lineExists)
        {
            lineTimer += Time.deltaTime;

            if (lineTimer > lineStayTime)
            {
                lineRenderer.gameObject.SetActive(false);
                lineExists = false;
                lineTimer = 0f;
            }

            if (target)
            {
                Vector3 endPoint = target.transform.position;
                lineRenderer.SetPosition(1, endPoint);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hasTarget && !attackDebounce)
        {
            target = collision.gameObject;

            hasTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        hasTarget = false;
    }
}

public enum TurretType
{
    Assault, Rocket
}