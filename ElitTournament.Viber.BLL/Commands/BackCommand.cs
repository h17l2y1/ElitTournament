using ElitTournament.Viber.BLL.View;
using ElitTournament.Viber.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElitTournament.Viber.BLL.Commands
{
	public class BackCommand : Command
	{
		public BackCommand() : base("")
		{
		}

		public override void Execute(RootObject rootObject, IViberBotClient client)
		{
			throw new NotImplementedException();
		}
	}
}
