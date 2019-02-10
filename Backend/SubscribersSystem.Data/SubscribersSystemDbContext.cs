using SubscribersSystem.Data.Models;
using SubscribersSystem.Data.ReportModels;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SubscribersSystem.Data
{
    public interface ISubscribersSystemDbContext : IDisposable
    {
        DbSet<Connection> ConnectionDbSet { get; set; }
        DbSet<Offer> OfferDbSet { get; set; }
        DbSet<Phone> PhoneDbSet { get; set; }
        DbSet<Sms> SmsDbSet { get; set; }
        DbSet<Subscriber> SubscriberDbSet { get; set; }
        DbSet<Invoice> InvoiceDbSet { get; set; }
        DbSet<PhoneReport> PhoneReportDbSet { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }

    public class SubscribersSystemDbContext : DbContext, ISubscribersSystemDbContext
    {
        public SubscribersSystemDbContext() : base(GetConnectionString())
        { }

        public DbSet<Subscriber> SubscriberDbSet { get; set; }
        public DbSet<Offer> OfferDbSet { get; set; }
        public DbSet<Phone> PhoneDbSet { get; set; }
        public DbSet<Sms> SmsDbSet { get; set; }
        public DbSet<Connection> ConnectionDbSet { get; set; }

        public DbSet<Invoice> InvoiceDbSet { get; set; }
        public DbSet<PhoneReport> PhoneReportDbSet { get; set; }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SubscribersSystemDbConnectionString"].ConnectionString;
        }
    }
}
