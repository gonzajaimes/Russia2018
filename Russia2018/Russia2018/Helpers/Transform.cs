namespace Russia2018.Helpers
{
    using Models;

    public static class Transform
    {
        public static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImageArray = user.ImageArray,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Password = user.Password,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
            };
        }

        internal static User ToUserDomain(UserLocal user, byte[] imageArray)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
                ImageArray = imageArray,
            };
        }  
    }
}
