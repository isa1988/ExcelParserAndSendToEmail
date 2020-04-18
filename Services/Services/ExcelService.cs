using OfficeOpenXml;
using Services.Dtos;
using Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services.Services
{
    public class ExcelService : IExcelService
    {
        private List<UserInfoDto> _messageToUserList;

        public ExcelService()
        {
            _messageToUserList = new List<UserInfoDto>();
        }

        public List<UserInfoDto> MessageToUserList { get { return _messageToUserList; } }

        public EntityOperationResult<InportExcelDto> Analysis(InportExcelDto value)
        {
            try
            {
                if (value.File == null || value.File.Length == 0)
                    return EntityOperationResult<InportExcelDto>.Failure().AddError("Вы не выбрали файл");
                using (var stream = new MemoryStream(value.File))
                {
                    //await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        //var colCount = worksheet.Dimension.Columns;
                        int age = 0;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            age = 0;
                            int.TryParse(worksheet.Cells[row, 4].Value.ToString().Trim(), out age);
                            _messageToUserList.Add(new UserInfoDto
                            {
                                Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                SurName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                EMail = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                Age = age,
                            });
                        }
                    }
                }
                return EntityOperationResult<InportExcelDto>.Success(new InportExcelDto());
            }
            catch (Exception ex)
            {
                return EntityOperationResult<InportExcelDto>.Failure().AddError(ex.Message);
            }
        }
    }
}
