using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Mappers
{
    public class CategoryMapper
    {
        public IEnumerable<CategoryDto> MapToCategoryDtoList(IEnumerable<Category> categoryListMapFrom)
        {
            var categoryDtoList = new List<CategoryDto>();
            foreach (var category in categoryListMapFrom)
            {
                categoryDtoList.Add(MapToCategoryDto(category));
            }

            return categoryDtoList;
        }


        public CategoryDto MapToCategoryDto(Category entityMapFrom)
        {
            var categoryDtoMapTo = new CategoryDto
            {
                Id = entityMapFrom.Id,
                CategoryName = entityMapFrom.CategoryName
            };
            return categoryDtoMapTo;
        }
    }
}