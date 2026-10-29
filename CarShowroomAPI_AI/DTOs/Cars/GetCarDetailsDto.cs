namespace CarShowroomAPI_AI.DTOs.Cars
{
    public class GetCarDetailsDto : CarDto
    {
        public List<CarFeaturesDto> Features { get; set; }
    }

    public class CarFeaturesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
