namespace Koduppgift.Models
	{
	public class User
		{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int? RoleId { get; set; }
		public ICollection<Group> Groups { get; set; } = new List<Group>();
		}
	}
