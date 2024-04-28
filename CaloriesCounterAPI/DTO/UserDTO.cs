namespace CaloriesCounterAPI.DTO
{
    /// <summary>
    /// Data Transfer Object for user information.
    /// </summary>
    public class UserDTO
    {
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
    }
}
