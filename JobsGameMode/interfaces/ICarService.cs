using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace JobsGameMode.interfaces
{
	public interface ICarService : IService
	{
		public BaseVehicle CreateCar(VehicleModelType id, Vector3 position);
		public BaseVehicle CreateCar(VehicleModelType id, Vector3 position, float rotation);
	}
}
