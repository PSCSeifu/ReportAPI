using AutoMapper;
using Report.Data.FilterItems;
using Report.Data.ReportItems;
using Report.Data.SortItems;
using Report.Data.Templates;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Service.TemplateService
{
    public interface ITemplateService
    {
        Task<List<TemplateDTO>> GetListAsync();
        Task<TemplateDTO> GetItemAsync(int id);
        Task<TemplateDTO> Save(TemplateDTO templateDTO);
        void Delete(int id);
    }

    public class TemplateService : ITemplateService
    {
        private ITemplateRepository _tRepo;
        private IReportItemRepository _rRepo;
        private ISortItemRepository _sRepo;
        private IFilterItemRepository _fRepo;

        public TemplateService(ITemplateRepository tRepo,
            IReportItemRepository rRepo,
            ISortItemRepository sRepo,
            IFilterItemRepository fRepo)
        {
            _tRepo = tRepo;
            _rRepo = rRepo;
            _sRepo = sRepo;
            _fRepo = fRepo;
        }

        public async Task<TemplateDTO> GetItemAsync(int id)
        {
            try
            {
                var item =  Mapper.Map<TemplateDTO>( await _tRepo.GetItemAsync(id));

                if (item != null)
                {
                    item.ReportItems = Mapper.Map<List<ReportItemDTO>>(_rRepo.GetList(item.Id));
                    item.SortItems = Mapper.Map<List<SortItemDTO>>(_sRepo.GetList(item.Id));
                    item.FilterItems = Mapper.Map<List<FilterItemDTO>>(_fRepo.GetList(item.Id));
                    return item;
                }

                return new TemplateDTO();
            }
            catch (Exception e)
            {
                var x = e.ToString();
                return new TemplateDTO();
            }
        }

        public async Task<TemplateDTO> Save(TemplateDTO templateDTO)
        {
            var createdBy = (templateDTO.Id == 0) ? templateDTO.CreatedBy : templateDTO.LastModifiedUser;
           
            if (templateDTO?.ReportItems?.Count > 0)
                templateDTO?.ReportItems?.ForEach(r => r.LastModifiedUser = createdBy);
            if (templateDTO?.SortItems?.Count > 0)
                templateDTO?.SortItems?.ForEach(r => r.LastModifiedUser = createdBy);
            if (templateDTO?.FilterItems?.Count > 0)
                templateDTO?.FilterItems?.ForEach(r => r.LastModifiedUser = createdBy);

            return  await _tRepo.SaveAsync(templateDTO);
        }
        

        public async Task<List<TemplateDTO>> GetListAsync()
        {
            try
            {
                var list = await _tRepo.GetListAsync();

                if (list != null)
                {
                    list.ForEach(l => {
                        l.ReportItems = _rRepo.GetList(l.Id);
                        l.SortItems = _sRepo.GetList(l.Id);
                        l.FilterItems = _fRepo.GetList(l.Id);
                    });

                    return list;
                }

                return new List<TemplateDTO>();
            }
            catch (Exception)
            {
                return new List<TemplateDTO>();
            }
        }

        public void Delete(int id) =>
            _tRepo.Delete(id);
            
    }
}
