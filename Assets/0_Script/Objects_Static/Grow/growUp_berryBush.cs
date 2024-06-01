using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class growUp_berryBush : MonoBehaviour
{
    int today;
    GameObject berryBush_0;
    GameObject berryBush_1;
 
    // Start is called before the first frame update
    void Start()
    {
        today = 0;
        berryBush_0 = transform.GetChild(0).gameObject;
        berryBush_1 = transform.GetChild(1).gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        today = checkToday.Today;
        growing_up(today);
     
    }
 
    void growing_up(int today)
    {
        //if (today < 20)
        //{
        //    if (potato_25.activeSelf == true)
        //        potato_25.SetActive(false);
        //}
        if (today >= 0 && today < 5)
        {
            if (berryBush_0.activeSelf == false)
                berryBush_0.SetActive(true);
            float size = (float)today * 0.1f;
            float size_y = (float)today * 0.2f;
            berryBush_0.transform.localScale = new Vector3(size, size_y, size);
 
        }
        else if (today >= 5)
        {
            // 여기서 potato_25의 상태를 비활성화 하지 않는 이유는
            // 45~90 동안 이미 비활성화가 되었기 때문이다.
            if (berryBush_0.activeSelf == true)
                berryBush_0.SetActive(false);
            if (berryBush_1.activeSelf == false)
                berryBush_1.SetActive(true);
        }
    }
}