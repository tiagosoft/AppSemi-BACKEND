using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSemi.Domain.Entities
{
    public class Patients
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Exams
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Orders
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public int ExamCount { get; set; }
        public DateTime AttentionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Exams { get; set; }
    }
    public class OrdersDTO
    {
        public string PatientName { get; set; }
        public DateTime AttentionDate { get; set; }
        public List<int> ExamsId { get; set; }
    }

    public class OrdersDetail
    {
        public int Id { get; set; }
        public string Exams { get; set; }
    }
    public class OrdersDetailDTO {
        public int Id { get; set; }
    }
}
