using CsvHelper.Configuration.Attributes;
namespace Application.DTO
{
    public class CirculationRecordExport
    {
        [Name("Название книги")]
        public string BookTitle { get; set; }
        [Name("Имя клиента")]
        public string ClientName { get; set; }
        [Name("Дата От")]
        public DateTime IssueFrom { get; set; }
        [Name("Дата До")]
        public DateTime IssueTo { get; set; }
    }

    public class CirculationRecordExportRetern
    {
        [Name("Название книги")]
        public string BookTitle { get; set; }
        [Name("Имя клиента")]
        public string ClientName { get; set; }
        [Name("Дата От")]
        public DateTime IssueFrom { get; set; }
        [Name("Дата возвращения")]
        public DateTime? IssueTo { get; set; }
    }
}