using AutoMapper;
using Report.Data.Entites;
using Report.Data.FilterItems;
using Report.Data.ReportItems;
using Report.Data.SortItems;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Report.Data.Templates
{
    public interface ITemplateRepository
    {
        Task<List<TemplateDTO>> GetListAsync();
        Task<TemplateDTO> GetItemAsync(int id);
        Task<TemplateDTO> SaveAsync(TemplateDTO template);
        void Delete(int id);
    }

    public  class TemplateRepository : ITemplateRepository
    {
        private ITemplateContext _dbContext;

        private IReportItemContext _rdbContext;
        private ISortItemContext _sdbContext;
        private IFilterItemContext _fdbContext;

        public TemplateRepository()
        {
            _dbContext = new TemplateContext();
            _rdbContext = new ReportItemContext();
            _sdbContext = new SortItemContext();
            _fdbContext = new FilterItemContext();
        }

        public TemplateRepository(ITemplateContext dbcontext,
            IReportItemContext rdbContext,
         ISortItemContext sdbContext,
         IFilterItemContext fdbContext)
        {
            _dbContext = dbcontext;

            _rdbContext = rdbContext;
            _sdbContext = sdbContext;
            _fdbContext = fdbContext;
        }

        public void Delete(int id)
        {
            try
            {
                var item = _dbContext.Templates.Where(t => t.Id == id).SingleOrDefault();
                if(item != null)
                {
                    var rItems = _rdbContext.ReportItems.Where(e => e.TemplateId == item.Id).ToList();
                    if(rItems?.Count() > 0) rItems.ForEach(r => _rdbContext.ReportItems.Remove(r));

                    var sItems = _sdbContext.SortItems.Where(e => e.TemplateId == item.Id).ToList();
                    if (sItems?.Count() > 0) sItems.ForEach(s=> _sdbContext.SortItems.Remove(s));

                    var fItems = _fdbContext.FilterItems.Where(e => e.TemplateId == item.Id).ToList();
                    if (fItems?.Count() > 0) fItems.ForEach(f => _fdbContext.FilterItems.Remove(f));


                    _dbContext.Templates.Remove(item);
                    _dbContext.SaveChanges();
                    _rdbContext.SaveChanges();
                    _sdbContext.SaveChanges();
                    _fdbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;          
            }
        }

        public async Task<TemplateDTO> GetItemAsync(int id) =>
             await (from t in _dbContext.Templates
                    where t.Id == id
                    select new TemplateDTO
                    {
                        Id = t.Id,
                        Name = t.Name,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
                        OutputFormat = t.OutputFormat,
                        LastModifiedDate = t.LastModifiedDate,
                        LastModifiedUser = t.LastModifiedUser

                    }).FirstOrDefaultAsync();

        public async Task<List<TemplateDTO>> GetListAsync() =>
            await (from t in _dbContext.Templates                          
                          select new TemplateDTO
                          {
                              Id = t.Id,
                              Name = t.Name,
                              CreatedBy = t.CreatedBy,
                              CreatedOn = t.CreatedOn,
                              OutputFormat = t.OutputFormat,
                              LastModifiedDate = t.LastModifiedDate,
                              LastModifiedUser = t.LastModifiedUser

                          }).ToListAsync() ?? new List<TemplateDTO>();

        public async Task<TemplateDTO> SaveAsync(TemplateDTO template)
        {
            try
            {
                var temp = Mapper.Map<TemplateEntity>(template);               
                temp.LastModifiedUser = temp.CreatedBy;

                if (template.Id == 0)
                {
                    temp.CreatedOn = DateTime.Now;
                    _dbContext.Templates.Add(temp);
                    if (template?.ReportItems?.Count >0)
                        _rdbContext.ReportItems.Add(Mapper.Map<ReportItemEntity>(template?.ReportItems));
                    if (template?.SortItems?.Count > 0)
                        _sdbContext.SortItems.Add(Mapper.Map<SortItemEntity>(template?.SortItems));
                    if (template?.FilterItems?.Count > 0)
                        _fdbContext.FilterItems.Add(Mapper.Map<FilterItemEntity>(template?.FilterItems));
                }
                else
                {
                    temp.LastModifiedDate = DateTime.Now;
                    _dbContext.Templates.Update(temp);
                    if (template?.ReportItems?.Count > 0)
                        _rdbContext.ReportItems.Update(Mapper.Map<ReportItemEntity>(template?.ReportItems));
                    if (template?.SortItems?.Count > 0)
                        _sdbContext.SortItems.Update(Mapper.Map<SortItemEntity>(template?.SortItems));
                    if (template?.FilterItems?.Count > 0)
                        _fdbContext.FilterItems.Update(Mapper.Map<FilterItemEntity>(template?.FilterItems));
                }

                _dbContext.SaveChanges();

                return await Task.Run (() => Mapper.Map<TemplateDTO>(temp));
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
