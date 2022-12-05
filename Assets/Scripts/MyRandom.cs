using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniData;

public class MyRandom
{

    private float[] weight; //����ġ�� ������ float[] ���۷���
    public System.Random random;
    public double[] new_weight;
    public double weightSum; //SetWeight�Լ� -> ���� ���� ���ڵ��� ��� ��(��ǰ�� ������ �ƴ� �������� Ȯ���� ��)
    public int countSum; //SetWeightbyItemCount�Լ� -> ���� ���� ���ڵ��� ��� ��(��ǰ�� ����)

    public bool SetWeightbyItemCount(int[] count) //��ǰ�� ������ �Է¹޾� ����ġ�� ���Ѵ�.
    {
        foreach (int i in count) //�������� ��ü ������ ���ϸ鼭 ���� ���� ���� �ִٸ� false�� ��ȯ�Ѵ�.
        {
            countSum += i;
            if (i<0)
            {
                Debug.LogError("minus value can not be approved, use postive integer");
                return false;
            }
           
        }
       
        new_weight = new double[count.Length];  //�̻��� ���ٰ� �Ǵ��ϰ� ����ġ�� ������ �迭�� �����ؼ� new_weight��  �Ҵ����ش�.
      

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

    public bool SetWeight(double[] weight, out GetRandomInfo info) //�� ��ǰ�� ���;��� Ȯ���� �Է¹޾� ����ġ�� �����Ѵ�.
    {
        weightSum = 0;
        foreach (double i in weight)
        {
            weightSum += i;
        }


        if (weightSum < 0f) //Ȯ�� ���� ���� ����, ������ �Ǽ��̹Ƿ� false�� ��ȯ�Ѵ�.
        {
            info.array = null;
            info.Length = 0;
            Debug.LogError("weightSum should not be under 0!");
            return false;

        }
        else if (weightSum > 100f) //�Է��� ���� 100�� �Ѵ´ٸ� �����ڰ� �Ǽ� �ߴٴ� ���̹Ƿ� false�� ��ȯ�Ѵ�.
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
        else if (weightSum < 100f) //sum�� 100������ ���� ������� Ȯ���� ���� �������� ���� ���̹Ƿ� ���� ũ���� �迭���� 1 ū �迭�� �����ؼ� �־��ְ� ������ �ε����� 100���� ������ Ȯ���� �� Ȯ������ �־��ش�.
        {
            new_weight = new double[weight.Length + 1];
            weight.CopyTo(new_weight, 0);
            new_weight[weight.Length] = 100f - weightSum;
            info.array = new_weight;
            info.Length = weight.Length + 1;
            Debug.LogWarning("Not complete array, last index was added");
            return true;

        }
        else //��Ȯ�� 100���� ��ġ�ϴ� ���
        {
            new_weight = new double[weight.Length];
            weight.CopyTo(new_weight, 0);
            info.array = new_weight;
            info.Length = weight.Length;
            return true;
        }

       





    }


}
