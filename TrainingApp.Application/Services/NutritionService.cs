using System;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp.Aplication.Services
{
    public class NutritionService : INutritionService
    {
        private readonly INutritionRepository _nutritionRepository;
        public NutritionService(INutritionRepository nutritionRepository)
        {
            _nutritionRepository = nutritionRepository;
        }

        public void CalculateAndAddNutritionStats(Nutrition nutrition)
        {
            nutrition.BMR = CalculateBMR(nutrition);
            nutrition.TDEE = CalculateTDEE(nutrition);
            CalculateMacronutrients(nutrition);

            _nutritionRepository.AddNutritionStats(nutrition);
        }

        public Nutrition GetNutritionStats(int id)
        {
            return _nutritionRepository.GetNutrition(id);
        }

        public List<Nutrition> GetAllNutritions()
        {
            return _nutritionRepository.GetAllNutrition()
                .ToList();
        }

        private decimal CalculateBMR(Nutrition nutrition)
        {
            if (nutrition.Gender == "M")
            {
                return 88.362m + (13.397m * nutrition.Weight) + (4.799m * nutrition.Height) - (5.677m * nutrition.Age);
            }
            else if (nutrition.Gender == "F")
            {
                return 447.593m + (9.247m * nutrition.Weight) + (3.098m * nutrition.Height) - (4.330m * nutrition.Age);
            }
            else
            {
                throw new ArgumentException("Invalid gender specified. Use 'M' for male and 'F' for female.");
            }
        }

        public decimal CalculateTDEE(Nutrition nutrition)
        {
            decimal bmr = CalculateBMR(nutrition);
            decimal activityFactor;

            switch (nutrition.ActivityLevel)
            {
                case 1:
                    activityFactor = 1.2m;
                    break;
                case 2:
                    activityFactor = 1.375m;
                    break;
                case 3:
                    activityFactor = 1.55m;
                    break;
                case 4:
                    activityFactor = 1.725m;
                    break;
                case 5:
                    activityFactor = 1.9m;
                    break;
                default:
                    throw new ArgumentException("Invalid activity level specified. Use a value between 1 and 5.");
            }

            return bmr * activityFactor;
        }

        public void CalculateMacronutrients(Nutrition nutrition)
        {
            decimal tdee = CalculateTDEE(nutrition);

            // Setting middle ground percentages for macronutrients
            nutrition.Calories = tdee;
            nutrition.Carbs = (tdee * 0.55m) / 4m; // Carbs have 4 calories per gram
            nutrition.Protein = (tdee * 0.20m) / 4m; // Protein has 4 calories per gram
            nutrition.Fat = (tdee * 0.25m) / 9m; // Fat has 9 calories per gram
        }
    }
}
