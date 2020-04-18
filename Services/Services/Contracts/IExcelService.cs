using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services.Contracts
{
    public interface IExcelService
    {
        List<UserInfoDto> MessageToUserList { get; }

        EntityOperationResult<InportExcelDto> Analysis(InportExcelDto value);
    }
}
