using JobsGameMode.entity;
using JobsGameMode.interfaces;
using Microsoft.EntityFrameworkCore;
using SampSharp.GameMode;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobsGameMode.services
{
	public class AccountService : IAccountService
	{
		Dictionary<int, int> _workingPlayers;
		ApplicationContext _context;
		BaseMode _gameMode;

		public BaseMode GameMode => _gameMode;
		public AccountService(GameMode gameMode)
		{
			_context = new ApplicationContext();
			_gameMode = GameMode;
			_workingPlayers = new Dictionary<int, int>();
		}

		public async Task<Account> GetAccount(string name)
		{
			var account = await _context.Accounts.FirstOrDefaultAsync(v => v.Name == name);
			return account;
		}

		public async Task AddAccount(Account account)
		{
			await _context.Accounts.AddAsync(account);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAccount(Account account)
		{
			var user = await _context.Accounts.FirstOrDefaultAsync(v => v.Name == account.Name);
			user.WorkExp = account.WorkExp;
			user.WorkLevel = account.WorkLevel;
			user.Name = account.Name;
			user.Money = account.Money;
			_context.Accounts.Update(user);
			await _context.SaveChangesAsync();
		}

		public bool IsWorking(int id)
		{
			return _workingPlayers.ContainsKey(id);
		}

		public void AddToJob(int id)
		{
			_workingPlayers.Add(id, 0);
		}

		public void RewokeJob(int id)
		{
			_workingPlayers.Remove(id);
		}

		public void AddActionCount(int id, int count)
		{
			_workingPlayers[id] += count;
		}

		public int GetActionCount(int id)
		{
			return _workingPlayers[id];
		}
	}
}
