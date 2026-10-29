namespace CarShowroomAPI_AI.DTOs
{
    public class PaginationDto<T> where T : class
    {
        public List<T> List { get; set; }
        public int Count { get; set; }
    }
}
