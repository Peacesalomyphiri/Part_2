namespace Prog_Recipe_App_Part2
{
    using System;
    internal class Program
    {
        class Recipe
        {

            // I made use of encapsulation 
            // Declared the data members as private since that way it will be protected from external access.
            private int numberOfIngredients;             // Stores the number of ingredients in the recipe
            private string[] ingredient_Names;            // Stores the names of the ingredients
            private double[] ingredient_Quantities;       // Stores the quantities of the ingredients
            private string[] ingredient_Units;
            private double[] original_Quantities;
            private int numberOfSteps;
            private string[] steps_Descriptions;
            private string recipeName;


            // This method allows a user to enter new recipe details

            public void Enter_New_Recipe_Details()
            {
                Console.WriteLine("Enter Name Of Recipe");
                recipeName = Console.ReadLine();

                Console.Write("Enter the number of ingredients to be added: ");
                numberOfIngredients = int.Parse(Console.ReadLine());

                ingredient_Names = new string[numberOfIngredients];
                ingredient_Quantities = new double[numberOfIngredients];
                ingredient_Units = new string[numberOfIngredients];
                original_Quantities = new double[numberOfIngredients];

                for (int i = 0; i < numberOfIngredients; i++)
                {
                    Console.Write("Enter The" + " " + (i + 1) + "St" + " " + "Ingredient  Name:");
                    ingredient_Names[i] = Console.ReadLine();

                    Console.Write("Enter The" + " " + (i + 1) + "St" + " " + "Ingredient Quantity:");
                    ingredient_Quantities[i] = double.Parse(Console.ReadLine());
                    original_Quantities[i] = ingredient_Quantities[i];

                    Console.Write("Enter The" + " " + (i + 1) + "St" + " " + "Ingredient  Unit Of Measuremnt:");
                    ingredient_Units[i] = Console.ReadLine();
                }

                Console.Write("Enter The Number Of Steps: ");
                numberOfSteps = int.Parse(Console.ReadLine());

                steps_Descriptions = new string[numberOfSteps];

                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.Write("Enter The" + " " + (i + 1) + "St" + " " + "Step Description:");
                    steps_Descriptions[i] = Console.ReadLine();
                }
            }

            //This method displays the recent recipe that was added. 
            //It goes through the ingredient arrays as well as the steps arrays and displays the necessary values
            public void Display_Recipe()

            //on the if statement, it handles the case when a user tries to display a recipe without entering any details

            {
                if (numberOfIngredients == 0 && numberOfSteps == 0)
                {
                    Console.WriteLine("No Recipe Added, Please Enter New Recipe! ");
                    return;
                }
                Console.WriteLine("Recently Added Ingredients:");
                Console.WriteLine();

                for (int i = 0; i < numberOfIngredients; i++)
                {
                    Console.WriteLine(ingredient_Quantities[i] + " " + ingredient_Units[i] + " " + ingredient_Names[i]);
                }

                Console.WriteLine();
                Console.WriteLine("\nSteps:");
                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.WriteLine((i + 1) + ". " + steps_Descriptions[i]);
                }
            }

            // The method scales the recipe by a given factor provided by the user, It multiplies the quantity by the scale factor

            public void Recipe_Scale_Factor(double scaleFactor)
            // double scaleFactor is declared within the Recipe_Scale_Factor method because it is specific to that method and is used for an input for scaling operation
            {
                for (int i = 0; i < numberOfIngredients; i++)
                {
                    ingredient_Quantities[i] = original_Quantities[i] * scaleFactor;
                }

                Console.WriteLine("Recipe Successfully Scaled.");
            }

            //it resets the recipe quantities to its original value
            public void Reset_Recipe_Quantities()
            {
                for (int i = 0; i < numberOfIngredients; i++)
                {
                    ingredient_Quantities[i] = original_Quantities[i];
                }

                Console.WriteLine("Recipe quantities reset to original values.");
            }

            //deletes and clears all data entered as a new recipe. Normally sets all variables and arrays to recipe to null and default values.
            public void Clear_Recipe()
            {
                numberOfIngredients = 0;
                ingredient_Names = null;
                ingredient_Quantities = null;
                ingredient_Units = null;
                original_Quantities = null;
                numberOfSteps = 0;
                steps_Descriptions = null;

                Console.WriteLine("Recipe data has been cleared.");
            }
            class Program
            {
                // Entry point of the RecipeApp
                static void Main(string[] args)
                {
                    Recipe recipe = new Recipe();


                    while (true)
                    {

                        Console.WriteLine("*****Welcome To Peace's Recipe App*****");
                        Console.WriteLine("Choose Your Option From Below:");
                        Console.WriteLine("1- Enter recipe details");
                        Console.WriteLine("2- Display recipe");
                        Console.WriteLine("3- Scale recipe");
                        Console.WriteLine("4- Reset recipe quantities");
                        Console.WriteLine("5- Clear recipe");
                        Console.WriteLine("6- Exit");

                        Console.Write("Enter your Option: ");
                        int choice = int.Parse(Console.ReadLine());

                        if (choice == 1)
                        {
                            recipe.Enter_New_Recipe_Details();
                        }
                        else if (choice == 2)
                        {
                            recipe.Display_Recipe();
                        }
                        else if (choice == 3)
                        {
                            Console.Write("Enter Your Scaling Factor (0.5, 2, or 3): ");
                            double factor = double.Parse(Console.ReadLine());
                            recipe.Recipe_Scale_Factor(factor);
                        }
                        else if (choice == 4)
                        {
                            recipe.Reset_Recipe_Quantities();
                        }
                        else if (choice == 5)
                        {
                            recipe.Clear_Recipe();
                        }
                        else if (choice == 6)
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Try again.");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
