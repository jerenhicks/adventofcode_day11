using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{

    static Dictionary<string, Double> stoneValues = new Dictionary<string, Double>();
    static int maxBlink = 75;
    static void Main()
    {

        string filePath = "data.txt";
        string fileContents = File.ReadAllText(filePath);

        List<int> values = fileContents
            .Split(' ')
            .Select(int.Parse)
            .ToList();


        Double amountStones = 0;

        foreach (int value in values)
        {
            Double stonesForValue = 0;

            stonesForValue += ProcessStone(0, value);

            Console.WriteLine("Value: " + value + " Stones: " + stonesForValue);
            amountStones += stonesForValue;
        }

        Console.WriteLine(amountStones);
    }

    static Double ProcessStone(int blinkCount, Double stoneValue)
    {

        if (blinkCount >= maxBlink)
        {
            return 1;
        }

        if (stoneValues.ContainsKey(stoneValue.ToString() + " - " + blinkCount))
        {
            return stoneValues[stoneValue.ToString() + " - " + blinkCount];
        }

        if (stoneValue == 0)
        {
            var newStoneValue = 1;
            var newBlinkCount = blinkCount + 1;
            var result = ProcessStone(newBlinkCount, newStoneValue);
            if (!stoneValues.ContainsKey(newStoneValue.ToString() + " - " + newBlinkCount))
            {
                stoneValues.Add(newStoneValue.ToString() + " - " + newBlinkCount, result);
            }
            return result;
        }
        else if (stoneValue.ToString().Length % 2 == 0)
        {
            string stoneValueStr = stoneValue.ToString();
            int mid = stoneValueStr.Length / 2;
            Double firstHalf = int.Parse(stoneValueStr.Substring(0, mid));
            Double secondHalf = int.Parse(stoneValueStr.Substring(mid));

            var newBlinkCount = blinkCount + 1;

            Double result = ProcessStone(newBlinkCount, firstHalf);
            if (!stoneValues.ContainsKey(firstHalf.ToString() + " - " + newBlinkCount))
            {
                stoneValues.Add(firstHalf.ToString() + " - " + newBlinkCount, result);
            }
            //stoneValues.Add(firstHalf.ToString() + " - " + newBlinkCount, result);
            Double secondResult = ProcessStone(newBlinkCount, secondHalf);
            if (!stoneValues.ContainsKey(secondHalf.ToString() + " - " + newBlinkCount))
            {
                stoneValues.Add(secondHalf.ToString() + " - " + newBlinkCount, secondResult);
            }
            //stoneValues.Add(stoneValue.ToString() + " - " + blinkCount, secondResult);

            return result + secondResult;
        }
        else
        {
            var newStoneValue = stoneValue * 2024;
            var newBlinkCount = blinkCount + 1;
            var result = ProcessStone(newBlinkCount, newStoneValue);
            stoneValues.Add(newStoneValue.ToString() + " - " + newBlinkCount, result);
            return result;
        }

    }

}