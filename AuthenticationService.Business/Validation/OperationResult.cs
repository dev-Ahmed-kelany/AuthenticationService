using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Business.Validation
{
    public class OperationResult<TData>
    {
        public bool Success { get; }
        public TData? Data { get; }
        public List<string> Errors { get; } = new List<string>();
    }
}
