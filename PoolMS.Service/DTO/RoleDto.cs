using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Repository.DTO
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class RoleCreateDto
    {
        public string Title { get; set; }
    }
    public class RoleUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
