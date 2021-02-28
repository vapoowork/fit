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
    public class UserController : ControllerBase {
        private const string USER_FILE_NAME = "users.dat";
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
            Save(USER_FILE_NAME, Users);
        }
        /// <summary>
        /// Получить сохраненный список пользователей.
        /// </summary>
        /// <returns> Список пользователей приложения. </returns>
        private List<User> GetUsersData() {
            return Load<List<User>>(USER_FILE_NAME) ?? new List<User>();
        }
        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1) {
            // TODO: проверка
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }
    }
}
