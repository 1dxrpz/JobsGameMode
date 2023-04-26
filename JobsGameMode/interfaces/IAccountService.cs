using JobsGameMode.entity;
using SampSharp.GameMode;
using System.Threading.Tasks;

namespace JobsGameMode.interfaces
{
	public interface IAccountService : IService
	{
		public bool IsWorking(int id);
		public void AddToJob(int id);
		public void RewokeJob(int id);
		public void AddActionCount(int id, int count);
		public int GetActionCount(int id);
		public Task<Account> GetAccount(string name);
		public Task AddAccount(Account account);
		public Task UpdateAccount(Account account);

	}
}
