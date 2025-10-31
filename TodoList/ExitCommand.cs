using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    internal class ExitCommand : ICommand
    {
		public void Execute()
		{
			Environment.Exit (0);	
		}
	}
}
