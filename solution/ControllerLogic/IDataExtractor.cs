using solution.Models;

namespace solution.ControllerLogic
{
    public interface IDataExtractor
    {
        ApiResult<ViewModel> GetDataFromText(string data);
    }
}
