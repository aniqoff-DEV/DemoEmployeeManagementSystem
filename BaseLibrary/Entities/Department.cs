namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {
        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }
        public List<Branch>? Branches { get; set; }
    }
}
