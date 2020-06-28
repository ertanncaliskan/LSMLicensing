using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LicenseSignatureService
{
    public class LicenseContext : DbContext
    {
        private string _connectionString;
        public LicenseContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }
        public DbSet<LicenseRegistry> LicenseRegistries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(string.IsNullOrEmpty(_connectionString) ? "Data Source=license.db" : _connectionString);
    }

    public class LicenseRegistry
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string LicenseKey { get; set; }
        public string LicenseSecret { get; set; }
        public string LicenseSignature { get; set; }

    }

}
