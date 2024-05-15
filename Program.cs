using System;
using System.Collections.Generic;

namespace Prog_Recipe_App_Part2
{
    public class Ingredient
    {

        // Properties of an ingredient
        public string Name { get; set; }       // Name of the ingredient
        public double Quantity { get; set; }    // Quantity of the ingredient
        public string Unit { get; set; }        // Unit of measurement for the quantity
        public int Calories { get; set; }       // Calories per unit of the ingredient
        public string FoodGroup { get; set; }   // Food group to which the ingredient belongs

        // Constructor to initialize an Ingredient object
        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        // Define the Recipe class to represent a recipe
        public class Recipe
        {
            // Properties of a recipe
            public string Name { get; set; }                     // Name of the recipe
            public List<Ingredient> Ingredients { get; set; }    // List of ingredients in the recipe
            public List<string> Steps { get; set; }              // List of steps to prepare the recipe
            public int TotalCalories { get; private set; }       // Total calories of the recipe

            // Constructor to initialize a Recipe object
            public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
            {
                Name = name;
                Ingredients = ingredients;
                Steps = steps;
                CalculateTotalCalories(); // Calculate the total calories when a recipe is created
            }

            // Method to calculate the total calories of the recipe based on its ingredients
            public void CalculateTotalCalories()
            {
                TotalCalories = 0;
                foreach (Ingredient ingredient in Ingredients)
                {
                    TotalCalories += ingredient.Calories;
                }
            }

            // Method to display the details of the recipe, including ingredients, steps, and total calories
            public void DisplayRecipe(CalorieNotificationHandler calorieNotification)
            {
                Console.WriteLine("Recipe: " + Name);
                Console.WriteLine("Ingredients:");
                foreach (Ingredient ingredient in Ingredients)
                {
                    Console.WriteLine(ingredient.Quantity + " " +
                          ingredient.Unit + " of " +
                          ingredient.Name + " (" +
                          ingredient.Calories + " calories, " +
                          ingredient.FoodGroup + ")");
                }

                Console.WriteLine("\nSteps:");
                for (int i = 0; i < Steps.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + Steps[i]);
                }

                Console.WriteLine("\nTotal Calories:" + TotalCalories);
                // If total calories exceed 300, invoke the calorie notification handler
                if (TotalCalories > 300)
                {
                    calorieNotification(this);
                    Console.WriteLine();
                }
            }

            // Method to scale the recipe by a given factor
            public void ScaleRecipe(double factor)
            {
                foreach (Ingredient ingredient in Ingredients)
                {
                    ingredient.Quantity *= factor;
                }
                CalculateTotalCalories(); // Recalculate total calories after scaling
                Console.WriteLine("Recipe " + " " + Name + " Has Been Scaled By a Factor of " + factor);
                Console.WriteLine();
            }

            // Method to reset the quantities of all ingredients in the recipe to 1
            public void ResetRecipeQuantities()
            {
                foreach (Ingredient ingredient in Ingredients)
                {
                    ingredient.Quantity = 1;
                }
                CalculateTotalCalories(); // Recalculate total calories after resetting quantities
                Console.WriteLine("Quantities For Recipe " + " " + Name + " Have Been Reset.");
                Console.WriteLine();
            }

            // Method to clear all ingredients and steps from the recipe
            public void ClearRecipe()
            {
                Ingredients.Clear();
                Steps.Clear();
                TotalCalories = 0; // Reset total calories to zero
                Console.WriteLine("Recipe" + " " + Name + " Has Been Cleared.");
                Console.WriteLine();
            }
        }

        // Method to enter details of a new recipe
        static Recipe EnterNewRecipeDetails()
        {
            Console.WriteLine("-------------ADD NEW RECIPE------------");
            string recipeName;
            while (true)
            {
                Console.Write("Enter the Recipe Name: ");
                recipeName = Console.ReadLine();
                // Validate that the recipe name is not empty
                if (string.IsNullOrEmpty(recipeName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Can You Please Enter Something.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    break; // Exit the loop if a valid recipe name is provided
                }
            }

            // Prompt for the number of ingredients to be added
            int numberOfIngredients;
            while (true)
            {
                Console.Write("Enter the Number of Ingredients to be Added: ");
                if (!int.TryParse(Console.ReadLine(), out numberOfIngredients) || numberOfIngredients <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input For Number of Ingredients. Please Enter an Integer.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    break; // Exit the loop if a valid number of ingredients is provided
                }
            }
            Console.WriteLine();

            // Create a list to store ingredients of the recipe
            List<Ingredient> ingredients = new List<Ingredient>();
            // Loop to enter details of each ingredient
            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.WriteLine("INGREDIENT " + (i + 1));
                string ingredientName;
                while (true)
                {
                    Console.Write("Enter Ingredient Name: ");
                    ingredientName = Console.ReadLine();
                    // Validate that the ingredient name is not empty
                    if (string.IsNullOrEmpty(ingredientName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Enter Something!");
                        Console.ResetColor();
                    }
                    else
                    {
                        break; // Exit the loop if a valid ingredient name is provided
                    }
                }

                double ingredientQuantity;
                while (true)
                {
                    Console.Write("Enter Ingredient Quantity: ");
                    if (!double.TryParse(Console.ReadLine(), out ingredientQuantity) || ingredientQuantity <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error! Invalid Input For Ingredient Quantity. Please Enter a Number.");
                        Console.ResetColor();
                    }
                    else
                    {
                        break; // Exit the loop if a valid ingredient quantity is provided
                    }
                }

                Console.Write("Enter Ingredient Unit Of Measurement: ");
                string ingredientUnit = Console.ReadLine();

                int ingredientCalories;
                while (true)
                {
                    Console.Write("Enter Ingredient Calories: ");
                    if (!int.TryParse(Console.ReadLine(), out ingredientCalories) || ingredientCalories <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error! Invalid Input For Ingredient Calories. Please Enter an Integer.");
                        Console.ResetColor();
                    }
                    else
                    {
                        break; // Exit the loop if valid ingredient calories are provided
                    }
                }

                Console.Write("Enter Ingredient Food Group: ");
                string ingredientFoodGroup = Console.ReadLine();

                // Create an Ingredient object with the entered details and add it to the list
                Ingredient ingredient = new Ingredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, ingredientFoodGroup);
                ingredients.Add(ingredient);
                Console.WriteLine();
            }

            int numberOfSteps;
            // Prompt for the number of steps in the recipe
            while (true)
            {
                Console.Write("Enter The Number Of Steps: ");
                if (!int.TryParse(Console.ReadLine(), out numberOfSteps) || numberOfSteps <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Invalid Input For Number of Steps. Input an Integer ");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    break; // Exit the loop if a valid number of steps is provided
                }
            }

            // Create a list to store the steps of the recipe
            List<string> steps = new List<string>();
            // Loop to enter details of each step
            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Write("Enter Step Description " + (i + 1) + ": ");
                steps.Add(Console.ReadLine());
            }

            // Create a new Recipe object with the entered details
            Recipe recipe = new Recipe(recipeName, ingredients, steps);
            return recipe;
        }

        // Method to display the list of recipes
        static void DisplayRecipeList(List<Recipe> recipes)
        {
            Console.WriteLine();
            // Check if there are any recipes available
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Recipes Available.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            // Sort the recipes alphabetically by name
            recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name));

            Console.WriteLine("List of Recipes:");
            // Display the names of all recipes in the list
            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }
        }

        // Method to display details of a selected recipe
        static void DisplaySelectedRecipe(List<Recipe> recipes, CalorieNotificationHandler calorieNotification)
        {
            Console.WriteLine();
            // Check if there are any recipes available
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Recipes Available.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            // Prompt the user to enter the name of the recipe
            Console.Write("Enter the Recipe Name: ");
            string recipeName = Console.ReadLine();

            Console.WriteLine();

            // Find the recipe with the entered name in the list
            Recipe recipe = recipes.Find(r => r.Name == recipeName);
            if (recipe != null)
            {
                // Display the details of the selected recipe
                recipe.DisplayRecipe(calorieNotification);
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Recipe not found. Check Your Spelling Maybe.");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Method to notify if a recipe exceeds 300 calories
        static void NotifyCalorieExcess(Recipe recipe)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning!! The Recipe " + " " + recipe.Name + " " + "Exceeds 300 Calories.");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Delegate type for handling calorie notification
        public delegate void CalorieNotificationHandler(Recipe recipe);

        // Main program execution
        class Program
        {
            static void Main(string[] args)
            {
                try
                {
                    // Create a list to store recipes
                    List<Recipe> recipes = new List<Recipe>();
                    // Define a calorie notification handler
                    CalorieNotificationHandler calorieNotification = NotifyCalorieExcess;

                    // Welcome message
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Welcome To Peace's Recipe App");
                    Console.ResetColor();
                    // Main menu loop
                    while (true)
                    {
                        Console.WriteLine("Choose Your Option From Below:");
                        Console.WriteLine("1- Enter New recipe");
                        Console.WriteLine("2- Display List of Recipes");
                        Console.WriteLine("3- Display Recipe Details");
                        Console.WriteLine("4- Scale Recipe");
                        Console.WriteLine("5- Reset Recipe Quantities");
                        Console.WriteLine("6- Clear Recipe");
                        Console.WriteLine("7- Delete Recipe");
                        Console.WriteLine("8- Exit");

                        Console.Write("Enter Your Choice  (e.g. 1): ");

                        string input = Console.ReadLine().Trim(); // Trim to remove leading and trailing spaces
                        Console.WriteLine(); // Add a newline for better readability

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Input. Please Enter a Valid option.");
                            Console.ResetColor();
                            Console.WriteLine();
                            continue;
                        }

                        int choice;
                        if (!int.TryParse(input, out choice))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Input. Please Enter a Valid Option.");
                            Console.ResetColor();
                            continue;
                        }

                        // Perform the selected action based on user input
                        if (choice == 1)
                        {
                            Recipe newRecipe = EnterNewRecipeDetails();
                            recipes.Add(newRecipe);
                            Console.WriteLine();
                        }
                        else if (choice == 2)
                        {
                            DisplayRecipeList(recipes);
                            Console.WriteLine();
                        }
                        else if (choice == 3)
                        {
                            DisplaySelectedRecipe(recipes, calorieNotification);
                            Console.WriteLine();
                        }
                        else if (choice == 4)
                        {
                            // Prompt the user to enter the name of the recipe to scale
                            Console.Write("Enter the Recipe Name You Want to Scale : ");
                            string recipeName = Console.ReadLine();
                            Recipe recipe = recipes.Find(r => r.Name == recipeName);
                            if (recipe != null)
                            {
                                // Prompt the user to enter the scaling factor
                                Console.Write("Enter Your Scaling Factor (0.5, 2, or 3): ");
                                double factor;
                                // Validate the scaling factor input
                                while (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Yoh: Invalid Input For Scaling Factor. Please Enter 0.5, 2, or 3.");
                                    Console.ResetColor();
                                }
                                // Scale the recipe
                                recipe.ScaleRecipe(factor);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Recipe not found. Check Your Spelling Maybe.");
                                Console.ResetColor();
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 5)
                        {
                            // Prompt the user to enter the name of the recipe to reset quantities
                            Console.Write("Enter the Recipe Name You Want Its Qualities to Reset: ");
                            string recipeName = Console.ReadLine();
                            Recipe recipe = recipes.Find(r => r.Name == recipeName);
                            if (recipe != null)
                            {
                                // Reset the quantities of ingredients in the recipe
                                recipe.ResetRecipeQuantities();
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Recipe not found. Check Your Spelling Maybe.");
                                Console.ResetColor();
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 6)
                        {
                            // Prompt the user to enter the name of the recipe to clear
                            Console.Write("Enter the Recipe Name You Want to Clear : ");
                            string recipeName = Console.ReadLine();
                            Recipe recipe = recipes.Find(r => r.Name == recipeName);
                            if (recipe != null)
                            {
                                // Clear the recipe
                                recipe.ClearRecipe();
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Recipe not found. Check Your Spelling Maybe.");
                                Console.ResetColor();
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 7)
                        {
                            // Prompt the user to enter the name of the recipe to delete
                            Console.Write("Enter the Recipe Name You Want To Delete: ");
                            string recipeNameToDelete = Console.ReadLine();
                            int initialCount = recipes.Count;
                            // Remove the recipe from the list if found
                            recipes.RemoveAll(r => r.Name == recipeNameToDelete);
                            int finalCount = recipes.Count;
                            // Check if the recipe was deleted successfully
                            if (initialCount > finalCount)
                            {
                                Console.WriteLine("Recipe deleted successfully.");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Recipe not found. Check Your Spelling Maybe.");
                                Console.ResetColor();
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 8)
                        {
                            // Exit the program
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Thank You For Using Peace's Recipe App. Good Byee!! See You Next Time.");
                            Console.ResetColor();
                            Console.WriteLine();
                           
                            return;
                        }
                        else
                        {
                            // Display error message for invalid choice
                            Console.WriteLine("Invalid choice. Try again.");
                            Console.WriteLine();
                        }
                    }

                }
                catch (Exception)
                {
                    // Handle unexpected errors
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Ooops! Try Again.");
                    Console.ResetColor();
                }
            }
        }
    }
}

