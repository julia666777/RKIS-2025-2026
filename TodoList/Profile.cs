

namespace TodoList;
internal class Profile
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int BirthYear { get; set; }

	public string GetInfo()
	{
		int currentYear = DateTime.Now.Year;
		return $"{FirstName} {LastName}, возраст {currentYear - BirthYear}";
	}

	public string ConvertToFileFormat()
	{
		return FirstName + "\n" + LastName + "\n" + BirthYear;
	}
}
