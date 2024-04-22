using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i ++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        
        // 내가 결국에 하려는 것은, player의 health 값을 heart container에 그대로 반영(매핑)하려는 것!!
        // 근데 둘이 단위가 다르다!!
        // 단위를 통일하는 가장 간단한 방법? "비율"
        // 내 체력이 50%가 있어 == 그럼 체력이 얼마라는 소리지? 3
        // 하트가 50%가 있어    == 그럼 하트가 몇 개라는 소리지? 1.5

        // 체력이 3이면, 하트가 1.5개라는 뜻
        // 체력의 maximum이 6
        // 하트의 maximum이 3
        // 둘의 관계는 6 : 3

        // heartContainerCount = 0.5(비율) * 전체 하트 칸 갯수 (3개)
        float currentHealthRate = (float)playerCurrentHealth.RuntimeValue / (float)playerCurrentHealth.initialValue;
        float heartContainerCount = currentHealthRate * heartContainers.initialValue; 
        // playerCurrentHealth.initialValue 내가 처음에 설정한 체력 값, Runtime 현재 체력 값
        // currentHealthRate 비율. 내가 처음 설정한 체력이 6이고, 현재 체력이 3일 때의 체력 비율 == 0.5 
        // heartContainers.initialValue 내가 처음에 설정한 하트 값, heartContainerCount 현재 하트 갯수

        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            float heartContainerDistance = heartContainerCount - i;
            if (1.0 <= heartContainerDistance)
            {
                //full hearts
                hearts[i].sprite = fullHeart;
            }
            else if (0.5 <= heartContainerDistance && heartContainerDistance < 1.0)
            {
                //half hearts
                hearts[i].sprite = halfFullHeart;
            }
            else
            {
                //empty hearts
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
