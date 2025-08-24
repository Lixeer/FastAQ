

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using FastAQ.Models.ApiResponeEntity;
using FastAQ.Models.AQEntity;
using FastAQ.Models.ESEntitys;
using FastAQ.Services.AQDBServices;
using FastAQ.Services.ElasticSearchServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;

[Route("api/FastAQController")]
[ApiController]
public class FastAQController : ControllerBase
{

    private readonly IWebHostEnvironment _env;
    
    private readonly ElasticSearchServices _elasticSearchServices;
    public FastAQController(
        IWebHostEnvironment env,
        
        ElasticSearchServices elasticSearchServices
    )
    {
        
        _env = env;
        _elasticSearchServices = elasticSearchServices;
    }




    
    [HttpGet("/")]
    public async Task<IActionResult> HandleIndex()

    {

        return Ok("welcome to FastA&Q WebAPI");

    }

    [HttpGet("/get_qestions")]
    public async Task<IActionResult> HandleGetQuestions(string keyword)

    {
        ApiResponeEntity result;
        try
        {
            var rp = await _elasticSearchServices.SearchAsync<ESSerchResponse<AQDocument>>(index_name: "dev_qa_index", keyword: keyword);
            result = new ApiResponeEntity
            {
                IsSuccess = true,
                Result = rp,

            };
        }
        catch (JsonException)
        {
            result = new ApiResponeEntity
            {
                IsSuccess = false,
                FailInfo = "can not find the keyword"
            };
        }
        catch (Exception)
        {
            result = new ApiResponeEntity
            {
                IsSuccess = false,
                FailInfo = "Unknow"
            };
        }
        return Ok(result);

    }
    [HttpPost("/insert_qestions_and_answer")]
    public async Task<IActionResult> HandleInsertQA(AQDocument entity)
    {
        var rp = await _elasticSearchServices.InsertAsync<ESInsertRespone>(index_name: "dev_qa_index", doc: entity);
        return Ok(
            new ApiResponeEntity
            {
                IsSuccess = true,
                Result = rp
            }
        );
    }

    [HttpPost("/delete_doc")]
    public async Task<IActionResult> HandleDeleteDoc(string question_name)
    {
        var _delquery = new
        {
            query = new
            {
                match = new
                {
                    question = question_name
                }
            }
        };
        ApiResponeEntity responeEntity;

        try
        {
            var rp = await _elasticSearchServices.DeleteDocAsync<ESDeleteDocRespone>(index_name: "dev_qa_index", query: _delquery);
            responeEntity = new ApiResponeEntity
            {
                Result = rp,
                IsSuccess = true
            };
        }
        catch (JsonException)
        {   
            responeEntity = new ApiResponeEntity
            {
                FailInfo = "doc is not exits",
                IsSuccess = false
            };
            
        }
        catch (Exception)
        {
            responeEntity = new ApiResponeEntity
            {
                FailInfo = "unknow",
                IsSuccess = false
            };
        }
        return Ok(responeEntity);
    }
    
    
}