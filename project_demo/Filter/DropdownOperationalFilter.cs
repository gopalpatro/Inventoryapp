using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using project_demo.Data;

namespace project_demo.Filter
{
    public class DropdownOperationalFilter : IOperationFilter
    {
        private readonly AppDbContext _context;
        public DropdownOperationalFilter(AppDbContext context)
        {
            _context = context;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            if (context.ApiDescription.RelativePath.Contains("approve/{id}"))
            {
                var unapprovedIds = _context.Registrationrequests.Where(r => !r.IsApproved).Select(r => r.Id).ToList();
                var parameter = operation.Parameters.FirstOrDefault(p => p.Name == "id");
                if (parameter != null)
                {
                    parameter.Schema.Enum = (IList<Microsoft.OpenApi.Any.IOpenApiAny>)unapprovedIds.Cast<object>().ToList();
                }

            }
        }
    }
}
