using System.Globalization;

namespace ONW_API.Application.Waze
{
    public class GenerateWazeLinkUseCase
    {
        public string Execute(double destinationLatitude, double destinationLongitude)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "https://waze.com/ul?ll={0},{1}&navigate=yes",
                destinationLatitude,
                destinationLongitude
            );
        }
    }
}
