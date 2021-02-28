using System;

namespace CodeBlogFitness.BL.Model {
    [Serializable]
    public class Food {
        public string Name { get; }
        /// <summary>
        /// Белки.
        /// </summary>
        public double Proteins { get; }
        /// <summary>
        /// Жиры.
        /// </summary>
        public double Fats { get; }
        /// <summary>
        /// Углеводы.
        /// </summary>
        public double Carbohydrates { get; }
        /// <summary>
        /// Калории на 100г.
        /// </summary>
        public double Calories { get; }

        public Food(string name) : this(name, 0, 0, 0, 0) {
            Name = name;
        }

        public Food(string name, double calories,double proteins,double fats,double carbohydrates) {
            // TODO: проверка
            Name = name;
            Calories = calories/100.0;
            Proteins = proteins / 100.0;
            Fats = fats / 100.0;
            Carbohydrates = carbohydrates / 100.0;
        }
        public override string ToString() {
            return Name;
        }

    }
}
