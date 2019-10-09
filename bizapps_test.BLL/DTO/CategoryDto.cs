namespace bizapps_test.BLL.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public BlogDto RelatedTo { get; set; }
    }
}