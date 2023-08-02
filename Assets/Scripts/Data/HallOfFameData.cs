using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallOfFameData
{
    private List<HallOfFamePlayerData> hallList;

    public List<HallOfFamePlayerData> HallList { get { return hallList; } set { hallList = value; } }

    public HallOfFameData() 
    {
        this.hallList = new List<HallOfFamePlayerData>();
    }

    public HallOfFameData(List<HallOfFamePlayerData> hallList)
    {
        this.hallList = hallList;
    }

    public void AddToListHOF(HallOfFamePlayerData newPlayer)
    {
        hallList.Add(newPlayer);
        SortHOFList();
        if (hallList.Count > 10)
        {
            RemoveFromHOFList();

        }
    }

    public void RemoveFromHOFList()
    {
        hallList.RemoveRange(10, hallList.Count - 10);
    }

    public void SortHOFList() //Norman helped me understand and implement this arrow fuction
    {
        hallList.Sort((data1, data2) => data2.Points.CompareTo(data1.Points));
    }


}

public class HallOfFamePlayerData
{
    private string name;
    private int points;
    private bool success;

    public string Name { get { return name; } set { name = value; } }
    public int Points { get { return points; } set { points = value; } }
    public bool Success { get { return success; } set { success = value; } }

    public HallOfFamePlayerData(string name, int points, bool success)
    {
        this.name = name;
        this.points = points;
        this.success = success;
    }
}
