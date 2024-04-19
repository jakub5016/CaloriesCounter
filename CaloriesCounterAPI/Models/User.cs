namespace CaloriesCounterAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public int Weight { get; set; }

        public bool Gender { get; set; } // 1 - male

        public int Height { get; set; }

        public int Age { get; set; }

        public int BMR { get; set; }

        public int KcalMaintaince {  get; set; }

        public void calculateMaintainceAndBmi()
        {
            if (this.Gender)
            {
                this.BMR = Convert.ToInt32(10 * this.Weight + 6.25 * this.Height - 5 * this.Age + 5);

            }
            else
            {
                this.BMR = Convert.ToInt32(10 * this.Weight + 6.25 * this.Height - 5 * this.Age - 161);

            }

            this.KcalMaintaince = Convert.ToInt32(1.2 * this.BMR);
        }
    }
}
