using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class EntryRecordDto
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public DateTime EntryTime { get; set; }
        public status Status { get; set; } = status.审核中;
        public string UserName { get; set; }
    }
}
