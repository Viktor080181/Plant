using SQLConnection.Models;

namespace SQLConnection.Services
{
	public class PlantService
	{

        static void Main(string[] args)
        {
            using var context = new DatabaseConnection();

            // Подключение к базе данных
            try
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Подключение к базе данных успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                return;
            }

            // Основное меню
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Отобразить всю информацию");
                Console.WriteLine("2. Отобразить все названия");
                Console.WriteLine("3. Отобразить все цвета");
                Console.WriteLine("4. Показать максимальную калорийность");
                Console.WriteLine("5. Показать минимальную калорийность");
                Console.WriteLine("6. Показать среднюю калорийность");
                Console.WriteLine("7. Показать количество овощей");
                Console.WriteLine("8. Показать количество фруктов");
                Console.WriteLine("9. Показать количество заданного цвета");
                Console.WriteLine("10. Показать количество каждого цвета");
                Console.WriteLine("11. Показать с калорийностью ниже указанной");
                Console.WriteLine("12. Показать с калорийностью выше указанной");
                Console.WriteLine("13. Показать с калорийностью в диапазоне");
                Console.WriteLine("14. Показать желтые и красные");
                Console.WriteLine("0. Выход");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayAllProducts(context);
                        break;
                    case "2":
                        DisplayAllNames(context);
                        break;
                    case "3":
                        DisplayAllColors(context);
                        break;
                    case "4":
                        ShowMaxKaloriinost(context);
                        break;
                    case "5":
                        ShowMinKaloriinost(context);
                        break;
                    case "6":
                        ShowAverageKaloriinost(context);
                        break;
                    case "7":
                        ShowCountByPlantType(context, "Овощ");
                        break;
                    case "8":
                        ShowCountByPlantType(context, "Фрукт");
                        break;
                    case "9":
                        ShowCountByColor(context);
                        break;
                    case "10":
                        ShowCountByEachColor(context);
                        break;
                    case "11":
                        ShowProductsBelowKaloriinost(context);
                        break;
                    case "12":
                        ShowProductsAboveKaloriinost(context);
                        break;
                    case "13":
                        ShowProductsInRangeKaloriinost(context);
                        break;
                    case "14":
                        ShowYellowOrRed(context);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }


        public bool AddPlant(Plant plant)
		{
			try
			{
				DatabaseConnection dbConnection = new DatabaseConnection();

				dbConnection.Plants.Add(plant);
				dbConnection.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return false;
		}

		
        private static void DisplayAllProducts(DatabaseConnection context)
        {
            var products = context.Plants.ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.PlantType} - {product.Color} - {product.Kaloriinost} ккал");
            }
        }

        private static void DisplayAllNames(DatabaseConnection context)
        {
            var names = context.Plants.Select(p => p.Name).ToList();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }

        private static void DisplayAllColors(DatabaseConnection context)
        {
            var colors = context.Plants.Select(p => p.Color).Distinct().ToList();
            foreach (var color in colors)
            {
                Console.WriteLine(color);
            }
        }

        private static void ShowMaxKaloriinost(DatabaseConnection context)
        {
            var maxKaloriinost = context.Plants.Max(p => p.Kaloriinost);
            Console.WriteLine($"Максимальная калорийность: {maxKaloriinost} ккал");
        }

        private static void ShowMinKaloriinost(DatabaseConnection context)
        {
            var minKaloriinost = context.Plants.Min(p => p.Kaloriinost);
            Console.WriteLine($"Минимальная калорийность: {minKaloriinost} ккал");
        }

        private static void ShowAverageKaloriinost(DatabaseConnection context)
        {
            var averageKaloriinost = context.Plants.Average(p => p.Kaloriinost);
            Console.WriteLine($"Средняя калорийность: {averageKaloriinost} ккал");
        }

        private static void ShowCountByPlantType(DatabaseConnection context, string PlantType)
        {
            var count = context.Plants.Count(p => p.PlantType == PlantType);
            Console.WriteLine($"Количество {PlantType}: {count}");
        }

        private static void ShowCountByColor(DatabaseConnection context)
        {
            Console.Write("Введите цвет: ");
            var color = Console.ReadLine();
            var count = context.Plants.Count(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Количество {color}: {count}");
        }

        private static void ShowCountByEachColor(DatabaseConnection context)
        {
            var colorCounts = context.Plants.GroupBy(p => p.Color)
                                               .Select(g => new { Color = g.Key, Count = g.Count() })
                                               .ToList();
            foreach (var item in colorCounts)
            {
                Console.WriteLine($"{item.Color}: {item.Count}");
            }
        }

        private static void ShowProductsBelowKaloriinost(DatabaseConnection context)
        {
            Console.Write("Введите максимальную калорийность: ");
            if (double.TryParse(Console.ReadLine(), out double maxKaloriinost))
            {
                var products = context.Plants.Where(p => p.Kaloriinost < maxKaloriinost).ToList();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name} - {product.Kaloriinost} ккал");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }

        private static void ShowProductsAboveKaloriinost(DatabaseConnection context)
        {
            Console.Write("Введите минимальную калорийность: ");
            if (double.TryParse(Console.ReadLine(), out double minKaloriinost))
            {
                var products = context.Plants.Where(p => p.Kaloriinost > minKaloriinost).ToList();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name} - {product.Kaloriinost} ккал");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }

        private static void ShowProductsInRangeKaloriinost(DatabaseConnection context)
        {
            Console.Write("Введите минимальную калорийность: ");
            double minKaloriinost = double.Parse(Console.ReadLine());

            Console.Write("Введите максимальную калорийность: ");
            double maxKaloriinost = double.Parse(Console.ReadLine());

            var products = context.Plants.Where(p => p.Kaloriinost >= minKaloriinost && p.Kaloriinost <= maxKaloriinost).ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Kaloriinost} ккал");
            }
        }

        private static void ShowYellowOrRed(DatabaseConnection context)
        {
            var products = context.Plants.Where(p => p.Color.Equals("желтый", StringComparison.OrdinalIgnoreCase) ||
                                                        p.Color.Equals("красный", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Color}");
            }
        }
    }
}

