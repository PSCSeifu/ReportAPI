using Report.Data.Entites;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report.Data.FilterItems
{
    public interface IFilterItemRepository
    {
        List<FilterItemDTO> GetList(int templateId);
        FilterItemDTO GetItem(int id);
        FilterItemDTO Save(FilterItemDTO filterItem);       
        void Delete(int id);
    }

    public class FilterItemRepository : IFilterItemRepository
    {

        private IFilterItemContext _dbContext;

        public FilterItemRepository()
        {
            _dbContext = new FilterItemContext();
        }

        public FilterItemRepository(IFilterItemContext context)
        {
            _dbContext = context;            
        }

        public void Delete(int id)
        {
            try
            {
                var item = _dbContext.FilterItems.Where(f => f.Id == id).SingleOrDefault();
                if(item != null)
                {
                    _dbContext.FilterItems.Remove(item);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public FilterItemDTO GetItem(int id) =>
            _dbContext.FilterItems.Where(f => f.Id == id)
                .Select(f => new FilterItemDTO
                {
                    Id = f.Id,
                    TemplateId = f.TemplateId,
                    Name = f.Name,
                    Priority = f.Priority,
                    Value = f.Value,
                    LastModifiedDate = f.LastModifiedDate,
                    LastModifiedUser = f.LastModifiedUser,
                    CreatedDate = f.CreatedDate
                }).SingleOrDefault() ?? new FilterItemDTO();

        public List<FilterItemDTO> GetList(int templateId) =>
             _dbContext.FilterItems.Where(f => f.TemplateId == templateId)
                .Select(f => new FilterItemDTO
                {
                    Id = f.Id,
                    TemplateId = f.TemplateId,
                    Name = f.Name,
                    Priority = f.Priority,
                    Value = f.Value,
                    LastModifiedDate = f.LastModifiedDate,
                    LastModifiedUser = f.LastModifiedUser,
                    CreatedDate = f.CreatedDate
                }).ToList() ?? new List<FilterItemDTO>();


        public FilterItemDTO Save(FilterItemDTO filterItem)
        {
            try
            {
                if (filterItem.Id == 0)
                {
                    _dbContext.FilterItems.Add(AutoMapper.Mapper.Map<FilterItemEntity>(filterItem));
                }
                else
                {
                    _dbContext.FilterItems.Update(AutoMapper.Mapper.Map<FilterItemEntity>(filterItem));
                }
                
                _dbContext.SaveChanges();

                return GetItem(filterItem.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
