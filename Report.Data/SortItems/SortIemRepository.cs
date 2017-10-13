using Report.Data.Entites;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report.Data.SortItems
{
    public interface ISortItemRepository
    {
        List<SortItemDTO> GetList(int templateId);
        SortItemDTO GetItem(int id);
        SortItemDTO Save(SortItemDTO reportItem);
        void Delete(int id);
    }

    public class SortItemRepository : ISortItemRepository
    {
        private ISortItemContext _dbContext;

        public SortItemRepository()
        {
            _dbContext = new SortItemContext();
        }
        public SortItemRepository(ISortItemContext context)
        {
            _dbContext = context;
        }

        public void Delete(int id)
        {
            try
            {
                var item = _dbContext.SortItems.Where(r => r.Id == id).SingleOrDefault();
                if (item != null)
                {
                    _dbContext.SortItems.Remove(item);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SortItemDTO GetItem(int id) =>
            _dbContext.SortItems
                .Where(s => s.Id == id)
                .Select(s => new SortItemDTO
                {
                    Id = s.Id,
                    TemplateId = s.TemplateId,
                    Name = s.Name,
                    Priority = s.Priority,
                    OrderBy = s.OrderBy,
                    LastModifiedDate = s.LastModifiedDate,
                    LastModifiedUser = s.LastModifiedUser,
                    CreatedDate = s.CreatedDate
                }).SingleOrDefault();



        public List<SortItemDTO> GetList(int templateId) =>
             _dbContext.SortItems
                .Where(s => s.TemplateId == templateId)
                .Select(s => new SortItemDTO
                {
                    Id = s.Id,
                    TemplateId = s.TemplateId,
                    Name = s.Name,
                    Priority = s.Priority,
                    OrderBy = s.OrderBy,
                    LastModifiedDate = s.LastModifiedDate,
                    LastModifiedUser = s.LastModifiedUser,
                    CreatedDate = s.CreatedDate
                }).ToList() ?? new List<SortItemDTO>();



        public SortItemDTO Save (SortItemDTO sortItem)
        {
            try
            {
                if (sortItem.Id == 0)
                {
                    _dbContext.SortItems.Add(AutoMapper.Mapper.Map<SortItemEntity>(sortItem));
                }
                else
                {
                    _dbContext.SortItems.Update(AutoMapper.Mapper.Map<SortItemEntity>(sortItem));
                }

                _dbContext.SaveChanges();

                return GetItem(sortItem.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
