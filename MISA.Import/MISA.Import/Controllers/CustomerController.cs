using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Import.Entity;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MISA.Import.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IDbConnection dbConnection;

        private string connectionString = ""
                + "Host=47.241.69.179;"
                + "Port=3306;"
                + "User Id=dev;"
                + "Password=12345678;"
                + "Database = MF810-Import-TMQuy;"
                + "convert zero datetime=True";

        [HttpPost]
        public IActionResult Import(IFormFile formFile)
        {
            if (formFile == null || formFile.Length < 0)
            {
                return BadRequest();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }
            var customers = GetAllCustomer(formFile);
            Validate(customers);
            var res = InsertCustomer(customers);
            var result = new
            {
                TotalRecord = customers.Count,
                Success = res,
                Data = customers
            };
            return Ok(result);
        }

        private List<Customer> GetAllCustomer(IFormFile formFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var customers = new List<Customer>();

            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    for (int row = 3; row < worksheet.Dimension.Rows; row++)
                    {
                        Customer c = new Customer(
                            worksheet.Cells[row, 2].Value == null ? null : worksheet.Cells[row, 2].Value.ToString(),
                            worksheet.Cells[row, 1].Value == null ? null : worksheet.Cells[row, 1].Value.ToString(),
                            worksheet.Cells[row, 3].Value == null ? null : worksheet.Cells[row, 3].Value.ToString(),
                            worksheet.Cells[row, 4].Value == null ? null : worksheet.Cells[row, 4].Value.ToString(),
                            null,
                            worksheet.Cells[row, 5].Value == null ? null : worksheet.Cells[row, 5].Value.ToString(),
                            FormatDob(worksheet.Cells[row, 6].Value == null ? null : worksheet.Cells[row, 6].Value.ToString()),
                            worksheet.Cells[row, 7].Value == null ? null : worksheet.Cells[row, 7].Value.ToString(),
                            worksheet.Cells[row, 8].Value == null ? null : worksheet.Cells[row, 8].Value.ToString(),
                            worksheet.Cells[row, 9].Value == null ? null : worksheet.Cells[row, 9].Value.ToString(),
                            worksheet.Cells[row, 10].Value == null ? null : worksheet.Cells[row, 10].Value.ToString(),
                            worksheet.Cells[row, 11].Value == null ? null : worksheet.Cells[row, 11].Value.ToString(),
                            null
                        );
                        customers.Add(c);
                    }
                }
                return customers;
            }
        }

        private DateTime? FormatDob(string dob)
        {
            try
            {
                string[] formats = { "dd/MM/yyyy", "MM/yyyy", "yyyy" };
                dob = dob.Trim();
                if (dob == null)
                {
                    return null;
                }
                else
                {
                    var result = DateTime.ParseExact(dob, formats, provider: null);
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private int InsertCustomer(List<Customer> customes)
        {
            string sqlCommand = "Proc_InsertCustomer";
            var res = 0;
            foreach (Customer customer in customes)
            {
                if (customer.Status == null)
                {
                    using (dbConnection = new MySqlConnection(connectionString))
                    {
                        dbConnection.Execute(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);
                        res++;
                    }
                }
            }
            return res;
        }

        private void Validate(List<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                CheckCustomerCodeExists(customer);
                CheckCustomerGroupNameExists(customer);
                CheckPhoneNumberExists(customer);
            }
            //Check CustomerCode
            for (int i = 1; i < customers.Count; i++)
            {
                for (int j = i + 1; j < customers.Count - 1; j++)
                {
                    if (customers[i].CustomerCode == customers[j].CustomerCode)
                    {
                        customers[i].Status += "Mã khách hàng tồn tại trên tệp\n";
                        break;
                    }
                }
            }

            //Check PhoneNumber
            for (int i = 1; i < customers.Count; i++)
            {
                for (int j = i + 1; j < customers.Count - 1; j++)
                {
                    if (customers[i].PhoneNumber == customers[j].PhoneNumber)
                    {
                        customers[i].Status += $"Số điện thoại {customers[i].PhoneNumber} tồn tại trên tệp\n";
                        break;
                    }
                }
            }
        }

        private void CheckCustomerCodeExists(Customer customer)
        {
            var sqlCommand = "Proc_CheckCustomerCodeExists";
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);

                if (res)
                {
                    customer.Status += "Mã khách hàng tồn tại trên hê thống\n";
                }
            }
        }

        private void CheckCustomerGroupNameExists(Customer customer)
        {
            var sqlCommand = "Proc_CheckCustomerGroupNameExists";
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);
                if (!res)
                {
                    customer.Status += "Tên nhóm khách hàng không tồn tại trên hê thống\n";
                }
            }
        }

        private void CheckPhoneNumberExists(Customer customer)
        {
            var sqlCommand = "Proc_CheckPhoneNumberExists";
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var res = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: customer, commandType: CommandType.StoredProcedure);
                if (res)
                {
                    customer.Status += "Số điện thoại tồn tại trên hê thống\n";
                }
            }
        }
    }
}