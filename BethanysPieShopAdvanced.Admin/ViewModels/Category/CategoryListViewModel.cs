namespace BethanysPieShop.Admin.ViewModels.Category;

//Todo: In the view of this method handle possibility of null reference exception here
//      <td>@category!.DateAdded.Value.ToShortDateString()</td>
public class CategoryListViewModel
{
    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}
