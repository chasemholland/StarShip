using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
///  A generic sorted list
/// </summary>
/// <typeparam name="T">generic</typeparam>
public class SortedList<T> where T:IComparable
{
    #region Fields

    // sorted list of items
    List<T> itemList = new List<T>();

    // temp list used in add method
    List<T> tempList = new List<T>();

    #endregion

    #region Constructor

    /// <summary>
    /// No argument constructor
    /// </summary>
    public SortedList()
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of items in the list
    /// </summary>
    public int Count
    {
        get { return itemList.Count; }
    }

    /// <summary>
    /// Gets the item in the list at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T this[int index]
    {
        get { return itemList[index]; }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds the given item to the list
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        // if list is empty add the item
        if (Count == 0)
        {
            itemList.Add(item);
        }
        else
        {
            // find location where to add the given item
            int addLocal = 0;
            while ((addLocal < Count) && (itemList[addLocal].CompareTo(item) > 0))
            {
                tempList.Add(itemList[addLocal]);
                addLocal++;
            }

            // add item to list then add the remainder from the start of the list
            tempList.Add(item);

            if (tempList.Count <= Count)
            {
                for (int i = addLocal; i < Count; i++)
                {
                    tempList.Add(itemList[i]);
                }
            }

            // reassign the item list to the newly created and sorted templist
            itemList = tempList;
            tempList = new List<T>();
        }
    }

    /// <summary>
    /// Binary search for the given item
    /// </summary>
    /// <param name="item">item to be found</param>
    /// <returns>index of the item</returns>
    public int IndexOf(T item)
    {
        int lowerBound = 0;
        int upperBound = itemList.Count - 1;
        int local = -1;

        // loop until value found or made it through entire list
        while ((local == -1) && (lowerBound <= upperBound))
        {
            // find the middle
            int middleLocal = lowerBound + (upperBound - lowerBound) / 2;
            T middleVal = itemList[middleLocal];

            // check for match
            if (middleVal.CompareTo(item) == 0)
            {
                local = middleLocal;
            }
            else
            {
                if (middleVal.CompareTo(item) < 0)
                {
                    upperBound = middleLocal - 1;
                }
                else
                {
                    lowerBound = middleLocal + 1;
                }
            }
        }
        return local;
    }

    /// <summary>
    /// Sorts the list
    /// </summary>
    public void Sort()
    {
        itemList.Sort();
        itemList.Reverse();
    }

    /// <summary>
    /// Returns the list as a csv string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string listString = string.Empty;
        for (int i = 0; i < Count; i++)
        {
            listString += itemList[i].ToString();
            if (i < Count -1)
            {
                listString += ",";
            }
        }
        return listString;
    }

    #endregion
}
