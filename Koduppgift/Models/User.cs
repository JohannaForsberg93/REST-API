namespace Koduppgift.Models
	{
	public class User
		{
			public int Id { get; set; }
			public string Username { get; set; }
			public int Age { get; set; }
			public Role Role { get; set; }
			public ICollection<Group> Groups { get; set; } = new List<Group>();
		}
	}
