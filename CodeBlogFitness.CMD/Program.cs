using CodeBlogFitness.BL.Controller;
using CodeBlogFitness.BL.Model;
using System;

namespace CodeBlogFitness.CMD {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Вас приветствует приложение CodeBlogFitness");
            Console.WriteLine("Введите имя пользователя");
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);
            if (userController.IsNewUser) {
                Console.WriteLine("Введите пол: ");
                var genger = Console.ReadLine();
                var birthDate = ParseDateTime();
                var weight = ParseDouble("вес");
                var height = ParseDouble("рост");

                userController.SetNewUserData(genger, birthDate, weight, height);
            }
            Console.WriteLine(userController.CurrentUser);

            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("E - Ввести прием пищи");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.E) {
                var foods = EnterEating();
                eatingController.Add(foods.Food,foods.Weight);
                foreach (var item in eatingController.Eating.Foods) {
                    Console.WriteLine($"\t{item.Key} - {item.Value}");
                }
            }

            Console.ReadLine();
        }

        private static (Food Food,double Weight) EnterEating() {
            Console.WriteLine("Введите имя продукта");
            var food = Console.ReadLine();

            var calories = ParseDouble("калорийность");
            var prots = ParseDouble("белки");
            var fats = ParseDouble("жиры");
            var carbs = ParseDouble("углеводы");

            var weight = ParseDouble("вес порции");
            var product = new Food(food,calories,prots,fats,carbs);
            return (Food: product, Weight: weight);
        }

        private static DateTime ParseDateTime() {
            DateTime birthDate;
            while (true) {
                Console.WriteLine("Введите дату рождения (dd.mm.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate)) {
                    break;
                } else {
                    Console.WriteLine("Неверный формат даты рождения");
                }
            }

            return birthDate;
        }

        private static double ParseDouble(string name) {
            while (true) {
                Console.WriteLine($"Введите {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value)) {
                    return value;
                } else {
                    Console.WriteLine($"Неверный формат поля {name}");
                }
            }
        }
    }
}
