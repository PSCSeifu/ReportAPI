using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Report.Service.TemplateService;
using Report.Types.DTOs;
using ReportAPI.Common;
using ReportAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPI.Models;

namespace TemplateAPI.Controllers
{
    //[EnableCors("OpenGate")]
    [EnableCors("AngularPolicy")]
    [Route("api/[controller]")]
    public class TemplatesController : Controller
    {
        public readonly ProjectAppSettings _AppSettings;
        private ITemplateService _service;

        public TemplatesController(ITemplateService service, IOptions<ProjectAppSettings> options)
        {
            _AppSettings = options.Value;
            _service = service;
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok($"API Templates - Health OK - @{DateTime.Now.ToString()} ");
        }

        [HttpGet]        
        public async Task<IActionResult> GetList()
        {
            var list = Mapper.Map<List<TemplateCreateInputModel>>(await _service.GetListAsync());

            if (list != null) return Ok(list);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = Mapper.Map<TemplateCreateInputModel>(await _service.GetItemAsync(id));
            //TemplateCreateInputModel item = RawData().Where(x => x.Id == id).FirstOrDefault();
          
            if (item != null) return Ok(item);

            return NotFound();           
        }

        [HttpPost]
        public async Task<ActionResult> CreateTemplate([FromBody] TemplateCreateInputModel template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {                
                var newTemplate = await _service.Save(Mapper.Map<TemplateDTO>(template));
                return Ok(newTemplate);
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return NotFound();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(true);
            }
            catch (Exception e)
            {
                //log
                e.ToString();
                return BadRequest();
            }
        }


        private List<TemplateCreateInputModel> RawData()
        {
            List<TemplateCreateInputModel> templates = new List<TemplateCreateInputModel>()
            {
                new TemplateCreateInputModel { Id = 1, Name="Employee-Basic-Report", CreatedBy = "Psc", CreatedOn = new DateTime(2017,09,15), OutputFormat="csv",

                    ReportItems = new List<ReportItem> () {
                        new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Forename1", IsDisplayed = true },
                        new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Surname", IsDisplayed = true }
                    },

                    SortItems = new List<SortItem> (){ new SortItem {Name = "Surname", OrderBy = "ASC" } },
                    FilterItems = new List<FilterItem> (){ new FilterItem { Name = "Forename1", Value ="A"} }

                },
                new TemplateCreateInputModel {Id = 2,  Name="EE-ContactInfo", CreatedBy = "Admin", CreatedOn = new DateTime(2017,09,25), OutputFormat="csv",

                    ReportItems = new List<ReportItem> () {
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Forename1", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Surname", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Address1", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Address2", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Address3", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Address4", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Telephone", IsDisplayed = true }
                    },

                    SortItems = new List<SortItem> (){ new SortItem {Name = "Surname", Priority = 1, OrderBy = "ASC" } },
                    FilterItems = new List<FilterItem> (){  }

                },
                new TemplateCreateInputModel {Id = 3,  Name="EE Tax Report", CreatedBy = "Admin", CreatedOn = new DateTime(2017,09,25), OutputFormat="csv",

                    ReportItems = new List<ReportItem> () {
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Forename1", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "Surname", IsDisplayed = true },                    
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "PostCode", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "TaxBasis", IsDisplayed = true },
                    new ReportItem {Table="Employee", Type="string", IsNullable =true, Name= "TaxCode", IsDisplayed = true },
                    
                    },

                    SortItems = new List<SortItem> (){ new SortItem {Name = "Surname", Priority = 1, OrderBy = "ASC" }, new SortItem { Name= "PostCode",Priority =2, OrderBy="DESC"} },
                    FilterItems = new List<FilterItem> (){ new FilterItem { Name = "Forename1", Priority = 1, Value ="A"} }

                },


            };

            return templates;
        }
    }
}
