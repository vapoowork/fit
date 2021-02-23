using CodeBlogFitness.BL.Controller;
using System;

namespace CodeBlogFitness.CMD {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Вас приветствует приложение CodeBlogFitness");
            Console.WriteLine("Введите имя пользователя");
            var name = Console.ReadLine();

            var userController = new UserController(name);
            if (userController.IsNewUser) {
                Console.WriteLine("Введите пол: ");
                var genger = Console.ReadLine();
                var birthDate = ParseDateTime();
                var weigth = ParseDouble("вес");
                var height = ParseDouble("рост");

                userController.SetNewUserData(genger, birthDate, weigth, height);
            }
            Console.WriteLine(userController.CurrentUser);
            Console.ReadLine();
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
                    Console.WriteLine($"Неверный формат {name}");
                }
            }
        }
    }
}
