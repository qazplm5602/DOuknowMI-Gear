using System;
using System.Collections.Generic;

public class CSVParser
{
    public static List<DialogueData> parse(string data) {
        List<DialogueData> dialList = new List<DialogueData>();
        string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            DialogueData dialogue = new DialogueData
            {
                RandomType = values[0],
                Type = values[1],
                Order = values[2],
                Content = values[3],
            };

            dialList.Add(dialogue);
        }
        return dialList;
    }
}
