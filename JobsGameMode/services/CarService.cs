using JobsGameMode.interfaces;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Factories;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobsGameMode.services
{
	public class CarService : ICarService
	{
		GameMode _gameMode;
		BaseVehicleFactory _vehicleFactory;
		public BaseMode GameMode => _gameMode;
		public CarService(GameMode gameMode)
		{
			_gameMode = gameMode;
			_vehicleFactory = new BaseVehicleFactory(gameMode);
		}


		public BaseVehicle CreateCar(VehicleModelType id, Vector3 position)
		{
			return CreateCar(id, position, 0);
		}

		public BaseVehicle CreateCar(VehicleModelType id, Vector3 position, float rotation)
		{
			return _vehicleFactory.Create(id, position, rotation, 0, 0);
		}
	}
}
