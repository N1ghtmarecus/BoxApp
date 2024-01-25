﻿using BoxApp.DataProviders;

namespace BoxApp.Services;

public class FilterBoxesProvider(IBoxesProvider boxesProvider) : IFilterBoxesProvider
{
    private readonly IBoxesProvider _boxesProvider = boxesProvider;
    private bool isSearchComplete = false;

    public void FilterBoxes()
    {
        DisplayInformationMenu();
        ChooseOptions();

        while (true)
        {
            var choice = Console.ReadLine();

            switch (choice?.ToUpper())
            {
                case "1":
                    DisplayInformationMenu();
                    GetUniqueGrammage();
                    ChooseOptions();
                    break;

                case "2":
                    DisplayInformationMenu();
                    GetMaxLength();
                    ChooseOptions();
                    break;

                case "3":
                    DisplayInformationMenu();
                    GetBoxesOrderedByLength();
                    ChooseOptions();
                    break;

                case "4":
                    DisplayInformationMenu();
                    GetBoxesOrderedByWidth();
                    ChooseOptions();
                    break;

                case "5":
                    DisplayInformationMenu();
                    GetBoxesOrderedByHeight();
                    ChooseOptions();
                    break;

                case "6":
                    DisplayInformationMenu();
                    GetBoxesWithSpecificHeight();
                    ChooseOptions();
                    break;

                case "7":
                    return;

                default:
                    Console.Write($"\nYour input '{choice}' is not correct! Please try again: ");
                    continue;
            }
        }
    }

    private static void DisplayInformationMenu()
    {
        Console.Clear();
        Console.Write("\n     SUBMENU: More Informations  \n" +
                "\nWhat specific details would you like to review?\n" +
                "1. Get unique <Grammage>\n" +
                "2. Get max <Length>\n" +
                "3. Get boxes ordered by <Length>\n" +
                "4. Get boxes ordered by <Width>\n" +
                "5. Get boxes ordered by <Height>\n" +
                "6. Get boxes with specific <Height>\n" +
                "7. Back to MAIN MENU\n");
    }

    private static void ChooseOptions()
    {
        Console.Write("\nChoose an option (1-7): ");
    }

    private void GetUniqueGrammage()
    {
        Console.WriteLine("\nAll unique Grammage: ");
        foreach (var box in _boxesProvider.GetUniqueBoxGrammage())
        {
            Console.WriteLine(box);
        }
    }

    private void GetMaxLength()
    {
        Console.Write($"\nMax Length of all boxes: {_boxesProvider.GetMaxLengthOfAllBoxes()}\n");
    }

    private void GetBoxesOrderedByLength()
    {
        Console.WriteLine("\nAll boxes ordered by length: ");
        foreach (var box in _boxesProvider.OrderByLength())
        {
            Console.WriteLine(box);
        }
    }

    private void GetBoxesOrderedByWidth()
    {
        Console.WriteLine("\nAll boxes ordered by width: ");
        foreach (var box in _boxesProvider.OrderByWidth())
        {
            Console.WriteLine(box);
        }
    }

    private void GetBoxesOrderedByHeight()
    {
        Console.WriteLine("\nAll boxes ordered by height: ");
        foreach (var box in _boxesProvider.OrderByHeight())
        {
            Console.WriteLine(box);
        }
    }

    private void GetBoxesWithSpecificHeight()
    {
        Console.Write("\nPlease enter the height value which are you looking for: ");

        while (!isSearchComplete)
        {
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int intValue))
            {
                var boxesWithHeight = _boxesProvider.GetAllBoxesWithSpecificHeight(intValue);

                if (boxesWithHeight.Count > 0)
                {
                    Console.WriteLine();
                    foreach (var box in boxesWithHeight)
                    {
                        Console.WriteLine(box);
                    }
                    break;
                }
                else
                {
                    SecondAndNextInput();
                }
            }
            else
            {
                Console.Write($"\nYour input '{userInput}' is not a valid number! Please enter a correct value: ");
            }
        }
    }

    private void SecondAndNextInput()
    {
        Console.Write("\nThere are no boxes with this height! Do you want to type a different height value? Y/N: ");

        while (true)
        {
            var secondInput = Console.ReadLine()?.ToUpper();

            switch (secondInput)
            {
                case "Y":
                    Console.Write("\nPlease enter the height value which are you looking for: ");
                    return;

                case "N":
                    isSearchComplete = true;
                    return;

                default:
                    Console.Write($"\nYour input '{secondInput}' is not a Y/N choice! Please enter a correct value: ");
                    break;
            }
        }
    }
}