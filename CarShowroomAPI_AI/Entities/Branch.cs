namespace CarShowroomAPI_AI.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
