using SubscribersSystem.Business.Models;
using SubscribersSystem.Data;
using SubscribersSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribersSystem.Business.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceBl> AddAsync(InvoiceBl invoiceBl, int indexOfSubscriber);
        string GenerateInvoiceNumber(SubscriberBl subscriberBl);
        List<InvoiceBl> GetAll();
        List<InvoiceBl> GetAllInvoicesForSubscriber(int indexOfSubscriber);
        InvoiceBl GetInvoiceByNumber(string numberOfInvoice);
        Task<InvoiceBl> GetInvoiceByIdAsync(int indexOfInvoice);
        DateTime GetDateOfPreviousInvoice(SubscriberBl subscriberBl);
        void RemoveInvoiceFile(string path);
    }

    internal class InvoiceService : IInvoiceService
    {
        private IDataObjectMapper _dataObjectMapper;
        private Func<ISubscribersSystemDbContext> _dbContextFactory;

        public InvoiceService(Func<ISubscribersSystemDbContext> dbContextFactory, 
                              IDataObjectMapper dataObjectMapper)
        {
            _dbContextFactory = dbContextFactory;
            _dataObjectMapper = dataObjectMapper;
        }

        public async Task<InvoiceBl> AddAsync(InvoiceBl invoiceBl, int indexOfSubscriber)
        {
            var invoice = _dataObjectMapper.MapInvoiceBlToInvoice(invoiceBl);
            Invoice invoiceToReturn;
            using (var dbContext = _dbContextFactory())
            {
                var chosenSubscriber = await dbContext.SubscriberDbSet
                    .SingleOrDefaultAsync(s => s.Id == indexOfSubscriber);

                invoice.Subscriber = chosenSubscriber;

                return await Task.Run(() => 
                                {
                                    invoiceToReturn = dbContext.InvoiceDbSet.Add(invoice);
                                    dbContext.SaveChanges();
                                    return _dataObjectMapper.MapInvoiceToInvoiceBl(invoiceToReturn);
                                });
            }
        }

        public List<InvoiceBl> GetAll()
        {
            List<Invoice> invoices;
            using (var dbContext = _dbContextFactory())
            {
                invoices = dbContext.InvoiceDbSet
                    .Include(i => i.Subscriber)
                    .Include(i => i.Subscriber.Phones.Select(p => p.Offer))
                    .Include(i => i.Subscriber.Phones.Select(p => p.Subscriber))
                    .Include(i => i.Subscriber.Phones.Select(p => p.Connections))
                    .Include(i => i.Subscriber.Phones.Select(p => p.ShortTextMessages))
                    .Include(i => i.PhoneReports)
                    .ToList();
            }
            return _dataObjectMapper.MapInvoiceListToInvoiceBlList(invoices);
        }

        public List<InvoiceBl> GetAllInvoicesForSubscriber(int indexOfSubscriber)
        {
            List<Invoice> invoices;
            using (var dbContext = _dbContextFactory())
            {
                if(!dbContext.SubscriberDbSet.Any(s => s.Id == indexOfSubscriber))
                {
                    throw new Exception("There is no subscriber with provided index");
                }

                invoices = dbContext.InvoiceDbSet.Where(i => i.Subscriber.Id == indexOfSubscriber).ToList();
            }
            return _dataObjectMapper.MapInvoiceListToInvoiceBlList(invoices);
        }

        public InvoiceBl GetInvoiceByNumber(string numberOfInvoice)
        {
            Invoice invoice;
            using (var dbContext = _dbContextFactory())
            {
                if (!dbContext.InvoiceDbSet.Any(i => i.Number == numberOfInvoice))
                {
                    throw new Exception("There is no invoice with provided number!");
                }
                invoice = dbContext.InvoiceDbSet
                    .Include(i => i.PhoneReports)
                    .Include(i => i.Subscriber)
                    .SingleOrDefault(i => i.Number == numberOfInvoice);
            }
            return _dataObjectMapper.MapInvoiceToInvoiceBl(invoice);
        }

        public async Task<InvoiceBl> GetInvoiceByIdAsync(int indexOfInvoice)
        {
            Invoice invoice;
            using (var dbContext = _dbContextFactory())
            {
                if (!(await dbContext.InvoiceDbSet.AnyAsync(i => i.Id == indexOfInvoice)))
                {
                    throw new Exception("There is no invoice with provided number!");
                }
                invoice = await dbContext.InvoiceDbSet
                    .Include(i => i.PhoneReports)
                    .Include(i => i.Subscriber)
                    .SingleOrDefaultAsync(i => i.Id == indexOfInvoice);
            }
            return _dataObjectMapper.MapInvoiceToInvoiceBl(invoice);
        }

        public DateTime GetDateOfPreviousInvoice(SubscriberBl subscriberBl)
        {
            DateTime dateOfPreviousInvoice;

            using (var dbContext = _dbContextFactory())
            {
                if (!dbContext.InvoiceDbSet.Any(i => i.Subscriber.Id == subscriberBl.Id))
                {
                    if (dbContext.PhoneDbSet.Where(p => p.Subscriber.Id == subscriberBl.Id).SelectMany(c => c.Connections).Count() != 0)
                    {
                        return dateOfPreviousInvoice = dbContext.PhoneDbSet
                                            .Where(p => p.Subscriber.Id == subscriberBl.Id)
                                            .Select(p => p.Connections
                                            .Min(c => c.DateOfBeginning)).Min();
                    }
                    return dateOfPreviousInvoice = DateTime.Now;
                }
                dateOfPreviousInvoice = dbContext.InvoiceDbSet
                                            .Where(i => i.Subscriber.Id == subscriberBl.Id)
                                            .OrderByDescending(i => i.GenerationDate)
                                            .First().GenerationDate;
            }

            return dateOfPreviousInvoice;
        }

        public string GenerateInvoiceNumber(SubscriberBl subscriberBl)
        {
            var numberOfInvoices = GetAll().Select(i => i.Subscriber).Where(s => s.Id == subscriberBl.Id).Count();
            numberOfInvoices++;

            return String.Format($"FV_{subscriberBl.Id:D4}_{numberOfInvoices:D4}");
        }

        public void RemoveInvoiceFile(string path)
        {
            File.Delete(path);
        }
    }
}
