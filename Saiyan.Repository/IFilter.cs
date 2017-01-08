
namespace Saiyan.Repository
{
    public interface IFilter
    {
        int? Limit { get; set; }
        string SortColumn { get; set; }
        bool isDescending { get; set; }
    }
}
