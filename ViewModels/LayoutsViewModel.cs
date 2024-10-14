using e_learning.DataTransfersObjects;

namespace e_learning.ViewModels
{
    public class LayoutsViewModel(UserDto user)
    {
        public UserDto User { get; set; } = user;

    }
}
