using Report.Data.Entites;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report.Data.ReportItems
{
    public interface IReportItemRepository
    {
        List<ReportItemDTO> GetList(int templateId);
        ReportItemDTO GetItem(int id);
        ReportItemDTO Save(ReportItemDTO reportItem);
        void Delete(int id);
    }

    public class ReportItemRepository  : IReportItemRepository
    {
        private IReportItemContext _dbContext;

        public ReportItemRepository()
        {
            _dbContext = new ReportItemContext();
        }
        public ReportItemRepository(IReportItemContext context)
        {
            _dbContext = context;
        }

        public void Delete(int id)
        {
            try
            {
                var item = _dbContext.ReportItems.Where(r => r.Id == id).SingleOrDefault();
                if (item != null)
                {
                    _dbContext.ReportItems.Remove(item);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ReportItemDTO GetItem(int id) =>
            _dbContext.ReportItems
                            .Where(r => r.Id == id)
                             .Select(r => new ReportItemDTO
                             {
                                 Id = r.Id,
                                 TemplateId = r.TemplateId,
                                 Table = r.Table,
                                 Type = r.Type,
                                 IsNullable = r.IsNullable,
                                 Name = r.Name,
                                 IsDisplayed = r.IsDisplayed,
                                 LastModifiedDate = r.LastModifiedDate,
                                 LastModifiedUser = r.LastModifiedUser,
                                 CreatedDate = r.CreatedDate
                             }).SingleOrDefault();
        
        public List<ReportItemDTO> GetList(int templateId) =>
             _dbContext.ReportItems
                 .Where(r => r.TemplateId == templateId)
                  .Select(r => new ReportItemDTO
                  {
                      Id = r.Id,
                      TemplateId = r.TemplateId,
                      Table = r.Table,
                      Type = r.Type,
                      IsNullable = r.IsNullable,
                      Name = r.Name,
                      IsDisplayed = r.IsDisplayed,
                      LastModifiedDate = r.LastModifiedDate,
                      LastModifiedUser = r.LastModifiedUser,
                      CreatedDate = r.CreatedDate
                  })?.ToList() ?? new List<ReportItemDTO>();

        public ReportItemDTO Save(ReportItemDTO reportItem)
        {
            try
            {   

                if (reportItem.Id == 0)
                {
                    reportItem.CreatedDate = DateTime.Now;
                    _dbContext.ReportItems.Add(AutoMapper.Mapper.Map<ReportItemEntity>(reportItem));
                }
                else
                {
                    reportItem.LastModifiedDate = DateTime.Now;
                    _dbContext.ReportItems.Update(AutoMapper.Mapper.Map<ReportItemEntity>(reportItem));
                }

                _dbContext.SaveChanges();

                return GetItem(reportItem.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
