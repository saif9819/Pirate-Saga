using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "World", menuName = "Level")]
public class Level : ScriptableObject
{
    [Header("Board Dimensions")]
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Starting Tiles")]
    [SerializeField] private TileType boardLayout;

    [Header("Available Dots")]
    [SerializeField] private GameObject[] dots;

    [Header("Score Goals")]
    [SerializeField] private int[] scoreGoals;
}
