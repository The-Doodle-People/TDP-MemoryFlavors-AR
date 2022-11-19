/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description:
 */

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizGenerator : MonoBehaviour
{
    // General Variables
    private GameManager gameManager;
    
    // Four Foods;
    /*
     * IceGem Biscuits
     * - Butter
     * - Sugar
     * - Flour
     * - Icing
     * - Water
     * - Food Coloring
     * 
     * Biscuit Piring
     * - Flour
     * - Coconut Milk
     * - Sugar
     * - Egg
     * - Food Coloring
     * 
     * Chocolate Eggs
     * - Butter
     * - Vanilla
     * - Cream Cheese
     * - Cocoa Powder
     * - Oil
     * - Peanut Butter
     * 
     * Murukku
     * - Flour
     * - Red Chili
     * - Oil
     * - Sesame Seed
     * - Salt
     */
    
    #if UNITY_EDITOR
        private enum gameModeOptions
        {
            None,
            IceGemBiscuits,
            BiscuitPiring,
            Murukku,
            ChocolateEggs
        }
        [Header("UnityEditor Only")]
        [SerializeField] private gameModeOptions editorChoice;

    #endif
    
    private string[] gameModes =
    {
        "IceGemBiscuits",
        "BiscuitPiring",
        "Murukku",
        "ChocolateEggs"
    };
    
    public string quizOption = "none";

    //  Ingredients;
    /*
     * Butter
     * Sugar
     * Flour
     * Water
     * Eggs
     * Cocoa
     * Oil
     * Salt
     * Icing
     * Coconut Milk
     * Red Chili
     * Food Colouring
     * Vanilla
     * Cream Cheese
     * Peanut Butter
     * Sesame Seed
     */
    public string[] allIngredients =
    {
        "Butter",
        "Sugar",
        "Flour",
        "Water",
        "Eggs",
        "Cocoa",
        "Oil",
        "Salt",
        "Icing",
        "CoconutMilk",
        "RedChili",
        "FoodColouring",
        "Vanilla",
        "CreamCheese",
        "PeanutButter",
        "SesameSeed"
    };

    /// <summary>
    /// List of food with respective ingredients (correctAns)
    /// </summary>
    private readonly Dictionary<string, string[]> usedIngredients = new Dictionary<string, string[]>()
    {
        {"IceGemBiscuits", new[] {"Butter", "Sugar", "Flour", "Icing", "Water", "FoodColouring"}},
        {"BiscuitPiring", new[] {"Flour", "CoconutMilk", "Sugar", "Egg", "FoodColouring"}},
        {"Murukku", new[] {"Flour", "RedChili", "Oil", "SesameSeed", "Salt"}},
        {"ChocolateEggs", new[] {"Butter", "Vanilla", "CreamCheese", "Cocoa", "Oil", "PeanutButter"}}
    };
    
    
    #region For Game
    
    [SerializeField] private List<string> unusedIngredients = new List<string> {};
    public List<string> selectedIngredients = new List<string> {};

    public TextMeshProUGUI result;

    public GameObject[] foods;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<TouchHandler>().quizGenerator = this;
        gameManager = FindObjectOfType<GameManager>();
        
        #if UNITY_EDITOR
            // apply editorChoice to quizOption
            quizOption = editorChoice.ToString();
            // if there is a choice by editor, stop game manager override and start game
            if (quizOption.ToLower() != "none")
            {
                GenerateQuiz();
                return;
            }
        
        #endif

        var quizId = gameManager.quizId;
        quizOption = gameModes[quizId];
        GenerateQuiz();
        foreach (var foodGrp in foods)
        {
            foodGrp.transform.GetChild(quizId).gameObject.SetActive(true);
        }

        result.text = gameModes[quizId];
    }

    private void GenerateQuiz()
    {
        var ingredientList = usedIngredients[quizOption];
        // sorts ingredients that are used (correct ans)
        foreach (var ingredient in ingredientList)
        {
            selectedIngredients.Add(ingredient);
        }

        // sorts ingredients that are not used (wrong ans)
        foreach (var ingredient in allIngredients)
        {
            if (selectedIngredients.Contains(ingredient)) continue;
            unusedIngredients.Add(ingredient);
        }
    }
}
