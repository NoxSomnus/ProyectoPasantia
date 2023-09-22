﻿using FerminToroMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Queries
{
    public class DatosEmpresaJuridicaQuery : IRequest<EmpresaJuridicaResponse>
    {
        public Guid EmpresaJuridicaId;
        public DatosEmpresaJuridicaQuery(Guid empresaJuridicaId)
        {
            EmpresaJuridicaId = empresaJuridicaId;
        }
    }
}
