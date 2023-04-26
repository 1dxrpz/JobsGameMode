using SampSharp.Core;
using System.Text;

namespace JobsGameMode
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new GameModeBuilder()
				.Use<GameMode>()
				.UseEncoding(Encoding.UTF8)
				.Run();
		}
	}
}
