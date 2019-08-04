using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public GameObject DebugParticle;

    [SerializeField] private float backToActiveTime = 0.3f;

    private bool portalActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalActive)
        {
            SetPortalActive(false);
            otherPortal.SetPortalActive(false);
            GameObject debugPart = Instantiate(DebugParticle);
            debugPart.transform.position = collision.transform.position;
            collision.transform.position = otherPortal.transform.position;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(backToActive());
    }

    public void SetPortalActive(bool active)
    {
        portalActive = active;
    }

    private IEnumerator backToActive()
    {
        yield return new WaitForSeconds(backToActiveTime);
        SetPortalActive(true);
        otherPortal.SetPortalActive(true);
    }
}
