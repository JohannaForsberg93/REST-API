namespace Koduppgift.Models
	{
	public class Group
		{
			public int Id { get; set; }
			public string GroupName { get; set; }
			private ICollection<User> Users { get; set; } = new List<User>();
		}
	}
