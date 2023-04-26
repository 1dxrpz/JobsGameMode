using JobsGameMode.interfaces;
using JobsGameMode.services;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using System;

namespace JobsGameMode
{
	public class GameMode : BaseMode
	{
		ICarService _carService;

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			SetGameModeText("Blank game mode");

			AddPlayerClass(158, new Vector3(10, 10, 10), 0);
			AddPlayerClass(159, new Vector3(10, 10, 10), 0);
			AddPlayerClass(160, new Vector3(10, 10, 10), 0);
			AddPlayerClass(161, new Vector3(10, 10, 10), 0);

			Services.AddService(typeof(ICarService), new CarService(this));
			Services.AddService(typeof(IAccountService), new AccountService(this));

			_carService = GameMode.Instance.Services.GetService<ICarService>();

			var vehicle = _carService.CreateCar(VehicleModelType.Walton, new Vector3(26.331242, 26.336506, 3.1171875), 272.99692f);
			vehicle.SetParameters(false, false, false, false, false, false, false);
			vehicle.Velocity = Vector3.Zero;
		}
	}
}