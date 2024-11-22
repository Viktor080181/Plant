namespace SQLConnection.Models
{
	public class Plant
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string PlantType { get; set; } = null!;

        public string Color { get; set; } = null!;

		public uint Kaloriinost { get; set; }
	}
}
