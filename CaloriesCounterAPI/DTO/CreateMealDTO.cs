namespace CaloriesCounterAPI.DTO
{
    public class CreateMealDTO
    {
        public String Type { get; set; }

        public DateOnly Date { get; set; }

        public List<int>? ProductIds { get; set; }
    }
}
