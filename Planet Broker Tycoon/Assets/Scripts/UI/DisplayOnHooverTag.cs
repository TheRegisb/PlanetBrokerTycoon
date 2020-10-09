using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnHooverTag : MonoBehaviour
{
    public string tag;
    public int flipWhenAbovePx = 332;
    private Ray ray;
    private RaycastHit2D hit;

    // Set first child as active only if hoovering something with a predefined tag.
    // Set inactive otherwise.
    void Update()
    {
        GameObject firstChild = transform.GetChild(0).gameObject;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.CompareTag(tag))
        {
            firstChild.SetActive(true);
            if (gameObject.transform.localPosition.x >= flipWhenAbovePx)
            {
                firstChild.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
            } else
            {
                firstChild.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            }
        } else
        {
            firstChild.SetActive(false);
        }
    }
}
