using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList;
internal class ProfileCommand : ICommand
{
		public Profile Profile { get; private set; }

		public ProfileCommand(Profile profile)
		{
			Profile = profile;
		}

		public void Execute()
		{
			Console.WriteLine($"Пользователь {Profile.GetInfo()}");
		}
}


