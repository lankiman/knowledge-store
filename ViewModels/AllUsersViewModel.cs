using e_learning.DataTransfersObjects;

namespace e_learning.ViewModels;

public class AllUsersViewModel
{
    public List<UserDto>? Users { get; set; }

    public string? Filters { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }
}