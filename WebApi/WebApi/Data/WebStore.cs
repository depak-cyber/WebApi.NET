using WebApi.Models.Dto;

namespace WebApi.Data
{
    public static class WebStore
    {
        public static List<WebDto> webList = new List<WebDto>
            {
                new WebDto { Id = 1,Name="Pool view" , Sqft=100, Occupancy=4},
                new WebDto { Id = 2, Name="Beach View" , Sqft=400, Occupancy=9}
            };
    }
}
