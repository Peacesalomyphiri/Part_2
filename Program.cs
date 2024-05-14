

namespace Prog_Recipe_App_Part2
{
    using System;
    using System.Collections.Generic;
    using static Prog_Recipe_App_Part2.Ingredient;

    internal class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public int TotalCalories { get; private set; }

        public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
            CalculateTotalCalories();
        }

        private void CalculateTotalCalories()
        {
            TotalCalories = 0;
            foreach (Ingredient ingredient in Ingredients)
            {
                TotalCalories += ingredient.Calories;

            }
        }

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
            if (TotalCalories > 300)
            {
                calorieNotification(this);
                Console.WriteLine();

            }
        }

        public void ScaleRecipe(double factor)
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
            CalculateTotalCalories();
            Console.WriteLine("Recipe " + " " + Name + " has been scaled by a factor of " + factor);
            Console.WriteLine();

        }

        public void ResetRecipeQuantities()
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity = 1;
            }
            CalculateTotalCalories();
            Console.WriteLine("Quantities for recipe " + " " + Name + " have been reset.");
            Console.WriteLine();

        }

        public void ClearRecipe()
        {
            Ingredients.Clear();
            Steps.Clear();
            TotalCalories = 0;
            Console.WriteLine("Recipe" + " " + Name + " has been cleared.");
            Console.WriteLine();

        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }


        static Recipe EnterNewRecipeDetails()
        {
            Console.Write("Enter the Recipe Name: ");
            string recipeName = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrEmpty(recipeName))
            {
                Console.WriteLine("Recipe name cannot be empty.");
                Console.WriteLine();
                return null;
            }

            Console.Write("Enter the Number of Ingredients to be Added: ");
            int numberOfIngredients;
            if (!int.TryParse(Console.ReadLine(), out numberOfIngredients) || numberOfIngredients <= 0)
            {
                Console.WriteLine("Invalid input for number of ingredients.");
                Console.WriteLine();
                return null;
            }
            Console.WriteLine();

            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.WriteLine("INGREDIENT " + (i + 1));
                Console.Write("Enter Ingredient Name: ");
                string ingredientName = Console.ReadLine();
                if (string.IsNullOrEmpty(ingredientName))
                {
                    Console.WriteLine("Ingredient name cannot be empty.");
                    return null;
                }

                Console.Write("Enter Ingredient Quantity: ");
                double ingredientQuantity;
                if (!double.TryParse(Console.ReadLine(), out ingredientQuantity) || ingredientQuantity <= 0)
                {
                    Console.WriteLine("Invalid input for ingredient quantity.");
                    return null;
                }


                Console.Write("Enter Ingredient  Unit Of Measurement" + " " + ":");
                string ingredientUnit = Console.ReadLine();


                Console.Write("Enter Ingredient Calories " + " " + ":");
                int ingredientCalories = int.Parse(Console.ReadLine());


                Console.Write("Enter Ingredient Food Group " + " " + ":");
                string ingredientFoodGroup = Console.ReadLine();


                Ingredient ingredient = new Ingredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, ingredientFoodGroup);
                ingredients.Add(ingredient);
                Console.WriteLine();

            }

            Console.Write("Enter The Number Of Steps: ");
            int numberOfSteps = int.Parse(Console.ReadLine());

            List<string> steps = new List<string>();
            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Write("Enter Step Description " + " " + (i + 1) + ":");
                string stepDescription = Console.ReadLine();
                steps.Add(stepDescription);
                Console.WriteLine();

            }

            Recipe recipe = new Recipe(recipeName, ingredients, steps);
            return recipe;

            Console.WriteLine();
        }

        static void DisplayRecipeList(List<Recipe> recipes)


        {
            Console.WriteLine();
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                Console.WriteLine();
                return;
            }

            recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name));

            Console.WriteLine("List of Recipes:");
            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
                Console.WriteLine();

            }
        }

        static void DisplaySelectedRecipe(List<Recipe> recipes, CalorieNotificationHandler calorieNotification)
        {
            Console.WriteLine();
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                Console.WriteLine();
                return;
            }

            Console.Write("Enter the recipe name: ");
            string recipeName = Console.ReadLine();

            Console.WriteLine();

            Recipe recipe = recipes.Find(r => r.Name == recipeName);
            if (recipe != null)
            {
                recipe.DisplayRecipe(calorieNotification);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Recipe not found.");

                Console.WriteLine();
            }
        }

        static void NotifyCalorieExcess(Recipe recipe)
        {
            Console.WriteLine("Warning!! The recipe " + " "  + recipe.Name + " " + "exceeds 300 calories.");
            Console.WriteLine();
        }
        public delegate void CalorieNotificationHandler(Recipe recipe);
        class Program

        {

            static void Main(string[] args)
            {
                try
                {
                    List<Recipe> recipes = new List<Recipe>();
                    CalorieNotificationHandler calorieNotification = NotifyCalorieExcess;

                    Console.WriteLine("Welcome To Peace's Recipe App");
                    while (true)
                    {
                        Console.WriteLine("Choose Your Option From Below:");
                        Console.WriteLine("1- Enter new recipe");
                        Console.WriteLine("2- Display list of recipes");
                        Console.WriteLine("3- Display recipe details");
                        Console.WriteLine("4- Scale recipe");
                        Console.WriteLine("5- Reset recipe quantities");
                        Console.WriteLine("6- Clear recipe");
                        Console.WriteLine("7- Delete recipe");
                        Console.WriteLine("8- Exit");

                        Console.Write("Enter your Option: ");

                        string input = Console.ReadLine().Trim(); // Trim to remove leading and trailing spaces
                        Console.WriteLine(); // Add a newline for better readability

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid option.");
                            Console.WriteLine();
                            continue;
                        }

                        int choice;
                        if (!int.TryParse(input, out choice))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid option.");
                            Console.WriteLine();
                            continue;
                        }


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
                            Console.Write("Enter the recipe name: ");
                            string recipeName = Console.ReadLine();
                            Recipe recipe = recipes.Find(r => r.Name == recipeName);
                            if (recipe != null)
                            {
                                Console.Write("Enter Your Scaling Factor (0.5, 2, or 3): ");
                                double factor = double.Parse(Console.ReadLine());
                                recipe.ScaleRecipe(factor);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Recipe not found.");
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 5)
                        {
                            Console.Write("Enter the recipe name: ");
                            string recipeName = Console.ReadLine();
                            Recipe recipe = recipes.Find(r => r.Name == recipeName);
                            if (recipe != null)
                            {
                                recipe.ResetRecipeQuantities();
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Recipe not found.");
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 6)
                        {
                            Console.Write("Enter the recipe name: ");
                            string recipeName = Console.ReadLine();
                            Recipe recipe = recipes.Find(r => r.Name == recipeName);
                            if (recipe != null)
                            {
                                recipe.ClearRecipe();
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Recipe not found.");
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 7)
                        {
                            Console.Write("Enter the recipe name to delete: ");
                            string recipeNameToDelete = Console.ReadLine();
                            int initialCount = recipes.Count;
                            recipes.RemoveAll(r => r.Name == recipeNameToDelete);
                            int finalCount = recipes.Count;
                            if (initialCount > finalCount)
                            {
                                Console.WriteLine("Recipe deleted successfully.");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Recipe not found.");
                                Console.WriteLine();
                            }
                        }
                        else if (choice == 8)
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Try again.");
                            Console.WriteLine();
                        }
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("An unexpected error occurred.");
                }
            }
        }
    }
}
