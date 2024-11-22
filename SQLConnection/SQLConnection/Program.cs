using SQLConnection.Services;
using SQLConnection.Models;

PlantService plantService = new PlantService();

plantService.AddPlant(new Plant()
{
	Name = "Кабачёк",
	PlantType = "Овощь",
	Color = "Синий",
	Kaloriinost = 10
});

plantService.AddPlant(new Plant()
{
	Name = "Баклажан",
	PlantType = "Фрукт",
	Color = "Красный",
	Kaloriinost = 100
});

plantService.AddPlant(new Plant()
{
	Name = "Яблоко",
	PlantType = "Фрукт",
	Color = "Зелёное",
	Kaloriinost = 50
});

plantService.AddPlant(new Plant()
{
	Name = "Абрикос",
	PlantType = "Фрукт",
	Color = "Оранжевый",
	Kaloriinost = 90
});

