using System;

namespace CaloriesCounterAPI.Models
{
    /// <summary>
    /// Model class representing a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the weight of the user.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user. (1 - male)
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// Gets or sets the height of the user.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the Basal Metabolic Rate (BMR) of the user.
        /// </summary>
        public int BMR { get; set; }

        /// <summary>
        /// Gets or sets the calorie maintenance level of the user.
        /// </summary>
        public int KcalMaintaince { get; set; }

        /// <summary>
        /// Calculates the Basal Metabolic Rate (BMR) and calorie maintenance level of the user.
        /// </summary>
        public void calculateMaintainceAndBmi()
        {
            if (Gender)
            {
                BMR = Convert.ToInt32(10 * Weight + 6.25 * Height - 5 * Age + 5);
            }
            else
            {
                BMR = Convert.ToInt32(10 * Weight + 6.25 * Height - 5 * Age - 161);
            }

            KcalMaintaince = Convert.ToInt32(1.2 * BMR);
        }
    }
}
