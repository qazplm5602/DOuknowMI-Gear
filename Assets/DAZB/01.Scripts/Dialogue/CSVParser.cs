using System;
using System.Collections.Generic;

public class CSVParser
{
    public static List<DialogueInfo> parse(string data) {
        List<DialogueInfo> dialList = new List<DialogueInfo>();
        string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            DialogueInfo dialogue = new DialogueInfo
            {
                ID = values[0],
                Speaker = values[1],
                Type = values[2],
                Order = values[3],
                Content = values[4]
            };

            dialList.Add(dialogue);
        }
        return dialList;
    }
}
