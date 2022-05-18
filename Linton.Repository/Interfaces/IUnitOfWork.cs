using Linton.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Repository
{
	public interface IUnitOfWork
	{
		IPersonRepository Persons { get; }
		IBankRepository Banks { get; }
		Task CompleteAsync();
	}
}
