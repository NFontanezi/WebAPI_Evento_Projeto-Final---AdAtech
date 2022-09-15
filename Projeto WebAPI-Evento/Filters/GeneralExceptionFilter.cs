using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace Projeto_WebAPI_Evento.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro Inesperado",
                Detail = "Ocorreu um erro inesperado na solicitação",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

            switch (context.Exception)
            {
                case SqlException:
                    problem.Status = 503;
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status503ServiceUnavailable
                    };
                    break;

                case ArgumentNullException:
                    problem.Status = 501;
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status501NotImplemented
                    };
                    break;

                case ArgumentException:
                    problem.Status = 404;
                    context.Result = new ObjectResult(problem)
                    {
                        
                        StatusCode = StatusCodes.Status404NotFound
                    };
                    break;

                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;

            }
           
        }
    }
}
