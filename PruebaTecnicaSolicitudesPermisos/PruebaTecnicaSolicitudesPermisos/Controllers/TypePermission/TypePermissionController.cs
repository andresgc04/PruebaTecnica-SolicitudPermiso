using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Generic;
using Entities;
using Entities.Data;
using Entities.DTO.TypePermission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnicaSolicitudesPermisos.Controllers.TypePermission
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePermissionController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IGenericRepository<TipoPermiso> _GenericRepository;

        public TypePermissionController(IUnitOfWork UnitOfWork, IGenericRepository<TipoPermiso> GenericRepository)
        {
            this._UnitOfWork = UnitOfWork;
            this._GenericRepository = GenericRepository;
        }


        // GET: api/TypePermissions
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> PermitTypeList()
        {
            try
            {
                var GetTypePermissions = await _UnitOfWork.Context.TipoPermiso.ToListAsync();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = GetTypePermissions
                });
            }
            catch (Exception ex)
            {
                return Ok(new Request()
                {
                    status = true,
                    message = "An unexpected error occurred",
                    data = ex.Message
                });
            }
        }

        // GET: api/TypePermissions/5
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> ListOfPermissionTypesByID(int id)
        {
            try
            {
                var GetTypePermissions = await _UnitOfWork.Context.TipoPermiso.Where(x => x.TipoPermisoId == id).FirstOrDefaultAsync();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = GetTypePermissions
                });
            }
            catch (Exception ex)
            {
                return Ok(new Request()
                {
                    status = false,
                    message = "An unexpected error occurred",
                    data = ex.Message
                });
            }
        }

        //GET: api/
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListOfTypesOfPermits()
        {
            try
            {
                var GetTypePermissions = await TypesOfPermitsListViews();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = GetTypePermissions
                });
            }
            catch (Exception ex)
            {
                return Ok(new Request()
                {
                    status = false,
                    message = "An unexpected error occurred",
                    data = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterPermitTypes([FromBody] TipoPermiso tipoPermiso)
        {
            try
            {
                await _GenericRepository.CreateAsync(tipoPermiso);
                _UnitOfWork.Commit();


                return Ok(new Request()
                {
                    status = true,
                    message = "The type of permit was registered correctly",
                    data = tipoPermiso
                });
            }
            catch (Exception ex)
            {

                return Ok(new Request()
                {
                    status = false,
                    message = "The type of permit was not registered correctly",
                    data = ex.Message
                });
            }
        }

        // PUT: api/TypePermissions/5
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> ModifyPermitTypes([FromBody] TipoPermiso tipoPermiso)
        {
            try
            {
                await _GenericRepository.UpdateAsync(tipoPermiso);
                _UnitOfWork.Commit();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = tipoPermiso

                });
            }
            catch (Exception ex)
            {
                return Ok(new Request()
                {
                    status = false,
                    message = "An unexpected error occurred",
                    data = ex.Message
                });
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        //Functions:

        private async Task<List<TypePermissionListView>> TypesOfPermitsListViews()
        {

            var TypePermissionsList = new List<TypePermissionListView>();


            TypePermissionsList = await _UnitOfWork.Context.TipoPermiso.Select(a => new TypePermissionListView()
            {
                TipoPermisoId = a.TipoPermisoId,
                Descripcion=a.Descripcion

            }).ToListAsync();

            return TypePermissionsList;

        }
    }
}
