using System;
using System.ComponentModel.Design;

namespace NumbersGame
{
    internal class Program
    {

        // Magdalena Kubien NET23

        static void Main(string[] args)
        {

            // default for how many attempts the user has, default setting
            int maxAmountOfGuesses = 5;

            // default for how many attempts the user has, default setting 
            int maxNumber = 20;

            //welcome
            Console.WriteLine("Välkommen! Jag tänker på ett nummer mellan 1 och 20. Kan du gissa vilket? Du får fem försök.");
            Console.WriteLine("");

            // game function with default settings!
            GameItself(maxNumber, maxAmountOfGuesses);

            // after the first game its possible to play with chosen difficulty level as long as user wishes to do it
            Console.Write("Vill du spela en gång till? J/N ");
            string yesNo = Console.ReadLine();
            Console.WriteLine("");

            // continue to play as long as user says yes
            while (yesNo.ToUpper() == "J")
            {
                Console.Clear();

                //let user choose the amount of numbers to guess among
                Console.Write("Ange antal nummer du vill gissa mellan: ");
                string input = Console.ReadLine();
                // the highest number I accept is 1000 so I check for that
                maxNumber = IsAnIntegerWithMax(input, 1000);
                Console.WriteLine("");

                //let user choose the amount of attempts he/she has
                Console.Write("Ange antal försök du vill ha på dig: ");
                input = Console.ReadLine();
                //it would be stupid to have more guessing chances than amount of numbers to guess among
                maxAmountOfGuesses = IsAnIntegerWithMax(input, maxNumber);
                Console.WriteLine("");

                // game time!
                Console.WriteLine("Jäääää-vlar va kul! Då kör vi!");
                Console.WriteLine("");
                GameItself(maxNumber, maxAmountOfGuesses);

                // continue or quit?
                Console.Write("Vill du spela en gång till? J/N ");
                yesNo = Console.ReadLine();
                Console.WriteLine("");

            }
        }
        static void GameItself(int maxNumber, int maxAmountOfGuesses)
        {
            // generates the random number that user will try to guess
            // max is exclusive so I have to add 1 to the second argument of the function
            Random random = new Random();
            int chosenNumber = random.Next(1, maxNumber + 1);

            // calculator for how many times user tried to guess
            int attemptNumber = 1;

            // loop for user's attempts to guess the random number
            do
            {
                // asks the user for a guess and saves it as an integer
                Console.Write($"Ditt försök nr {attemptNumber}: ");
                string guessString = Console.ReadLine();
                int guess = IsAnIntegerWithMax(guessString, maxNumber);

                //checks the result using the function Checkguess
                bool result = CheckGuess(guess, chosenNumber);

                // if the function returns true it breaks the loop, if its still false after max amount of attempts it prints a sad message
                if (result == true)
                {
                    break;
                }
                else if (result == false && attemptNumber == maxAmountOfGuesses)
                {
                    Console.WriteLine($"Ajdå, du lyckades inte gissa talet på {maxAmountOfGuesses} försök!");
                    Console.WriteLine("");
                }

                attemptNumber++;

            } while (attemptNumber <= maxAmountOfGuesses);
        }

        static bool CheckGuess(int guess, int chosenNumber)
            {

            // function CheckGuess returns false/true depending on if the user's guess was correct or not
            // it also informs the user if the guess was too low or high and how close it was

            //example responses depending on how far/close the guess was and if it was too high or too low
            string[] responseFar = { 
                    "Oh no, du är ju en evighet bort!", 
                    "Skojar du?"
                };
            string[] responseNear = {
                    "Inte så dumt.", 
                    "Så jäkla nära!"
                };
            string[] responseLow = {
                    "Hade varit bättre att gissa högre än så.", 
                    "För lågt." 
                };
            string[] responseHigh = { 
                    "För högt.", 
                    "Kanske ett lägre tal nästa gång?" 
                };

            // decide if a guess is near or far
            // logic today: diff 10%+ but minimum 3 is far away
            int diffAbs = Math.Abs(guess - chosenNumber);
            double diffRelative= Math.Abs(Convert.ToDouble(diffAbs) / Convert.ToDouble(chosenNumber) * 100);
            bool checkIfNear;
            if (diffAbs >= 3 && diffRelative >= 10) {
                    checkIfNear = false;
                }
            else
                {
                    checkIfNear = true;
                }

            // random variables to generate answers
            // at the moment just 2 options in each array (can check the arrays length in the future)
            Random random =new Random();
            int randomFarNear = random.Next(0, 2);
            int randomLowHigh = random.Next(0, 2);

            // depending on Low/High and Near/Far or a correct response, prints a message
            if (guess > chosenNumber)
            {
                    if (checkIfNear == true)
                    {
                        Console.WriteLine($"{responseNear[randomFarNear]} {responseHigh[randomLowHigh]}");
                    }
                    else {
                        Console.WriteLine($"{responseFar[randomFarNear]} {responseHigh[randomLowHigh]}");
                    }

                    Console.WriteLine(""); 
                    return false;
            }
            else if (guess < chosenNumber)
            {
                    if (checkIfNear == true)
                    {
                        Console.WriteLine($"{responseNear[randomFarNear]} {responseLow[randomLowHigh]}");
                    }
                    else
                    {
                        Console.WriteLine($"{responseFar[randomFarNear]} {responseLow[randomLowHigh]}");
                    }

                    Console.WriteLine("");
                    return false;
            }
            else
            {
                Console.WriteLine("Wohoo! Du klarade det!");
                Console.WriteLine("");
                return true;
            }

        }

        static int IsAnIntegerWithMax(string guessString, int maxNumber)
        {
            // this function takes input from the console, checks if its an integer within valid minimum and maximum
            // it continues until the guess is valid then saves it to an integer

            //prepares variables 
            bool correctFormat = false;
            int guessInt;

            do
            {                
                if (int.TryParse(guessString, out guessInt) && guessInt >= 1 && guessInt <= maxNumber + 1)
                {
                    correctFormat = true;
                }
                else
                {
                    Console.Write($"Välj ett tal mellan 1 och {maxNumber}. Prova igen: ");
                    guessString = Console.ReadLine();
                }
            } while (correctFormat == false);

            // returns the guessed integer
            return guessInt;
        }
    }
}