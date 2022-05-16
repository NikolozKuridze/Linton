using Linton.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain.Interfaces
{
	public interface IUnitOfWork
	{ 
		IPersonRepository Persons { get; }
		IBankRepository Banks { get; }
		Task CompleteAsync(); 
	}
}
