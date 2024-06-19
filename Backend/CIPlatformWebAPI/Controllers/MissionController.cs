using Business_logic_Layer;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly BALMission _balMission;
        private readonly ResponseResult result = new ResponseResult();
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public MissionController(BALMission balMission, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _balMission = balMission;
            _environment = environment;
        }

        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            try
            {
                result.Data = _balMission.MissionList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("AddMission")]
        public ResponseResult AddMission(Missions mission, [FromQuery] List<string> imageFiles)
        {
            try
            {
                mission.MissionImages = string.Join(";", imageFiles);
                result.Data = _balMission.AddMission(mission);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public ActionResult<ResponseResult> DeleteMission(int id)
        {
            try
            {
                var resultMessage = _balMission.DeleteMission(id);
                return Ok(new ResponseResult { Data = resultMessage, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("MissionDetailById/{id}")]
        public ActionResult<ResponseResult> MissionDetailById(int id)
        {
            try
            {
                var mission = _balMission.MissionDetailById(id);
                if (mission == null)
                {
                    return NotFound(new ResponseResult { Result = ResponseStatus.Error, Message = "Mission not found." });
                }

                return Ok(new ResponseResult { Data = mission, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UpdateMission")]
        public ActionResult<ResponseResult> UpdateMission(Missions mission)
        {
            try
            {
                // Retrieve the mission to update
                var existingMission = _balMission.MissionList().FirstOrDefault(m => m.Id == mission.Id);
                if (existingMission == null)
                {
                    return NotFound(new ResponseResult { Result = ResponseStatus.Error, Message = "Mission not found." });
                }

                existingMission.MissionTitle = mission.MissionTitle;
                existingMission.MissionDescription = mission.MissionDescription;
                existingMission.MissionOrganisationName = mission.MissionOrganisationName;
                existingMission.MissionOrganisationDetail = mission.MissionOrganisationDetail;
                existingMission.CountryId = mission.CountryId;
                existingMission.CityId = mission.CityId;
                existingMission.StartDate = mission.StartDate;
                existingMission.EndDate = mission.EndDate;
                existingMission.MissionType = mission.MissionType;
                existingMission.TotalSheets = mission.TotalSheets;
                existingMission.RegistrationDeadLine = mission.RegistrationDeadLine;
                existingMission.MissionThemeId = mission.MissionThemeId;
                existingMission.MissionSkillId = mission.MissionSkillId;
                existingMission.MissionImages = mission.MissionImages;
                existingMission.MissionDocuments = mission.MissionDocuments;
                existingMission.MissionAvailability = mission.MissionAvailability;
                existingMission.MissionVideoUrl = mission.MissionVideoUrl;
                existingMission.IsDeleted = mission.IsDeleted;

                var resultMessage = _balMission.UpdateMission(existingMission);
                return Ok(new ResponseResult { Data = resultMessage, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> upload)
        {
            try
            {
                string filePath = "";
                string fullPath = "";
                var files = Request.Form.Files;
                List<string> fileList = new List<string>();
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePath = Path.Combine("UploadMissionImage", "Mission");
                        string fileRootPath = Path.Combine(_environment.WebRootPath, filePath);

                        if (!Directory.Exists(fileRootPath))
                        {
                            Directory.CreateDirectory(fileRootPath);
                        }

                        string name = Path.GetFileNameWithoutExtension(fileName);
                        string extension = Path.GetExtension(fileName);
                        string fullFileName = name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                        fullPath = Path.Combine(fileRootPath, fullFileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        fileList.Add(fullPath);
                    }
                }
                return Ok(fileList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("MissionApplicationApprove")]
        public ResponseResult MissionApplicationApprove(MissionApplication missionApplication)
        {
            try
            {
                result.Data = _balMission.MissionApplicationApprove(missionApplication.Id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionApplicationList")]
        public ResponseResult MissionApplicationList()
        {
            var result = new ResponseResult();
            try
            {
                result.Data = _balMission.GetMissionApplicationList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }
        [HttpDelete]
        [Route("MissionApplicationDelete/{id}")]
        public ActionResult<ResponseResult> MissionApplicationDelete(int id)
        {
            try
            {
                var resultMessage = _balMission.MissionApplicationDelete(id);
                var result = new ResponseResult { Data = resultMessage, Result = ResponseStatus.Success };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult { Result = ResponseStatus.Error, Message = ex.Message });
            }
        }
    }
}
