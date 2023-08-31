using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class StudentByIdQuery : IRequest<StudentResponse>
    {
        public Guid Id { get; set; }
        public StudentByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}
