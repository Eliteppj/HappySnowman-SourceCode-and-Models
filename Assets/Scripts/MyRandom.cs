using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class MyRandom
{

    private float[] weight; //가중치를 저장할 float[] 레퍼런스
    public System.Random random;
    public double[] new_weight;
    public double weightSum; //SetWeight함수 -> 전달 받은 인자들의 모든 합(물품의 총합이 아닌 직접적인 확률의 합)
    public int countSum; //SetWeightbyItemCount함수 -> 전달 받은 인자들의 모든 합(물품의 총합)

    public bool SetWeightbyItemCount(int[] count) //상품의 수량을 입력받아 가중치를 구한다.
    {
        foreach (int i in count) //아이템의 전체 개수를 구하면서 만약 음의 값이 있다면 false를 반환한다.
        {
            countSum += i;
            if (i<0)
            {
                Debug.LogError("minus value can not be approved, use postive integer");
                return false;
            }
           
        }
       
        new_weight = new double[count.Length];  //이상이 없다고 판단하고 가중치를 저장할 배열을 생성해서 new_weight에  할당해준다.
      

        for(int i=0; i<count.Length; i++)
        {
            new_weight[i] = (count[i] / (double)countSum);
        }

        foreach (double i3 in new_weight)
        {
            Debug.Log(i3);
        }
        return true;

    }

    public bool SetWeight(double[] weight, out GetRandomInfo info) //각 상품이 나와야할 확률을 입력받아 가중치를 저장한다.
    {
        weightSum = 0;
        foreach (double i in weight)
        {
            weightSum += i;
        }


        if (weightSum < 0f) //확률 값의 합이 음수, 개발자 실수이므로 false를 반환한다.
        {
            info.array = null;
            info.Length = 0;
            Debug.LogError("weightSum should not be under 0!");
            return false;

        }
        else if (weightSum > 100f) //입력한 값이 100을 넘는다면 개발자가 실수 했다는 뜻이므로 false를 반환한다.
        {
            info.array = null;
            info.Length = 0;
            Debug.LogError("weightSum should not over 100!");
            return false;
        }
        else if(weightSum == 0f)
        {
            info.array = null;
            info.Length = 0;
            Debug.LogError("weightSum should not be 0!");
            return false;


        }
        else if (weightSum < 100f) //sum이 100이하의 양의 정수라면 확률을 전부 기입하지 않은 것이므로 원래 크기의 배열보다 1 큰 배열을 생성해서 넣어주고 마지막 인덱스에 100에서 나머지 확률을 뺀 확률값을 넣어준다.
        {
            new_weight = new double[weight.Length + 1];
            weight.CopyTo(new_weight, 0);
            new_weight[weight.Length] = 100f - weightSum;
            info.array = new_weight;
            info.Length = weight.Length + 1;
            Debug.LogWarning("Not complete array, last index was added");
            return true;

        }
        else //정확히 100으로 일치하는 경우
        {
            new_weight = new double[weight.Length];
            weight.CopyTo(new_weight, 0);
            info.array = new_weight;
            info.Length = weight.Length;
            return true;
        }

       





    }


}
