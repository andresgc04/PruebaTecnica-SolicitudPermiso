using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Generic;
using Entities;
using Entities.Data;
using Entities.DTO.Permits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnicaSolicitudesPermisos.Controllers.Permits
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitsController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IGenericRepository<Permiso> _GenericRepository;

        public PermitsController(IUnitOfWork UnitOfWork, IGenericRepository<Permiso> GenericRepository)
        {
            this._UnitOfWork = UnitOfWork;
            this._GenericRepository = GenericRepository;
        }


        // GET: api/Permits
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> PermissionList()
        {
            try
            {
                var GetPermissions = await _UnitOfWork.Context.Permiso.ToListAsync();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = GetPermissions
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

        // GET: api/Permits/5
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> ListOfPermissionsByID(int id)
        {
            try
            {
                var GetPermissions = await _UnitOfWork.Context.Permiso.Where(x => x.PermisoId == id).FirstOrDefaultAsync();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = GetPermissions
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
        public async Task<IActionResult> ListOfPermits()
        {
            try
            {
                var GetPermissions = await PermitsListViews();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = GetPermissions
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
        public async Task<IActionResult> RegisterPermissions([FromBody] Permiso permiso)
        {
            try
            {
                await _GenericRepository.CreateAsync(permiso);
                _UnitOfWork.Commit();


                return Ok(new Request()
                {
                    status = true,
                    message = "The permit was registered correctly",
                    data = permiso
                });
            }
            catch (Exception ex)
            {

                return Ok(new Request()
                {
                    status = false,
                    message = "The permit was not properly registered",
                    data = ex.Message
                });
            }
        }

        // PUT: api/Permissions/5
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> ModifyPermits([FromBody] Permiso permiso)
        {
            try
            {
                await _GenericRepository.UpdateAsync(permiso);
                _UnitOfWork.Commit();

                return Ok(new Request()
                {
                    status = true,
                    message = "This action was successful",
                    data = permiso

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

        private async Task<List<PermitsListView>> PermitsListViews()
        {

            var PermitsList = new List<PermitsListView>();


            PermitsList = await _UnitOfWork.Context.Permiso.Select(a => new PermitsListView()
            {
                PermisoId = a.PermisoId,
                NombreEmpleado = a.NombreEmpleado,
                ApellidosEmpleado = a.ApellidosEmpleado,
                TipoPermiso = _UnitOfWork.Context.Permiso.Include(x => x.TipoPermiso).Where(x => x.PermisoId == a.PermisoId).Select(a => a.TipoPermiso.Descripcion).FirstOrDefault(),
                FechaPermiso = a.FechaPermiso

            }).ToListAsync();

            return PermitsList;

        }
    }
}
