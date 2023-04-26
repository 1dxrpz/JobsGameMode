using JobsGameMode.entity;
using JobsGameMode.interfaces;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobsGameMode
{
	[PooledType]
	public class Player : BasePlayer
	{
		IAccountService _accountService;

		// Crate needed to be carried to checkpoint to gain 1 exp
		Pickup _sourcePickup;

		// Multiplyer that affects on how many exp player must gain to level up
		int _levelMultiplyer = 100;

		public Player()
		{
			_accountService = GameMode.Instance.Services.GetService<IAccountService>();
		}

		// Assign default values if player not found in database & set job point
		public override async void OnConnected(EventArgs e)
		{
			SetCheckpoint(new Vector3(-13.073224, 47.82628, 3.1171875), 2);
			
			Account account = await _accountService.GetAccount(Name);
			if (account == null)
			{
				await _accountService.AddAccount(new Account()
				{
					Name = Name,
					Money = Money,
					WorkExp = 0,
					WorkLevel = 1
				});
			}
		}

		[Command("skill")]
		private async void SkillInfo()
		{
			Account account = await _accountService.GetAccount(Name);
			MessageDialog skillDialog = new MessageDialog("Job Skill", "", "Close");
			skillDialog.Style = DialogStyle.MessageBox;
			skillDialog.Message = $"Level: {account.WorkLevel}\n" +
				$"Experience: {account.WorkExp}\n" +
				$"Exp to next Level: {account.WorkLevel * _levelMultiplyer - account.WorkExp}";
			skillDialog.Show(this);
		}

		// Start / End job dialogue checkpoint
		public override void OnEnterCheckpoint(EventArgs e)
		{
			string jobAction = _accountService.IsWorking(Id) ? "End" : "Start";
			MessageDialog jobDialog = new MessageDialog("Job", jobAction + " Job?", "Close", jobAction);
			jobDialog.Show(this);
			jobDialog.Response += JobDialog;
		}

		// Start / End job dialogue Event
		private async void JobDialog(object sender, DialogResponseEventArgs e)
		{
			if (e.DialogButton == DialogButton.Right)
			{
				if (_accountService.IsWorking(Id))
				{
					Account account = await _accountService.GetAccount(Name);
					for (int i = 0; i < _accountService.GetActionCount(Id); i++)
					{
						account.WorkExp += 1;
						if (account.WorkExp >= _levelMultiplyer)
							account.WorkLevel++;
					}
					account.Money += account.WorkLevel * 100;
					await _accountService.UpdateAccount(account);
					Money += account.Money;
					_accountService.RewokeJob(Id);
					EndJob();
				}
				else
				{
					_accountService.AddToJob(Id);
					CreateCrate();
				}
			}
		}

		// Crate Pickup event
		public override void OnPickUpPickup(PickUpPickupEventArgs e)
		{
			var color = SampSharp.GameMode.SAMP.Color.White;
			SetAttachedObject(0, 1271, Bone.RightHand, Vector3.Zero, Vector3.Zero, Vector3.One, color, color);
			SetRaceCheckpoint(CheckpointType.Normal, new Vector3(-10.18743, 55.07592, 3.1171875), new Vector3(24.958004, 27.488297, 3.1171875), 2);
		}

		// Crate Dropdown event
		public override void OnEnterRaceCheckpoint(EventArgs e)
		{
			_accountService.AddActionCount(Id, 1);

			RemoveAttachedObject(0);
			CreateCrate();
			DisableRaceCheckpoint();
		}

		void EndJob()
		{
			RemoveAttachedObject(0);
			DisableRaceCheckpoint();
			_sourcePickup.Dispose();
		}

		void CreateCrate()
		{
			_sourcePickup = Pickup.Create(1271, PickupType.ShowTillPickedUp, new Vector3(22.830042, 26.20143, 3.1171875));
		}
	}
}