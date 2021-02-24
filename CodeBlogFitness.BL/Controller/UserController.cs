using CodeBlogFitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeBlogFitness.BL.Controller {
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    public class UserController {
        /// <summary>
        /// Пользователи приложения.
        /// </summary>
        public List<User> Users { get; }
        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public User CurrentUser { get; }
        public bool IsNewUser { get; } = false;
        /// <summary>
        /// Создание нового контроллера пользователя.
        /// </summary>
        /// <param name="user"> Имя пользователя. </param>
        public UserController(string userName) {
            if (string.IsNullOrWhiteSpace(userName)) {
                throw new ArgumentNullException("Имя пользователя не может быть пустым",nameof(userName));
            }
            Users = GetUsersData();

            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);
            if (CurrentUser == null) {
                CurrentUser = new User(userName);
                IsNewUser = true;
                Users.Add(CurrentUser);
                Save();
            }
        }
        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save() {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.dat",FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs,Users);
            }
        }
        /// <summary>
        /// Получить сохраненный список пользователей.
        /// </summary>
        /// <returns> Список пользователей приложения. </returns>
        private List<User> GetUsersData() {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate)) {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<User> users) {
                    return users;
                } else {
                    return new List<User>();
                }
            }
        }
        public void SetNewUserData(string genderName, DateTime birthDate, double weigth = 1, double height = 1) {
            // TODO: проверка
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weigth = weigth;
            CurrentUser.Height = height;
            Save();
        }
    }
}
