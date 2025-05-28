using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.UseCase.DTOs
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public T Data { get; set; }

        public DateTime? ResponsedAt { get; set; }
    }
}
